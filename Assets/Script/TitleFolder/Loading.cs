using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    private const float LoadTime = 4f;
    float time = 0f;
    float Ftime = 1f;
    public Image Panel;
    public string SceneToLoad;

    private void Start()
    {
        StartCoroutine(FadeIn());
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneToLoad);
        operation.allowSceneActivation = false;

        float startTime = Time.time;

        while(!operation.isDone)
        {
            yield return null;

            if (operation.progress >= 0.9f)
            {
                if(Time.time - startTime >= LoadTime)
                {
                    yield return StartCoroutine(FadeOut());
                    operation.allowSceneActivation = true;
                }
            }
        }
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
        yield return null;
    }

    IEnumerator FadeOut()
    {
        time = 0f;
        Color alpha = Panel.color;
        alpha.a = 0f;
        Panel.color = alpha;
        
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / Ftime;
            alpha.a = Mathf.Lerp(0,1,time);
            Panel.color = alpha;
            yield return null;
        }
        yield return null; 
    }
}