using UnityEngine;
using TMPro;

public class ZoneManager : MonoBehaviour
{
    TMP_Text ZoneMessageText;
    string message;

    void Start()
    {
        ZoneMessageText = GameObject.Find("ZoneMessageText").GetComponent<TMP_Text>();
        ZoneMessageText.enabled = true;
        message = "Settlement";
    }

    void Update()
    {
        ZoneMessageText.text = message;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"OnTriggerEnter called with: {other.gameObject.name}");
        switch (other.gameObject.tag)
        {
            case "SettlementZone":
                message = "Pick up Pistol and Items";
                //Debug.Log($"OnTriggerEnter called with: {other.gameObject.name}");
                break;
            case "OfficeBuildingZone":
                message = "Grab the M3 Shotgun behind the elevator";
                // Debug.Log($"OnTriggerEnter called with: {other.gameObject.name}");
                break;
            case "SupermarketZone":
                message = "Grab the MP5 in Storage Room";
                break;
            case "ParkingGarageZone":
                message = "Grab the Scar On the Roof";
     
                break;
            default:
                message = "";
                
                break;
        }
        ZoneMessageText.enabled = true;

    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Untagged":
                message = "Pick up Pistol and Items";
                break;
            case "SettlementZone":
                message = "Go to Office Building";
                //Debug.Log($"OnTriggerEnter called with: {other.gameObject.name}");
                break;
            case "OfficeBuildingZone":
                message = "Go to Supermarket";
                //Debug.Log($"OnTriggerEnter called with: {other.gameObject.name}");
                break;
            case "SupermarketZone":
                message = "Go to Parking Garage";
                break;
            case "ParkingGarageZone":
                message = "";
     
                break;
            default:
                message = "";
                ZoneMessageText.enabled = false;
                break;
        }


            
    }

}