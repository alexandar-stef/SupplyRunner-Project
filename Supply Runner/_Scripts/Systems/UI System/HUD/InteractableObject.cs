using UnityEngine;
using System.Text.RegularExpressions;

public class InteractableObject : MonoBehaviour
{
    public string interactionPrompt; // This will be the text displayed.

    private string ItemName;

    void Start()
    {
        if(GetComponent<ItemStack>() != null){
            interactionPrompt = GetComponent<ItemStack>().itemID;
            ItemName = Resources.Load<ItemInfo>("Items/" + interactionPrompt).fullName;
            return;
        }
        else if(GetComponentInChildren<RaycastWeapon>() != null){
            interactionPrompt = GetComponentInChildren<RaycastWeapon>().weaponName;
            ItemName = TransformText(interactionPrompt);
            return;
        }
        else{
            ItemName = TransformText(interactionPrompt);
        }
        
    }

    // This function could be called by the raycasting script to display the prompt.
    public string GetInteractionPrompt()
    {
        return ItemName;
    }

    static string TransformText(string input)
    {
        // Transform the input using a regular expression
        string result = Regex.Replace(input, @"\b\w", match => match.Value.ToUpper(), RegexOptions.IgnoreCase);
        result = Regex.Replace(result, @"_([a-zA-Z])", match => " " + match.Groups[1].Value.ToUpper());

        return result;
    }


}

