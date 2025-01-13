using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public RaycastWeapon WeaponFab;

    private PlayerInventory inv;

    private void OnTriggerEnter(Collider other) {
        // this code would equip the weapon. we comment it out so that it gets
        // picked up instead
        // ActiveWeapon activeWeapon = other.GetComponent<ActiveWeapon>();
        // if(activeWeapon){
        //     RaycastWeapon newWeapon = Instantiate(WeaponFab);
        //     activeWeapon.EquipWeapon(newWeapon);
        // }


        if (other.tag == "Player")
        {
            if (!inv) inv = other.GetComponentInChildren<PlayerInventory>();

            if (inv.GetAmount(WeaponFab.weaponName) < 1)
            {
                inv.AddItem(WeaponFab.weaponName, 1);
                inv.OnPickup(WeaponFab.weaponName);
                Destroy(gameObject);
            }
        }
    }

    void Spin(){
        transform.Rotate(0, 90 * Time.deltaTime, 0 );
    }

    void Bounce(){
        transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time * 2) * 0.01f, transform.position.z);

    }

    void Update(){
        Spin();
    }
}
