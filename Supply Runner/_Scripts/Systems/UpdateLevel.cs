using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateLevel : MonoBehaviour
{

    PlayerLevel playerlevel;
    TMP_Text currentLevel;


    void Start()
    {
        currentLevel = GameObject.Find("CurrentLevel").GetComponent<TMP_Text>();
        playerlevel = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLevel>();
    }

    
    // Update is called once per frame
    void FixedUpdate()
    { 
        currentLevel.text = "Level " + playerlevel.GetPlayerLevel() + " - " + playerlevel.playerXP + "/" + playerlevel.playerXPToNextLevelTotalFinal + " XP";
        // int level = playerlevel.GetPlayerLevel();

        // Debug.Log("Player Level: " + level);
        
    }
}
