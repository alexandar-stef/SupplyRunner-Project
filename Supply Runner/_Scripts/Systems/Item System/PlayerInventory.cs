using System;
using System.Collections.Generic;
using UnityEngine;

public delegate void ItemPickupFn(string itemID);

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    ScreenManager screenManager;

    [SerializeField]
    GameObject stackParent;

    public GameObject hud, invScreen, stackPrefab, dropPrefab, dropParent;
    List<ItemStack> contents;

    public bool open;

    bool initialized = false;

    ItemPickupFn onItemPickup;

    void Start()
    {
        if (!initialized) {
            contents = new List<ItemStack>();
            open = false;
            initialized = true;
        }
    }

    public void Create()
    {
        Start();
    }

    public int GetAmmo(bool isPrimary)
    {
        string ammoName = (isPrimary ? "primary" : "sidearm") + "_ammo";

        foreach (ItemStack stack in contents)
        {
            if (stack.itemID == ammoName) return stack.amount;
        }

        return 0;
    }

    public int GetAmount(string itemID)
    {
        foreach (ItemStack stack in contents)
        {
            if (stack.itemID == itemID)
            {
                return stack.amount;
            }
        }

        return 0;
    }

    public void AddItem(string itemID, int amount)
    {
        ItemStack newStack = stackParent.AddComponent<ItemStack>().Intialize(itemID, amount);
        AddItem (newStack);
    }

    public void AddItem(ItemStack item)
    {  
        foreach (ItemStack slot in contents)
        {
            if (slot.itemID == item.itemID)
            {
                slot.amount += item.amount;
                return;
            }
        }

        ItemStack newStack = Instantiate(stackPrefab, transform).GetComponent<ItemStack>();

        contents.Add(newStack.Intialize(item.itemID, item.amount));
        OnPickup(item.itemID);
    }

    public bool DropItem(string id, int amount)
    {
        if (RemoveItem(id, amount))
        {
            ItemPickup drop = Instantiate(dropPrefab, dropParent.transform).GetComponent<ItemPickup>();
            
            drop.transform.position = transform.position + (transform.forward * (2 + Math.Min(amount * 0.075f / 2.0f, 1.0f)));
            drop.Create(id, amount);

            return true;
        }

        return false;
    }

    public bool RemoveItem(ItemStack stack)
    {
        return RemoveItem(stack.itemID, stack.amount);
    }

    public bool RemoveItem(string id, int amount)
    {
        foreach (ItemStack stack in contents)
        {
            if (stack.itemID == id)
            {
                if (stack.amount > amount)
                    stack.amount -= amount;
                else if (stack.amount == amount)
                    contents.Remove(stack);
                else continue;

                return true;
            }
        }

        return false;
    }

    public string SerializeList()
    {
        string result = "Inventory: {\n";

        foreach (ItemStack slot in contents)
        {
            result += "\t" + slot.itemID + " : " + slot.amount + "\n";
        }

        return result + "}";
    }

    void OnTriggerEnter(Collider other)
    {
        Transform otherParent = other.transform.parent;
        if (otherParent != null 
        && otherParent.tag.Equals("ItemPickup"))
        {
            AddItem(otherParent.gameObject.GetComponent<ItemStack>());
            Destroy(otherParent.gameObject);
        }
    }

    public List<ItemStack> GetStacks()
    {
        return contents;
    }

    public void RegisterPickupFn(ItemPickupFn fn)
    {
        this.onItemPickup = fn;
    }

    public void OnPickup(string id)
    {
        onItemPickup(id);
    }
}
