using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Video;
using UnityEditor.PackageManager;

public class PauseManager : MonoBehaviour
{
    public InGamePlayManager GM;
    public GameObject pausePanel;
    public bool isPause = false;

    private void Start()
    {
        GM = InGamePlayManager.instance;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    void Pause()
    {
        if(GM.countdownTime == 0)
        {
            isPause = !isPause;
            GM.isPlaying = !GM.isPlaying;

            if(isPause && !GM.isPlaying)
            {
                pausePanel.gameObject.SetActive(true);
                var selected = pausePanel.transform.GetChild(1).gameObject;
                EventSystem.current.SetSelectedGameObject(selected.gameObject);
            }
            else if(!isPause && GM.isPlaying)
            {
                pausePanel.gameObject.SetActive(false);
            }
        }
    }

    public void Continue()
    {
        Pause();
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
        SceneManager.LoadScene("Result");
    }
}
