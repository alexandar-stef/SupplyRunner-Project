using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public ScreenManager manager;

    public GameObject
        hud,
        inv,
        pause
    ;

    [SerializeField]
    GameObject deathScreen;

    [SerializeField]
    Image deathImage;

    [SerializeField]
    TextMeshProUGUI menuText;

    GameObject[] screens;

    bool isAnimating = false;
    bool initialized = false;
    Color color;

    void Start()
    {
        if (!initialized)
        {
            color = Color.white;
            color.a = 0;
            screens = new GameObject[3] {
                hud,
                inv,
                pause
            };

            initialized = true;
        }
    }

    public void ToFPS()
    {
        manager.SwitchTo(ScreenMode.fps);
    }

    public void SwitchTo(GameObject target)
    {
        foreach (GameObject screen in screens)
        {
            if (screen == target) screen.SetActive(true);
            else screen.SetActive(false);
        }

        if (target == hud)
        {
            Cursor.visible = false;  
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        if (target != inv)
        {
            inv.GetComponent<ItemSlots>().CleanUp();
        }

        if (target == pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void OnPlayerDeath()
    {
        color.a = 0;
        isAnimating = true;

        pause.SetActive(false);
        deathScreen.SetActive(true);
    }

    void FixedUpdate()
    {
        if (isAnimating)
        {
            if (color.a < 0.95f) color.a += (0.95f / 3.0f) * Time.fixedDeltaTime;
            else {
                color.a = 0.95f;
                isAnimating = false;
            }

            deathImage.color = color;
            menuText.color = color;
        }
    }

    public void Initialize()
    {
        Start();
        initialized = true;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
