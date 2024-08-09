using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
        if(Input.GetKeyDown(KeyCode.F10))
        {
            SceneManager.LoadScene("Editor");
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("InGamePlay");
        }
    }
}
