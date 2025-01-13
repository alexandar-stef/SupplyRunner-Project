using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class ActiveWeapon : MonoBehaviour
{   
    public ScreenManager screenManager;
    
    public Transform crosshair;
    public UnityEngine.Animations.Rigging.Rig handRig;
    public Transform weaponParent;
    public Transform LeftHand;
    public Transform RightHand;
    
    public Animator rigController;

    Animator animator;
    public RaycastWeapon raycastWeapon;
    
    

    private void Awake()
    {
        // get a reference to our main camera
        if (crosshair == null)
        {
            crosshair = GameObject.FindGameObjectWithTag("Aim_Point").GetComponent<Transform>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if (existingWeapon != null)
        {
            EquipWeapon(existingWeapon);
        }
    }

    // void checkEquippedWeapon(){
    //     RaycastWeapon raycastWeapon = GetComponentInChildren<RaycastWeapon>();
    //     if(raycastWeapon != null){
    //         handRig.weight = 1.0f;
    //         animator.SetLayerWeight(2, 1.0f);
    //     }
    // }

    // Update is called once per frame
    private void Update(){
        // checkEquippedWeapon();
        
        MyInput();
    }

    public void MyInput(){
        if(raycastWeapon && screenManager.mode == ScreenMode.fps){
            if(Input.GetKeyDown(KeyCode.Mouse0)) // Changed MyInput to Input
            {
                raycastWeapon.StartFiring();
            }
            else if(Input.GetKeyUp(KeyCode.Mouse0)) // Changed MyInput to Input
            {
                raycastWeapon.StopFiring();
            }
            
            else if(Input.GetKeyDown(KeyCode.R)) // Changed MyInput to Input
            {
                raycastWeapon.Reload();
            }
            else if(Input.GetKey(KeyCode.Mouse0) && raycastWeapon.allowButtonHold && raycastWeapon.bulletsLeft != 0) // Changed MyInput to Input
            {
                raycastWeapon.StartFiring();
            }

            
            
        }
        
    }

    public void EquipWeapon(RaycastWeapon weaponToEquip)
    {
        
        if (raycastWeapon)
        {
            Destroy(raycastWeapon.gameObject);
        }
        raycastWeapon = weaponToEquip;
        raycastWeapon.raycastDestination = crosshair;
        
        raycastWeapon.transform.parent = weaponParent;
        raycastWeapon.transform.localPosition = Vector3.zero;
        raycastWeapon.transform.localRotation = Quaternion.identity;
        rigController.Play("equip_"+raycastWeapon.weaponName);

        
        if(raycastWeapon.isRifle == true){
            animator.SetBool("HoldingRifle", true);
        }else{
            animator.SetBool("HoldingRifle", false);
        }
        if(raycastWeapon.isPistol == true){
            animator.SetBool("HoldingPistol", true);
        }else{  
            animator.SetBool("HoldingPistol", false);
        }

        
    }

}
