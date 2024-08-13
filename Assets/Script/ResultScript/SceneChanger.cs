using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public Image maxCombo;
    public GameObject newRecord;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
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
