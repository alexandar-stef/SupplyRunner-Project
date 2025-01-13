using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public ZombieMovement zombieMovement;

    public bool isHead;
    // Start is called before the first frame update
    public void TakeDamage(float damage)
    {   
        if(!isHead){
            zombieMovement.TakeDamage(damage);
        }
        else{
            zombieMovement.KillZombie();
        }
        
    }
}
