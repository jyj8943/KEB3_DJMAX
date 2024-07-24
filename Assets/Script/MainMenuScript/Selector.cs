using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Selector : MonoBehaviour
{
    public TextMeshProUGUI trackTitle;
    public TextMeshProUGUI trackArtist;
    public static string selectedTrack;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F10))
        {
            SceneManager.LoadScene("Editor");
        }
        else if(Input.GetKeyDown(KeyCode.Return))
        {
            selectedTrack = trackTitle.text+trackArtist.text+".json";
            Debug.Log(selectedTrack);
            // SceneManager.LoadScene("InGamePlay");
        }
    }
}
