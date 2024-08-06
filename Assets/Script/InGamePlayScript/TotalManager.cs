using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalManager : MonoBehaviour
{
    public static TotalManager instance;

    public float defaultChartSpeed = 2f;
    public float userChartSpeed = 1f;
    public float finalChartSpeed;

    public float maxUserChartSpeed = 7f;

    public float minNotePosY = -3f;
    public float minNotePosX = -2f;
    public float maxNotePosX = 2f;

    public int songTime = 80;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
        finalChartSpeed = defaultChartSpeed * userChartSpeed;
    }

    private void Update()
    {
        finalChartSpeed = defaultChartSpeed * userChartSpeed;
    }

    public void ChangeSpeed(float speed)
    {
        userChartSpeed += speed;
        finalChartSpeed = defaultChartSpeed * userChartSpeed;
    }
}
