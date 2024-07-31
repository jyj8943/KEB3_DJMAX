using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCamera : MonoBehaviour
{
    void Update()
    {
        if (InGamePlayManager.instance.isPlaying) transform.Translate(0f, TotalManager.instance.defaultChartSpeed
                                               * TotalManager.instance.userChartSpeed * Time.deltaTime, 0f);
    }
}
