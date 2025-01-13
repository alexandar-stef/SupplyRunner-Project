using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoCount : MonoBehaviour
{
    public Transform gunParent;
    public PlayerInventory inventory;
    public TextMeshProUGUI text;
    public TextMeshProUGUI maxAmmo;
    
    private RaycastWeapon gun;
    private int mag;
    private int reserves;

    private bool equipped;

    // Start is called before the first frame update
    void Start()
    {
        try 
        {
            gun = gunParent.GetChild(2).GetComponent<RaycastWeapon>();
            mag = gun.bulletsLeft;
            equipped = true;
        }
        catch (Exception)
        {
            equipped = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Refresh();
        if (equipped)
        {
            mag = gun.bulletsLeft;
            text.text = mag.ToString();

            reserves = inventory.GetAmmo(!(gun.weaponName == "M9" || gun.weaponName == "MP5"));
            maxAmmo.text = reserves.ToString();
        }
    }

    public void Refresh()
    {
        Start();
    }
}
