using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneChange : MonoBehaviour
{
    public Image Panel;
    public TextMeshProUGUI Text;
    private TextBlink textBlink;
    float time = 0f;
    float F_time = 1f;
    public string SceneToLoad;

    void Start()
    {
        textBlink = Text.GetComponent<TextBlink>();
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            Fade();
        }    
    }

    public void Fade()
    {
        if (textBlink != null)
        {
            textBlink.StopBlinking();
        }
        new WaitForSeconds(1f);
        StartCoroutine(FadeFlow());
    }

    IEnumerator FadeFlow()
    {
        Panel.gameObject.SetActive(true);
        Color panelAlpha = Panel.color;
        Color textAlpha = Text.color;

        while (panelAlpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            panelAlpha.a = Mathf.Lerp(0,1,time);
            textAlpha.a = Mathf.Lerp(1,0,time);
            Panel.color = panelAlpha;
            Text.color = textAlpha;
            yield return null;
        }
        SceneManager.LoadScene(SceneToLoad);
    }    
}
