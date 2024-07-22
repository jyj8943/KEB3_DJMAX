using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    public static EditorManager instance;

    public TextMeshProUGUI userChartSpeedText;

    //public float defaultChartSpeed;
    //public float userChartSpeed;

    public float defaultGridHeight = 8f;
    public float defaultVerticalLineHeight = 2f;

    public float gridHeight;
    public float verticalLineHeight;

    //public float maxUserChartSpeed = 5f;

    public int maxGridCount;

    public bool isInsertShortNote = false;
    public bool isInsertLongNote = false; 
    public bool isDeleteNote = false;

    //public float minNotePosY = -3f;
    //public float minNotePosX = -2f;
    //public float maxNotePosX = 2f;

    //public int songTime = 80;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gridHeight = defaultGridHeight * TotalManager.instance.userChartSpeed;
        verticalLineHeight = defaultVerticalLineHeight * TotalManager.instance.userChartSpeed;
        maxGridCount = (TotalManager.instance.songTime / 4);
    }

    private void Update()
    {
        userChartSpeedText.SetText("User Chart Speed: " + TotalManager.instance.userChartSpeed);
    }

    public void changeUserChartSpeed(float changeSpeed)
    {
        TotalManager.instance.userChartSpeed += changeSpeed;
        gridHeight = defaultGridHeight * TotalManager.instance.userChartSpeed;
        verticalLineHeight = defaultVerticalLineHeight * TotalManager.instance.userChartSpeed;
    }

    public void InitInstance()
    {
        maxGridCount = (TotalManager.instance.songTime / 4);
    }
}
