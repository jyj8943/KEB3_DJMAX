using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBlink : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // TextMeshPro 텍스트 객체
    public float blinkSpeed = 1.0f; // 깜빡거림 속도

    private bool isBlinking = true;

    void Start()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        StartCoroutine(BlinkText());
    }

    public void StopBlinking()
    {
        isBlinking = false;
        StopCoroutine(BlinkText());
    }

    IEnumerator BlinkText()
    {
        while (isBlinking)
        {
            // 텍스트의 알파 값을 점진적으로 변경
            for (float alpha = 1; alpha >= 0; alpha -= Time.deltaTime * blinkSpeed)
            {
                SetAlpha(alpha);
                yield return null;
            }

            for (float alpha = 0; alpha <= 1; alpha += Time.deltaTime * blinkSpeed)
            {
                SetAlpha(alpha);
                yield return null;
            }
        }
    }

    void SetAlpha(float alpha)
    {
        if (textMeshPro != null)
        {
            Color color = textMeshPro.color;
            color.a = alpha;
            textMeshPro.color = color;
        }
    }
}
