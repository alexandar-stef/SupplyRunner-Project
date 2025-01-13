using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("Map-2.0 1");
    }
}
