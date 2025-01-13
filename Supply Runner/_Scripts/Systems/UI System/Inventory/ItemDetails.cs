using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemDetails : MonoBehaviour
{
    [SerializeField]
    RectTransform pos;

    [SerializeField]
    TextMeshProUGUI title, description, amount, category;

    void Update()
    {
        pos.position = Input.mousePosition;
        pos.Translate(-60, -20, 0);
    }

    public void LoadDetails(string itemID, int amount)
    {
        Update();
        
        ItemInfo info = Resources.Load<ItemInfo>("Items/" + itemID);

        title.text = info.fullName;
        description.text = info.Description;
        this.amount.text = amount.ToString();

        switch (info.category)
        {
            case ItemCategory.material:
                category.text = "Material";
                break;
            case ItemCategory.equipment:
                category.text = "Equipment";
                break;
            case ItemCategory.consumable:
                category.text = "Consumable";
                break;
            case ItemCategory.quest:
                category.text = "Quest";
                break;
            case ItemCategory.ammo:
                category.text = "Ammunition";
                break;
        }
    }
}
