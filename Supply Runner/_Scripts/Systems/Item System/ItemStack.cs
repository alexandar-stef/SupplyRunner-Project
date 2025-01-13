using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack : MonoBehaviour
{
    public string itemID;
    public int amount;

    public ItemStack Intialize(string itemID, int amount)
    {
        this.itemID = itemID;
        this.amount = amount;

        return this;
    }

    public override string ToString()
    {
        return "ItemStack(" + amount + "x " + itemID + ")";
    }
}
