using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SelectMenu : MonoBehaviour
{
    public Canvas canvas;
    public AudioSource audioSource;
    public AudioClip sceneClip;
    public AudioClip buttonClip;
    public AudioClip playClip;
    void Start()
    {
        TotalManager.instance.prevScene = "TitleMenu";
        var select = canvas.transform.GetChild(2).gameObject;
        EventSystem.current.SetSelectedGameObject(select.gameObject);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            audioSource.PlayOneShot(buttonClip);
        }
    }

    public void Play()
    {
        audioSource.PlayOneShot(playClip);
        SceneManager.LoadScene("MainMenu");
    }

    public void Editor()
    {
        audioSource.PlayOneShot(sceneClip);
        SceneManager.LoadScene("Editor");
    }

    public void Setting()
    {
        audioSource.PlayOneShot(sceneClip);
        SceneManager.LoadScene("Setting");
    }

    public void Quit()
    {
        audioSource.PlayOneShot(sceneClip);
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
