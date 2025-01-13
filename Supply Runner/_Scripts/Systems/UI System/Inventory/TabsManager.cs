using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabsManager : MonoBehaviour
{
    public ItemSlots slots;
    public List<GameObject> screens;

    List<Toggle> toggles;

    void Start()
    {
        toggles = new List<Toggle>();

        foreach (Transform child in transform)
        {
            toggles.Add(child.gameObject.GetComponent<Toggle>());
        }

        foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener((val) => {
                if (val) 
                {
                    OpenTab(toggle.name);
                }
            });
        }
    }

    public void OpenTab(string tabName)
    {
        ToggleAllExcept(tabName);
        CloseAllExcept(tabName);

        slots.tab = tabName;
        slots.LoadSlots();
    }

    public void ToggleAllExcept(string tabName)
    {
        foreach (Toggle toggle in toggles)
        {
            if (!toggle.name.Equals(tabName))
            {
                toggle.isOn = false;
            }
        }
    }

    void CloseAllExcept(string tabName)
    {
        foreach (GameObject screen in screens)
        {
            if (screen.name.Equals(tabName + "Screen"))
            {
                screen.SetActive(true);
            }
            else
            {
                screen.SetActive(false);
            }
        }
    }
}
