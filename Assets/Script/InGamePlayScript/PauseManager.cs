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
            Pause();
        }
    }

    void Pause()
    {
        isPause = !isPause;
        if(isPause && InGamePlayManager.instance.isPlaying)
        {
            pausePanel.gameObject.SetActive(true);
        }
        else if(!isPause && !InGamePlayManager.instance.isPlaying)
        {
            pausePanel.gameObject.SetActive(false);
        }
    }

    public void Continue()
    {
        
    }

    public void Restart()
    {

    }

    public void TrackSelect()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        SceneManager.LoadScene("TitleMenu");
    }
}
