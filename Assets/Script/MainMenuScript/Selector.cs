using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEditor.Timeline;
using Unity.VisualScripting;

public class Selector : MonoBehaviour
{
    public TotalManager TM;
    
    public SpeedSelect speedSelect;

    public TextMeshProUGUI trackTitle;
    public TextMeshProUGUI trackArtist;
    public static string selectedTrackTitle;
    public static string selectedTrackArtist;
    public static string selectedTrack;
    public GameObject trackSelect;
    public GameObject selector;
    public GameObject speedSelector;
    public Image backImage;
    public Image askPanel;

    private void Start()
    {
        TM = TotalManager.instance;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F10))
        {
            SceneManager.LoadScene("Editor");
        }
        else if(Input.GetKeyDown(KeyCode.Return))
        {
            trackSelect.gameObject.SetActive(false);
            selector.gameObject.SetActive(false);
            AskPlay();
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleMenu");
        }
    }

    public void AskPlay()
    {
        backImage.gameObject.SetActive(true);
        speedSelector.gameObject.SetActive(true);
        var selected = askPanel.transform.GetChild(2).gameObject;
        EventSystem.current.SetSelectedGameObject(selected.gameObject);
    }

    public void Play()
    {
        selectedTrackTitle = trackTitle.text;
        selectedTrackArtist = trackArtist.text;
        selectedTrack = trackTitle.text+trackArtist.text+".json";
        
        TM.SetTempSong(selectedTrackTitle, selectedTrackArtist);
        
        Debug.Log(selectedTrack);
        Debug.Log(SpeedSelect.finalSpeed);

        TotalManager.instance.userChartSpeed = SpeedSelect.finalSpeed;
        SceneManager.LoadScene("InGamePlay");
    }

    public void Not()
    {
        Debug.Log("other track");
        speedSelect.ResetSpeed();

        backImage.gameObject.SetActive(false);
        speedSelector.gameObject.SetActive(false);
        
        trackSelect.gameObject.SetActive(true);
        selector.gameObject.SetActive(true);
    }
}
