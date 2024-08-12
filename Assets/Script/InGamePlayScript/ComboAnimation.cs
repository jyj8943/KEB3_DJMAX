using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ComboAnimation : MonoBehaviour
{
    public RectTransform textBox; // Panel의 RectTransform
    public float animationDuration = 1.0f; // 애니메이션 지속 시간

    private void Start()
    {
        // 텍스트 박스를 처음에 숨깁니다.
        textBox.anchoredPosition = new Vector2(textBox.anchoredPosition.x, -Screen.height);
        textBox.gameObject.SetActive(false); // 비활성화
    }

    public void ShowTextBox()
    {
        textBox.gameObject.SetActive(true); // 텍스트 박스를 활성화
        StartCoroutine(AnimateTextBox());
    }

    private IEnumerator AnimateTextBox()
    {
        float elapsedTime = 0f;
        Vector2 startPos = new Vector2(textBox.anchoredPosition.x, -Screen.height);
        Vector2 endPos = new Vector2(textBox.anchoredPosition.x, 0f); // 최종 위치

        while (elapsedTime < animationDuration)
        {
            textBox.anchoredPosition = Vector2.Lerp(startPos, endPos, (elapsedTime / animationDuration));
            elapsedTime += Time.deltaTime;
            yield return null; // 다음 프레임까지 대기
        }

        textBox.anchoredPosition = endPos; // 최종 위치 설정
    }
}
