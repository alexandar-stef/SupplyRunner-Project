using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    [Header("Internal Information")]
    public string itemID;
    public ItemCategory category;

    [Header("Display Information")]
    public string fullName;
    public Sprite icon;
    public string Description;

    [Header("Item Properties")]
    public int dropChance;
    public int dropAmountMax;
    public int dropAmountMin;



}
