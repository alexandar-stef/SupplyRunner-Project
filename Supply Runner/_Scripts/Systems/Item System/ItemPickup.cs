using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemStack stack;

    void Start()
    {
        float size = SizeFunction();

        transform.localScale = new Vector3(size, size, size);
    }

    public void Create(string id, int amount)
    {
        stack.itemID = id;
        stack.amount = amount;

        Start();
    }

    float SizeFunction()
    {
        return Math.Min((stack.amount * 0.017f) + 0.25f, 0.75f);
    }
}
