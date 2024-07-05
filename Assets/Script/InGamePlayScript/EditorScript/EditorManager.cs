using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    public static EditorManager instance;

    public TextMeshProUGUI userChartSpeedText;

    // 그리드는 초당 defaultChartSpeed * userChartSpeed 만큼 아래로 움직이며, 그리드 하나가 의미하는 시간은 결국 4초를 의미해야한다.
    // 다음 그리드로 넘어가는 시간은 ( 1배속의 그리드 높이 * userChartSpeed )  / ( defaultChartSpeed * userChartSpeed )초
    public float defaultChartSpeed;
    public float userChartSpeed;

    // 유저 1배속 때의 그리드 높이, 수평바 간격
    public float defaultGridHeight = 8f;
    public float defaultVerticalLineHeight = 2f;

    public float gridHeight;
    public float verticalLineHeight;

    public float maxUserChartSpeed = 5f;

    public int maxGridCount = 10;

    private void Awake()
    {
        instance = this;

        gridHeight = defaultGridHeight * userChartSpeed;
        verticalLineHeight = defaultVerticalLineHeight * userChartSpeed;
    }

    private void Update()
    {
        userChartSpeedText.SetText("User Chart Speed: " + userChartSpeed);
    }

    public void changeUserChartSpeed(float changeSpeed)
    {
        userChartSpeed += changeSpeed;
        gridHeight = defaultGridHeight * userChartSpeed;
        verticalLineHeight = defaultVerticalLineHeight * userChartSpeed;
    }
}
