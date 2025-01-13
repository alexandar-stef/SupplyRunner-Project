using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void Play()
    {
        // TODO: change this to the actual level
        SceneManager.LoadScene("Loading");
    }

    public void Quit()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }
}
