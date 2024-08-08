using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Video;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public bool isPause = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            IsPause();
        }
    }

    public void IsPause()
    {
        isPause = !isPause;
        if(!isPause && InGamePlayManager.instance.isPlaying)
        {
            Time.timeScale = 0f;
            pausePanel.gameObject.SetActive(true);
            // var selected = pausePanel.transform.GetChild(2).gameObject;
            // EventSystem.current.SetSelectedGameObject(selected.gameObject);
        }
        else if(isPause && !InGamePlayManager.instance.isPlaying)
        {
            Time.timeScale = 1f;
            pausePanel.gameObject.SetActive(false);
        }
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }


}
