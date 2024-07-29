using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Selector : MonoBehaviour
{
    public TextMeshProUGUI trackTitle;
    public TextMeshProUGUI trackArtist;
    public static string selectedTrack;
    public GameObject trackSelect;
    public Image backImage;
    public Image askPanel;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F10))
        {
            SceneManager.LoadScene("Editor");
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            AskPlay();
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleMenu");
        }
    }

    public void AskPlay()
    {
        trackSelect.gameObject.SetActive(false);
        backImage.gameObject.SetActive(true);
        var selected = askPanel.transform.GetChild(2).gameObject;
        EventSystem.current.SetSelectedGameObject(selected.gameObject);
    }

    public void Play()
    {
        selectedTrack = trackTitle.text+trackArtist.text+".json";
        Debug.Log(selectedTrack);
        SceneManager.LoadScene("InGamePlay");
    }

    public void Not()
    {
        Debug.Log("other track");
        backImage.gameObject.SetActive(false);
        trackSelect.gameObject.SetActive(true);
    }
}
