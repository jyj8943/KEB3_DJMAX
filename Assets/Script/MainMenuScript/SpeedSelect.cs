using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;
/////////////////////

public class SpeedSelect : MonoBehaviour
{
    public TotalManager TM;
    
    public TextMeshProUGUI speed;
    
    private float holdingTime = 0.15f;
    private float timer = 0f;
    private bool isHoldingUp = false;
    private bool isHoldingDown = false;

    public AudioSource audioSource;
    public AudioClip upClip;
    public AudioClip downClip;
    
    void Start()
    {
        TM = TotalManager.instance;
        
        speed.text = "Track Speed: x " + TM.userChartSpeed.ToString("F1");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (TM.userChartSpeed < TM.maxUserChartSpeed)
            {
                TM.userChartSpeed += 0.1f;
            }
            isHoldingUp = true;
            timer = 0f;
            audioSource.PlayOneShot(upClip);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (TM.userChartSpeed > 1f)
            {
                TM.userChartSpeed -= 0.1f;
            }
            isHoldingDown = true;
            timer = 0f;
            audioSource.PlayOneShot(downClip);
        }
        
        if (Input.GetKey(KeyCode.UpArrow) && isHoldingUp)
        {
            timer += Time.deltaTime;
            
            if (timer >= holdingTime)
            {
                if (TM.userChartSpeed < TM.maxUserChartSpeed)
                {
                    TM.userChartSpeed += 0.1f;
                }
                timer = 0f;
            }
        }
        if (Input.GetKey(KeyCode.DownArrow) && isHoldingDown)
        {
            timer += Time.deltaTime;

            if (timer >= holdingTime)
            {
                if (TM.userChartSpeed > 1f)
                {
                    TM.userChartSpeed -= 0.1f;
                }
                timer = 0f;
            }
        }
        
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isHoldingUp = false;
            timer = 0f;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isHoldingDown = false;
            timer = 0f;
        }

        TM.userChartSpeed = Mathf.Round(TM.userChartSpeed * 10f) / 10f;
        
        speed.text = "Track Speed: x " + TM.userChartSpeed.ToString("F1");
    }

    public void ResetSpeed()
    {
        TM.userChartSpeed = 1.0f;
        speed.text = "Track Speed: x " + TM.userChartSpeed.ToString("F1");
    }
}
