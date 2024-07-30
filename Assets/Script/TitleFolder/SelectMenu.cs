using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SelectMenu : MonoBehaviour
{
    public Canvas canvas;

    void Start()
    {
        var select = canvas.transform.GetChild(2).gameObject;
        EventSystem.current.SetSelectedGameObject(select.gameObject);
    }

    public void Play()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Editor()
    {
        SceneManager.LoadScene("Editor");
    }

    public void Setting()
    {
        SceneManager.LoadScene("Setting");
    }

    public void Quit()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
