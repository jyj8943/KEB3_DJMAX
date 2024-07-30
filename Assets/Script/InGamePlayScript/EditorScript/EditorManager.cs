using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    public static EditorManager instance;

    public TextMeshProUGUI userChartSpeedText;

    public float defaultGridHeight = 8f;
    public float defaultVerticalLineHeight = 2f;

    public float gridHeight;
    public float verticalLineHeight;

    public int maxGridCount;

    public bool isInsertShortNote = false;
    public bool isInsertLongNote = false; 
    public bool isDeleteNote = false;

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
        TotalManager.instance.ChangeSpeed(changeSpeed);
        
        gridHeight = defaultGridHeight * TotalManager.instance.userChartSpeed;
        verticalLineHeight = defaultVerticalLineHeight * TotalManager.instance.userChartSpeed;
    }

    public void InitInstance()
    {
        maxGridCount = (TotalManager.instance.songTime / 4);
    }
}
