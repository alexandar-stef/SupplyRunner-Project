using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipe : MonoBehaviour
{
    public CraftingManager manager;
    public InvSlot result, itemOne, itemTwo;

    public void OnClick()
    {
        manager.Craft(this);
    }
}
