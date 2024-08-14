using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SettingManager : MonoBehaviour
{
    public GameObject startSetting;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(startSetting.gameObject);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("MainMenu");
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleMenu");
        }
    }
}
