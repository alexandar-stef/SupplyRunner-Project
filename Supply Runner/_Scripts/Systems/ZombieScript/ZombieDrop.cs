using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDrop : MonoBehaviour
{
    public List<ItemInfo> AmmoDrop = new List<ItemInfo>();
    public List<ItemInfo> ConsumableDrop = new List<ItemInfo>();
    public GameObject dropObject;
    [Header("Ammo Drop")]
    public int[] AmmoDropChance;
    public int[] AmmoDropAmount;
    public int[] AmmoDropAmountMin;
    public int[] AmmoDropAmountMax;

    [Header("Consumable Drop")]
    public int[] ConsumableDropChance;
    public int[] ConsumableDropAmount;
    public int[] ConsumableDropAmountMin;
    public int[] ConsumableDropAmountMax;




    public Transform ZombiePos;

    public GameObject dropParent;

    private ItemInfo itemInfo;
    private ItemPickup itemPickup;
    private ItemStack itemStack;

    private string itemID;

    
    // Start is called before the first frame update
    void Start()
    {

        dropParent = GameObject.Find("FloorItems");
        SetupArrays();
        SetDropChance();
        
    }

    void Update()
    {   
        
    

        
    }
    void SetupArrays()
    {
        AmmoDropChance = new int[AmmoDrop.Count];
        AmmoDropAmount = new int[AmmoDrop.Count];
        AmmoDropAmountMin = new int[AmmoDrop.Count];
        AmmoDropAmountMax = new int[AmmoDrop.Count];

        ConsumableDropChance = new int[ConsumableDrop.Count];
        ConsumableDropAmount = new int[ConsumableDrop.Count];
        ConsumableDropAmountMin = new int[ConsumableDrop.Count];
        ConsumableDropAmountMax = new int[ConsumableDrop.Count];
    }

    void SetDropChance()
    {
        for(int i = 0; i < AmmoDrop.Count; i++)
        {
            AmmoDropChance[i] = AmmoDrop[i].dropChance;
            AmmoDropAmountMin[i] = AmmoDrop[i].dropAmountMin;
            AmmoDropAmountMax[i] = AmmoDrop[i].dropAmountMax;
            
        }

        for(int i = 0; i < ConsumableDrop.Count; i++)
        {
            ConsumableDropChance[i] = ConsumableDrop[i].dropChance;
            ConsumableDropAmountMin[i] = ConsumableDrop[i].dropAmountMin;
            ConsumableDropAmountMax[i] = ConsumableDrop[i].dropAmountMax;
        }
    }


    public void Print(){
        Debug.Log("Test");
    }

    public void Drop()
    {
        DropAmmo();
        DropItem(); 
    }

    void DropAmmo(){
        for(int i = 0; i < AmmoDrop.Count; i++)
        {
            int amount = Random.Range(AmmoDropAmountMin[i], AmmoDropAmountMax[i]);
            int random = Random.Range(0, 100);
            if(random <= AmmoDropChance[i])
            {
                
                GameObject dropAmmo = Instantiate(dropObject, dropParent.transform);
            
                dropAmmo.transform.position = ZombiePos.position;
                dropAmmo.GetComponent<ItemPickup>().Create(AmmoDrop[i].itemID, amount);

            }
        }
    }
    
    

    void DropItem(){
        for(int i = 0; i < ConsumableDrop.Count; i++)
        {
            int amount = Random.Range(ConsumableDropAmountMin[i], ConsumableDropAmountMax[i]);
            int random = Random.Range(0, 100);
            if(random <= ConsumableDropChance[i])
            {
                GameObject dropConsumable = Instantiate(dropObject, dropParent.transform);
            
                dropConsumable.transform.position = ZombiePos.position;
                dropConsumable.GetComponent<ItemPickup>().Create(ConsumableDrop[i].itemID, amount);

            }
        }
    }

    
}
