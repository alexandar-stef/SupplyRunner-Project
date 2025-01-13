using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreenMode
{
    fps,
    inventory,
    pause
}

public class ScreenManager : MonoBehaviour
{
    public ScreenMode mode;
    public UIManager uiManager;

    void Start()
    {
        uiManager.Initialize();
        SwitchTo(mode);
    }

    public void SwitchTo(ScreenMode newMode)
    {
        switch (newMode)
        {
            case ScreenMode.fps:
                uiManager.SwitchTo(uiManager.hud);
                break;
            case ScreenMode.inventory:
                uiManager.SwitchTo(uiManager.inv);
                break;
            case ScreenMode.pause:
                uiManager.SwitchTo(uiManager.pause);
                break;
        }

        mode = newMode;
    }

    public void OnPlayerDeath()
    {
        SwitchTo(ScreenMode.pause);
        uiManager.OnPlayerDeath();
    }
}
