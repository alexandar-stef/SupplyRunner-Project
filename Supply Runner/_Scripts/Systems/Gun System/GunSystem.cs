using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
    //Gun variables
    [Header("Gun Values")]
    public float damage = 10f;
    public float timeBetweenShooting;

    public float range = 100f;
    public float reloadTime;
    public float timeBetweenShots;
    public int magazineSize, bulletsPerTrigger;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    public Camera fpsCam;
    public Transform attackPoint;
    public LayerMask IsEnemy;

    

    void Start()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        
    }

    private void Awake() {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    // private void Update(){
    //     MyInput();
    // }

    // private void MyInput(){
    //     if (allowButtonHold)
    //     {
    //         shooting = Input.GetKey(KeyCode.Mouse0);
    //     }
    //     else
    //     {
    //         shooting = Input.GetKeyDown(KeyCode.Mouse0);
    //     }
    
    //     // Reloads
    //     if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading){
    //         Reload();
    //     }

    //     if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
    //     {
    //         Shoot();
            
    //     }

    // }

    // private void Shoot(){
    //     Debug.Log(bulletsLeft);
    //     readyToShoot = false;

    //     //Spread Calculation should be here
    //     // float y = Random.Range(-spread, spread);
    //     // float x = Random.Range(-spread, spread);

    //     //Spread Direction 
    //     // Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

    //     // raycastWeapon.StartFiring();
        
    //     // if(Physics.Raycast(fpsCam.transform.position, direction, out hit, range)){
    //     //     Debug.Log(hit.transform.name);
    //         // Debug.DrawLine()

    //         //enemy colliding should be here once we have enemies
    //     // }

    //     ////Graphics 
    //     // Instantiate(bulletHoleGraphic, hit.point + hit.normal * 0.001f, Quaternion.FromToRotation(Vector3.forward, hit.normal));
    //     // Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

    //     bulletsLeft--;
    //     bulletsShot--;

    //     Invoke("ResetShot", timeBetweenShooting);

    //     if (bulletsShot > 0 && bulletsLeft > 0)
    //     {
    //         bulletsShot = bulletsPerTrigger;
    //         Invoke("Shoot", timeBetweenShots);
    //     }

    // }

    private void ResetShot(){
        readyToShoot = true;
    }

    private void Reload(){
        reloading = true;

        Invoke("ReloadFinished",reloadTime);

    }

    private void ReloadFinished(){
        bulletsLeft = magazineSize;
        reloading = false;
    }

    public int getBulletsLeft() {
        return bulletsLeft;
    }

    public bool isReloading() {
        return reloading;
    }
}
