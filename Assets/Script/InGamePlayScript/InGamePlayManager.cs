using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Serialization;

public class InGamePlayManager : MonoBehaviour
{
    public static InGamePlayManager instance;
    
    public float defaultChartSpeed;
    public float userChartSpeed;

    public float defaultGridHeight = 8f;
    public float defaultVerticalLineHeight = 2f;
    
    public float maxUserChartSpeed = 5f;
    
    public float minNotePosY = -3f;
    public float minNotePosX = -2f;
    public float maxNotePosX = 2f;
    
    public int songTime = 80;
    
    void Awake()
    {
        instance = this;
    }
    
    void Update()
    {
        
    }
}
