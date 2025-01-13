using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public PlayerInventory inventory;

    public ItemSlots slots;
    
    public bool Craft(CraftingRecipe recipe)
    {
        if (inventory.RemoveItem(recipe.itemOne.stack))
        {
            if (inventory.RemoveItem(recipe.itemTwo.stack))
            {
                inventory.AddItem(recipe.result.stack);
                slots.LoadSlots();
                return true;
            }
            else
            {
                inventory.AddItem(recipe.itemOne.stack);
                return false;
            }
        }

        return false;
    }
}
