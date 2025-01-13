using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickupDisplay : MonoBehaviour
{
    public PlayerInventory inv;
    public Image img;
    public TextMeshProUGUI exclaim;

    void Awake()
    {
        inv.RegisterPickupFn(DisplayPickup);
        Clear();
    }

    void DisplayPickup(string name)
    {
        ItemInfo info = Resources.Load<ItemInfo>("Items/" + name);

        img.sprite = info.icon;
        img.color = Color.white;
        exclaim.color = Color.red;

        Invoke("Clear", 4);
    }

    void Clear()
    {
        img.color = Color.clear;
        exclaim.color = Color.clear;
    }
}
