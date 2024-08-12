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
    
    // private float keyHoldTime = 0.0f;
    // private bool isKeyHeld = false;
    // public float trackSpeed = 1.0f;
    // public static float finalSpeed;

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
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (TM.userChartSpeed > 1f)
            {
                TM.userChartSpeed -= 0.1f;
            }
            isHoldingDown = true;
            timer = 0f;
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
        
        // if (Input.GetKey(KeyCode.UpArrow))
        // {
        //     if (!isKeyHeld)
        //     {
        //         trackSpeed += 0.1f;
        //         isKeyHeld = true;
        //         keyHoldTime = 0.0f;
        //     }
        //     else
        //     {
        //         keyHoldTime += Time.deltaTime;
        //         if (keyHoldTime >= 1.0f)
        //         {
        //             trackSpeed += 0.1f * Time.deltaTime * 20;
        //         }
        //     }
        //
        //     if (Mathf.Round(trackSpeed * 10.0f) / 10.0f > 7.0f)
        //     {
        //         trackSpeed = 1.0f;
        //     }
        //
        //     trackSpeed = Mathf.Round(trackSpeed * 10.0f) / 10.0f;
        //
        //     speed.text = "Track Speed: x " + trackSpeed.ToString("F1");
        // }
        // else if (Input.GetKey(KeyCode.DownArrow))
        // {
        //     if (!isKeyHeld)
        //     {
        //         trackSpeed -= 0.1f;
        //         isKeyHeld = true;
        //         keyHoldTime = 0.0f;
        //     }
        //     else
        //     {
        //         keyHoldTime += Time.deltaTime;
        //         if (keyHoldTime >= 1.0f)
        //         {
        //             trackSpeed -= 0.1f * Time.deltaTime * 20;
        //         }
        //     }
        //
        //     if (Mathf.Round(trackSpeed * 10.0f) / 10.0f < 1.0f)
        //     {
        //         trackSpeed = 7.0f;
        //     }
        //
        //     trackSpeed = Mathf.Round(trackSpeed * 10.0f) / 10.0f;
        //
        //     speed.text = "Track Speed: x " + trackSpeed.ToString("F1");
        // }
        // else
        // {
        //     keyHoldTime = 0.0f;
        //     isKeyHeld = false;
        // }
        //
        // finalSpeed = trackSpeed;
    }

    public void ResetSpeed()
    {
        TM.userChartSpeed = 1.0f;
        speed.text = "Track Speed: x " + TM.userChartSpeed.ToString("F1");
        //finalSpeed = trackSpeed;
    }
}
