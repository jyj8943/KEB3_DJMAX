using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCamera : MonoBehaviour
{
    public bool isPlaying = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isPlaying)
            {
                isPlaying = false;
                //audioManager.pauseSong();
            }
            else
            {
                isPlaying = true;
                //audioManager.playSong();
            }
            
        }
        if (isPlaying) transform.Translate(0f, TotalManager.instance.defaultChartSpeed
                                               * TotalManager.instance.userChartSpeed * Time.deltaTime, 0f);
        
    }
}
