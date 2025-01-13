using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public PlayerInventory inv;
    public int playerLevel;
    public int playerXP;
    public int playerXPToNextLevel;
    public int playerXPToNextLevelMultiplier;
    public int playerXPToNextLevelAddition;
    public int playerXPToNextLevelTotal;
    public int playerXPToNextLevelTotalMultiplier;
    public int playerXPToNextLevelTotalAddition;
    public int playerXPToNextLevelTotalFinal;

    PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerLevel = 1;
        playerXP = 0;
        playerXPToNextLevel = 100;
        playerXPToNextLevelMultiplier = 2;
        playerXPToNextLevelAddition = 100;
        playerXPToNextLevelTotal = 100;
        playerXPToNextLevelTotalMultiplier = 200;
        playerXPToNextLevelTotalAddition = 100;
        playerXPToNextLevelTotalFinal = 100;
        ZombieKillXP = 10;
    }

    public int ZombieKillXP;
    public int ZombieKillCount;

    public int GetPlayerLevel()
    {
        return playerLevel;
    }

    public void ZombieKill(){
        ZombieKillCount++;
        playerXP += ZombieKillXP;
        
    }

    public void LevelUp()
    {
        if(playerXP >= playerXPToNextLevelTotalFinal){
            playerLevel++;
            playerXP = 0;
            playerXPToNextLevelTotalFinal = playerXPToNextLevelTotalFinal * playerXPToNextLevelMultiplier + playerXPToNextLevelAddition;
            playerHealth.IncreaseMaxHealth(10);
        }
        
    }

    void FixedUpdate()
    {
        LevelUp();
        if (ZombieKillCount >= 30 && inv.RemoveItem("quest_kill30", 1))
            inv.AddItem("quest_kill60", 1);
        if (ZombieKillCount >= 60 && inv.RemoveItem("quest_kill60", 1))
            inv.AddItem("quest_kill100", 1);
        if (ZombieKillCount >= 80 && inv.RemoveItem("quest_kill100", 1))
            inv.AddItem("crown", 1);
    }
    
}
