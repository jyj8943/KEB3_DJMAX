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
    }

    void Update()
    {
        // if (InGamePlayManager.instance.isPlaying)
        // {
        //     transform.position = new Vector3(0f, TM.finalChartSpeed * (float)GM.video.time, 0f);
        // }
    }
}
