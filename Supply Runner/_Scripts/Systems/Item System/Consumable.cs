using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    [SerializeField]
    int amount;

    public bool OnConsume()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().TakeDamage(-amount);
    }
}
