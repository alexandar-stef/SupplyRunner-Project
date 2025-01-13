using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    GameObject prefabInstance;
    GameObject rootPrefab;
    public int zombieTimer = 5;
    //-------------------------------------------

    //Gun variables
    [Header("Gun Values")]
    public float damage;
    public float timeBetweenShooting;
    public float reloadTime;
    public float timeBetweenShots;
    public int magazineSize;
    public bool allowButtonHold;
    public int bulletsLeft, bulletsShot;
    public PlayerInventory ammoInventory;
    public int trueMaxAmmo;
    bool readyToShoot, reloading;
    
    [Header("Raycast Weapon Values")]
    public bool isFiring = false;
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem bulletImpactMetal;
    public ParticleSystem bulletImpactFlesh;
    public Transform raycastOrigin;
    public Transform raycastDestination;

    [SerializeField] 
    public float recoilMagnitude = 1.0f;
    [SerializeField] 
    public float returnSpeed = 2.0f;

    [Header("Raycast Animator")]
    public string weaponName;
    public bool isRifle;
    public bool isPistol;
    public bool isShotgun;

    Ray ray;
    RaycastHit hitInfo;

    Weapon_Recoil weaponRecoil;


    [Header("Raycast Weapon Settings")]
    public float spread;
    public int bulletsPerTrigger;
    public AudioClip gunShot;
    public AudioClip gunReload;
    public AudioClip gunEmpty;


    void Awake()
    {
        trueMaxAmmo = magazineSize;
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    public void Start()
    {
        // thats a lot of parents
        ammoInventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerInventory>();
        weaponRecoil = GameObject.FindGameObjectWithTag("Player").GetComponent<Weapon_Recoil>();
        bulletsLeft = magazineSize;
        readyToShoot = true;
        
    }

    public void StartFiring()
    {
        if(readyToShoot && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTrigger;
            Shot();
        }
        else if(bulletsLeft <= 0)
        {
            if(gunEmpty != null){
                AudioSource.PlayClipAtPoint(gunEmpty, transform.position, 0.01f);
            }
        }
    }
    

    private void Shot(){
        readyToShoot = false;
        isFiring = true;

        //Raycast
        for(int i = 0; i < bulletsPerTrigger; i++)
        {
            ShootRaycast();
        }
        if(gunShot != null){
            AudioSource.PlayClipAtPoint(gunShot, transform.position, 0.01f);
        }
        

        //Decrement bullets
        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);
    }

    public void ShootRaycast()
    {
        //Muzzle Flash
        foreach (var particle in muzzleFlash){
            particle.Emit(1);
        }

        //Calculate Direction with Spread
        float x = UnityEngine.Random.Range(-spread, spread);
        float y = UnityEngine.Random.Range(-spread, spread);

        ray.origin = raycastOrigin.position;
        ray.direction = raycastDestination.position - raycastOrigin.position + new Vector3(x, y, x);
        if(Physics.Raycast(ray, out hitInfo)){
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);

            if(hitInfo.collider.CompareTag("ZombieHitbox"))
            {
                // bulletImpactFlesh.transform.position = hitInfo.point;
                // bulletImpactFlesh.transform.forward = hitInfo.normal;
                // bulletImpactFlesh.Emit(1);
                Hitbox zombie = hitInfo.collider.GetComponent<Hitbox>();
                zombie.TakeDamage(damage);

            }
            else{
                bulletImpactMetal.transform.position = hitInfo.point;
                bulletImpactMetal.transform.forward = hitInfo.normal;
                bulletImpactMetal.Emit(1);
            }
        }

        weaponRecoil.RecoilFire();

    }

    public void zombieRespawn()
    {
        rootPrefab.SetActive(true);
    }

    public void StopFiring()
    {
        isFiring = false;
    }

    private void ResetShot(){
        readyToShoot = true;
    }

    public void Reload(){

        bool bigWeapon = true;

        if (weaponName == "MP5" || weaponName == "M9")
        {
            bigWeapon = false;
        }

        int ammoAvailable = ammoInventory.GetAmmo(bigWeapon);

        magazineSize = Math.Min(bulletsLeft + ammoAvailable, trueMaxAmmo);

        ammoInventory.RemoveItem(
            bigWeapon ? "primary_ammo" : "sidearm_ammo", 
            magazineSize - bulletsLeft
        );
        
        if (ammoAvailable > 0) reloading = true;

        Invoke("ReloadFinished",reloadTime);
        if(gunReload != null && reloading == true){
            AudioSource.PlayClipAtPoint(gunReload, transform.position, 0.01f);
        }

    }

    private void ReloadFinished(){
        bulletsLeft = magazineSize;
        reloading = false;
    }

    public bool isReloading()
    {
        return reloading;
    }
}
