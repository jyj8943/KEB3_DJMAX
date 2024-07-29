using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SelectMenu : MonoBehaviour
{
    private const float LoadTime = 4f;
    float time = 0f;
    float Ftime = 1f;
    public Image Panel;
    public Canvas cavas;

    void Start()
    {
        StartCoroutine(FadeIn());
        var select = cavas.transform.GetChild(1).gameObject;
        EventSystem.current.SetSelectedGameObject(select.gameObject);
    }

    IEnumerator FadeIn()
    {
        Color alpha = Panel.color;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / Ftime;
            alpha.a = Mathf.Lerp(1,0,time);
            Panel.color = alpha;
            yield return null;
        }
        Panel.gameObject.SetActive(false);
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
