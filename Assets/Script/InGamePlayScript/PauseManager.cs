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
    public TotalManager TM;

    public GameObject pausePanel;
    public bool isPause = false;

    private void Start()
    {
        GM = InGamePlayManager.instance;
        TM = TotalManager.instance;
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
                GM.countdownTime = 3;
                StartCoroutine(GM.StartChart());
            }
        }
    }

    public void Continue()
    {
        Pause();
    }

    public void Restart()
    {
        SceneManager.LoadScene("InGamePlay");
    }

    public void TrackSelect()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        TM.SetTempScoreAndCombo(Mathf.RoundToInt(GM.tempScore), GM.tempHighestCombo);
        TM.SetAccuracyCount(GM.perfectCount, GM.greatCount, GM.goodCount, GM.missCount);
        
        Debug.Log("Game Finish!");

        SceneManager.LoadScene("Result");
    }
}
