using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    public TextMeshProUGUI textObj;

    float alpha = 0;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        textObj.color = new Color(
            textObj.color.r,
            textObj.color.g,
            textObj.color.b,
            alpha
        );
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (alpha < 1)
        {
            alpha += Time.fixedDeltaTime * 0.3f;
        }
        else
        {
            alpha = 1.0f;
        }

        textObj.color = new Color(
            textObj.color.r,
            textObj.color.g,
            textObj.color.b,
            alpha
        );

        timer += Time.deltaTime;

        if (timer > 3)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
