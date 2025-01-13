using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContextMenu : MonoBehaviour
{
    public RectTransform pos;
    public ItemSlots inventorySlots;
    //public string mode;

    public GameObject 
        dropOne,
        dropAll,
        equip,
        use
    ;

    ActiveWeapon activeWeapon;

    ItemStack stack;

    List<GameObject> opts;

    public void Start()
    {
        activeWeapon = inventorySlots.inv.transform.parent.GetComponentInChildren<ActiveWeapon>();
        Debug.Log(activeWeapon);
    }

    public void Create()
    {
        opts = new List<GameObject>();
    }

    public void BindItemSlot(InvSlot slot, string mode)
    {
        stack = slot.stack;

        pos.position = Input.mousePosition;
        pos.Translate(10, 10, 0);

        ResetOptions();
        SetUpOptions(mode);
    }
    
    void ResetOptions()
    {
        opts.Clear();

        dropOne.SetActive(false);
        dropAll.SetActive(false);
        equip.SetActive(false);
        use.SetActive(false);
    }

    void SetUpOptions(string mode)
    {
        switch (mode)
        {
            case "Crafting":
                opts.Add(dropOne);
                opts.Add(dropAll);
                break;
            case "Equip":
                opts.Add(dropAll);
                opts.Add(equip);
                break;
            case "Consumables":
                opts.Add(dropOne);
                opts.Add(dropAll);
                opts.Add(use);
                break;
            case "Quest":
                opts.Add(dropAll);
                break;
            default:
                throw new System.Exception("INVALID MODE. MODE PROVIDED: " + mode);
        }

        pos.sizeDelta = new Vector2(200, Height());

        int newY = -30;
        foreach (GameObject opt in opts)
        {
            opt.transform.localPosition = new Vector3(-pos.rect.width / 2, newY, 0);
            newY -= 40;

            opt.SetActive(true);
        }
    }

    int Height()
    {
        int buttonHeight = 30;
        int paddingInner = 10;
        int paddingOuter = 30;
        int rows = opts.Count;

        return (buttonHeight * rows) + (paddingInner * (rows - 1)) + (paddingOuter * 2);
    }

    public void DropOne()
    {
        inventorySlots.inv.DropItem(stack.itemID, 1);
        inventorySlots.LoadSlots();
    }

    public void DropAll()
    {
        inventorySlots.inv.DropItem(stack.itemID, stack.amount);
        inventorySlots.LoadSlots();
    }

    public void Equip()
    {
        // absolute monstrosity of a line of code. may god have mercy on me for writing this.
        activeWeapon.EquipWeapon(Instantiate(Resources.Load<PrefabPointer>("Guns/" + stack.itemID).prefab.GetComponent<RaycastWeapon>()));
        inventorySlots.EquipWeapon(stack.itemID);
    }

    public void Use()
    {
        // try
        // {
            Consumable info = Resources.Load<Consumable>("Items/" + stack.itemID);
            if (info.OnConsume())
            {
                inventorySlots.inv.RemoveItem(stack.itemID, 1);
                inventorySlots.LoadSlots();
            }

        // }
        // catch (Exception)
        // {
        //     Debug.Log("Attempting to consume a non-consumable item!");
        // }
    }

    public void OnMouseExit()
    {
        gameObject.SetActive(false);
    }
}
