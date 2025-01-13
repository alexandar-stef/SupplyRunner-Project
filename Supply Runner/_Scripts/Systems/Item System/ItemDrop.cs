using System.Collections;
using System.Collections.Generic;

public class ItemDrop : PoolableObject
{
    public ItemStack ItemStack;
    public ItemPickup Pickup;

    void Start()
    {
        ItemStack = GetComponent<ItemStack>();
        Pickup = GetComponent<ItemPickup>();
    }

    
}