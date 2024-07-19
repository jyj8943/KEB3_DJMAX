using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCamera : MonoBehaviour
{
    public bool isPlaying = false;
    
    //public AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        if (isPlaying) transform.Translate(0f, InGamePlayManager.instance.defaultChartSpeed
                                               * InGamePlayManager.instance.userChartSpeed * Time.deltaTime, 0f);
        
    }
}
