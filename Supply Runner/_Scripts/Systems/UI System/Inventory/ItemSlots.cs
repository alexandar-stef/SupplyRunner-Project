using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlots : MonoBehaviour
{
    public PlayerInventory inv;
    public string defaultTab;

    [NonSerialized]
    public string tab;

    [SerializeField]
    ItemDetails details;

    [SerializeField]
    ItemContextMenu ctxMenu;

    [SerializeField]
    InvSlot equippedWeaponSlot;

    List<CraftingRecipe> recipes;

    List<InvSlot> 
        slots, 
        recipeSlots,
        weaponSlots,
        ammoSlots
    ;

    List<ItemStack> filteredStacks;
    List<ItemStack> stacks;

    string[] tabStrs;

    void Awake()
    {
        tabStrs = new string[5]
        {
            "Crafting",
            "Equip",
            "Consumables",
            "Quest",
            "Ammo"
        };

        recipes = new List<CraftingRecipe>();
        slots = new List<InvSlot>();
        stacks = new List<ItemStack>();
        filteredStacks = new List<ItemStack>();

        recipeSlots = new List<InvSlot>();

        weaponSlots = new List<InvSlot>();
        ammoSlots = new List<InvSlot>();

        if (details != null) details.gameObject.SetActive(false);
        tab = defaultTab;
        LoadSlots();
    }

    void OnEnable()
    {
        LoadSlots();
    }

    public void LoadSlots()
    {
        slots.Clear();
        stacks.Clear();
        filteredStacks.Clear();

        recipeSlots.Clear();

        weaponSlots.Clear();
        ammoSlots.Clear();

        try
        {
            ListStuff.ListCopy(stacks, inv.GetStacks());
        }
        catch (Exception)
        {
            return;
        }

        foreach (Transform child in transform) // iterate thru inventory screens
        {
            if (child.name.Equals(tab + "Screen")) // if the screen is the selected one
            {
                foreach (Transform slot in child.GetChild(0).transform) // iterate through screen's 1st child (parent of slots)
                {
                    slots.Add(slot.GetComponent<InvSlot>());
                }

                if (tab == "Crafting") // special case for crafting (add crafting slots)
                {
                    foreach (Transform slot in child.GetChild(1).transform) // iterate through screen's 2nd child (parent of crafting slots)
                    {
                        recipeSlots.Add(slot.GetComponent<CraftingRecipe>().result);
                        recipeSlots.Add(slot.GetComponent<CraftingRecipe>().itemOne);
                        recipeSlots.Add(slot.GetComponent<CraftingRecipe>().itemTwo);
                    }
                    
                    // register events:
                    // - hover: show details
                    // - click: craft
                    foreach (InvSlot slot in recipeSlots)
                    {
                        slot.RegisterMouseEnter((itemID, amount) =>
                        {
                            details.LoadDetails(itemID, amount);
                            details.gameObject.SetActive(true);
                        });

                        slot.RegisterMouseExit(() =>
                        {
                            details.gameObject.SetActive(false);
                        });

                        slot.RegisterClick((self) => {
                            // parent of button is the crafting recipe object itself
                            slot.transform.parent.GetComponent<CraftingRecipe>().OnClick();
                        });
                    }
                }
                else if (tab == "Equip") // special case for equipment (add weapon and ammo slots)
                {
                    foreach (Transform slot in child.GetChild(1).transform) // GetChild(1) is ammo slots;
                    {
                        ammoSlots.Add(slot.GetComponent<InvSlot>());
                    }

                    int i = 0;
                    // iterate through stacks
                    foreach (ItemStack stack in stacks)
                    {
                        ItemInfo info = Resources.Load<ItemInfo>("Items/" + stack.itemID);

                        // filters the stacks => only relevant item stacks are added to filteredStacks
                        if (tabStrs[(int) info.category] == "Ammo")
                        {
                            ammoSlots[i].stack = stack;
                            i++;
                        }
                    }

                    // register events:
                    // - hover: show details
                    foreach (InvSlot slot in ammoSlots)
                    {
                        slot.RegisterMouseEnter((itemID, amount) =>
                        {
                            details.LoadDetails(itemID, amount);
                            details.gameObject.SetActive(true);
                        });

                        slot.RegisterMouseExit(() =>
                        {
                            details.gameObject.SetActive(false);
                        });

                        slot.RegisterClick((self) => {
                            // Do nothing
                        });
                    }
                }
            }
        }

        // iterate through stacks
        foreach (ItemStack stack in stacks)
        {
            ItemInfo info = Resources.Load<ItemInfo>("Items/" + stack.itemID);

            // filters the stacks => only relevant item stacks are added to filteredStacks
            // tabStrs[(int) info.category] is the tab that corresponds to info.category
            if (tabStrs[(int) info.category].Equals(tab))
            {
                filteredStacks.Add(stack);
            }
        }

        int requiredSlots = Math.Min(slots.Count, filteredStacks.Count);

        for (int i = 0; i < requiredSlots; i++)
        {
            slots[i].stack = filteredStacks[i];
            slots[i].Refresh();

            slots[i].RegisterMouseEnter((itemID, amount) =>
            {
                details.LoadDetails(itemID, amount);
                details.gameObject.SetActive(true);
            });

            slots[i].RegisterMouseExit(() =>
            {
                details.gameObject.SetActive(false);
            });

            slots[i].RegisterClick(CreateSubMenu);
        }

        // Refreshes each slot
        for (int i = requiredSlots; i < slots.Count; i++)
        {
            slots[i].Reset();
        }

        // register events for weaequippedWeaponSlot
        equippedWeaponSlot.RegisterMouseEnter((_itemID, _amount) => {});

        equippedWeaponSlot.RegisterMouseExit(() =>
        {
            details.gameObject.SetActive(false);
        });

        equippedWeaponSlot.RegisterClick((_click) => {});
    }

    void CreateSubMenu(InvSlot slot)
    {
        details.gameObject.SetActive(false);

        ctxMenu.Create();
        ctxMenu.BindItemSlot(slot, tab);

        ctxMenu.gameObject.SetActive(true);
    }

    public void EquipWeapon(string id)
    {
        equippedWeaponSlot.stack.Intialize(id, 1);
        equippedWeaponSlot.Refresh();

        equippedWeaponSlot.RegisterMouseEnter((id, amt) => {
            details.LoadDetails(id, amt);
            details.gameObject.SetActive(true);
        });
    }

    public void CleanUp()
    {
        details.gameObject.SetActive(false);
        ctxMenu.gameObject.SetActive(false);
    }
}