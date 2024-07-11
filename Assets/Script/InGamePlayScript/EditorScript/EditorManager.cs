using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    public static EditorManager instance;

    public TextMeshProUGUI userChartSpeedText;

    // �׸���� �ʴ� defaultChartSpeed * userChartSpeed ��ŭ �Ʒ��� �����̸�, �׸��� �ϳ��� �ǹ��ϴ� �ð��� �ᱹ 4�ʸ� �ǹ��ؾ��Ѵ�.
    // ���� �׸���� �Ѿ�� �ð��� ( 1����� �׸��� ���� * userChartSpeed )  / ( defaultChartSpeed * userChartSpeed )��
    public float defaultChartSpeed;
    public float userChartSpeed;

    // ���� 1��� ���� �׸��� ����, ����� ����
    public float defaultGridHeight = 8f;
    public float defaultVerticalLineHeight = 2f;

    public float gridHeight;
    public float verticalLineHeight;

    public float maxUserChartSpeed = 5f;

    public int maxGridCount;

    public bool isInsertShortNote = false;
    public bool isInsertLongNote = false; 
    //public bool isDeleteNote = false; -> ��ӿ� ���� ��Ʈ Y�� ���� ��� �ϼ� �� ���� ��� ����

    public float minNotePosY = -3f;
    public float minNotePosX = -2f;
    public float maxNotePosX = 2f;

    public int songTime = 128;

    private void Awake()
    {
        instance = this;

        gridHeight = defaultGridHeight * userChartSpeed;
        verticalLineHeight = defaultVerticalLineHeight * userChartSpeed;
        maxGridCount = songTime / 4;
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
