using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public Image maxCombo;
    public GameObject newRecord;

    public AudioSource audioSource;
    public AudioClip sceneClip;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            audioSource.PlayOneShot(sceneClip);
            SceneManager.LoadScene("MainMenu");
        }
        if(Input.GetKeyDown(KeyCode.F10))
        {
            audioSource.PlayOneShot(sceneClip);
            SceneManager.LoadScene("Editor");
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            audioSource.PlayOneShot(sceneClip);
            SceneManager.LoadScene("InGamePlay");
        }
    }
}
