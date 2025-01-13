using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public delegate void MouseEnterFn(string id, int amt);
public delegate void MouseExitFn();

public delegate void ClickFn(InvSlot slotObj);

public class InvSlot : MonoBehaviour
{
    public TextMeshProUGUI amountText;
    public Image slotIconComponent;

    [SerializeField]
    ItemStack fallback;

    [NonSerialized]
    public ItemStack stack;

    ItemInfo item;

    MouseEnterFn onEnter;
    MouseExitFn onExit;
    ClickFn onClick;

    void Start()
    {
        if (!hasItem()) 
        {
            if (fallback != null) stack = fallback;
            else return;
        }

        item = Resources.Load<ItemInfo>("Items/" + stack.itemID);
        LoadInfo();
    }

    public void Refresh()
    {
        Start();
    }

    public void Reset()
    {
        amountText.text = "";
        slotIconComponent.color = Color.clear;
    }

    // Unity overrides the "== null" operator so even if stack wasnt null
    // and had an itemID and amount "stack == null" returns true after picking up
    // an item. So we have this instead
    bool hasItem()
    {
        try 
        {
            return stack.itemID + stack.amount != "$";
        }
        catch (Exception)
        {
            return false;
        }
    }

    void OnEnable()
    {
        Start();
    }

    void LoadInfo()
    {
        amountText.text = 
            stack.amount == 0 || item.category == ItemCategory.quest || item.category == ItemCategory.equipment
                ? "" 
                : stack.amount.ToString()
        ;

        slotIconComponent.sprite = item.icon;
        slotIconComponent.color = Color.white;

        if (stack.itemID == "dummy") slotIconComponent.color = Color.clear;
    }

    public void RegisterMouseEnter(MouseEnterFn fn)
    {
        onEnter = fn;
    }

    public void RegisterMouseExit(MouseExitFn fn)
    {
        onExit = fn;
    }

    public void RegisterClick(ClickFn fn)
    {
        onClick = fn;
    }

    public void OnClick()
    {
        if (hasItem())
        {   
            onClick(this);
        }
    }

    public void OnMouseEnter()
    {  
        if (hasItem())
        {
            onEnter(stack.itemID, stack.amount);
        }
    }

    public void OnMouseExit()
    {
        if (hasItem())
        {
            onExit();
        }
    }
}
