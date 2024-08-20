using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCamera : MonoBehaviour
{
    public TotalManager TM;
    public InGamePlayManager GM;
    
    private void Start()
    {
        TM = TotalManager.instance;
        GM = InGamePlayManager.instance;

        transform.position = new Vector3(0f, 0f - TM.userSync, 0f);
    }

    void Update()
    {
        // if (InGamePlayManager.instance.isPlaying)
        // {
        //     transform.position = new Vector3(0f, TM.finalChartSpeed * (float)GM.video.time, 0f);
        // }
    }
}
