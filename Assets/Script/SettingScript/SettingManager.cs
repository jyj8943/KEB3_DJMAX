using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SettingManager : MonoBehaviour
{
    public TotalManager TM;
    public GameObject startSetting;

    private void Awake()
    {
        TM = TotalManager.instance;
    }

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(startSetting.gameObject);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(TM.prevScene);
        }
    }
}
