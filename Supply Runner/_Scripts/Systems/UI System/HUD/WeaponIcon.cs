using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfo : MonoBehaviour
{
    public Transform gunParent;
    public Image image;
    public TextMeshProUGUI gunName;
    
    private RaycastWeapon gun;

    private bool equipped;

    private string currentGun;

    // Start is called before the first frame update
    void Start()
    {
        try 
        {
            gun = gunParent.GetChild(2).GetComponent<RaycastWeapon>();
            equipped = true;
        }
        catch (Exception)
        {
            image.color = Color.clear;
            equipped = false;
        }
        
        currentGun = "none";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Refresh();
        if (equipped)
        {
            if (gun.weaponName != currentGun)
            {
                // load the image
                image.sprite = Resources.Load<Sprite>("Items/Gun Icons/" + gun.weaponName + "_icon");
                gunName.text = gun.weaponName;
                currentGun = gun.weaponName;
            }
            image.color = Color.white;
        }
    }

    public void Refresh()
    {
        Start();
    }
}
