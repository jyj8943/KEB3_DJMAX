using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShortNote : MonoBehaviour
{
    public float defaultDist;

    void Start()
    {
        defaultDist = ( transform.position.y - EditorManager.instance.minNotePosY ) / EditorManager.instance.userChartSpeed;
    }

    void Update()
    {
        
    }

}
