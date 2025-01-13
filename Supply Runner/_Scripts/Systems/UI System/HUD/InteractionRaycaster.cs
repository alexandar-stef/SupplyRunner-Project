using UnityEngine;
using TMPro;
using System;

public class InteractionRaycaster : MonoBehaviour
{
    public Camera playerCamera;
    public TextMeshProUGUI _interactionText;
    public float interactionDistance = 3f; // Adjust as necessary for game

    InteractableObject interactableObject;

    bool hit;

    void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _interactionText = GameObject.FindGameObjectWithTag("Interaction").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        RaycastHit hit;
        // Cast a ray from the center of the screen
        Ray ray = playerCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

        // Check if we hit an InteractableObject
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.rigidbody != null)
            {
                interactableObject = hit.rigidbody.GetComponent<InteractableObject>();
            }
            else if (hit.collider != null)
            {
                interactableObject = hit.collider.GetComponent<InteractableObject>();
            }

            try
            {
                _interactionText.text = interactableObject.GetInteractionPrompt();
                _interactionText.enabled = true;
            }
            catch (Exception)
            {
                _interactionText.enabled = false; // No object detected, hide the text
                return;
            }
        }
        else
        {
            _interactionText.enabled = false; // No object detected, hide the text
        }
    }

    
}

