using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedSelect : MonoBehaviour
{
    public TextMeshProUGUI speed;
    private float keyHoldTime = 0.0f;
    private bool isKeyHeld = false;
    public float trackSpeed = 1.0f;
    public static float finalSpeed;

    void Start()
    {
        speed.text = "Track Speed: x " + trackSpeed.ToString("F1");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (trackSpeed < 7.0f)
            {
                if (!isKeyHeld)
                {
                    trackSpeed += 0.1f;
                    isKeyHeld = true;
                }
                else
                {
                    keyHoldTime += Time.deltaTime;
                    if (keyHoldTime >= 0.5f)
                    {
                        trackSpeed += 0.1f * Time.deltaTime * 20;
                    }
                }
            }

            if (trackSpeed >= 7.0f)
            {
                trackSpeed = 1.0f;
                keyHoldTime = 0.0f;
                isKeyHeld = true;
            }

            speed.text = "Track Speed: x " + trackSpeed.ToString("F1");
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (trackSpeed > 1.0f)
            {
                if (!isKeyHeld)
                {
                    trackSpeed -= 0.1f;
                    isKeyHeld = true;
                }
                else
                {
                    keyHoldTime += Time.deltaTime;
                    if (keyHoldTime >= 0.5f)
                    {
                        trackSpeed -= 0.1f * Time.deltaTime * 20;
                    }
                }
            }

            if (trackSpeed <= 1.0f)
            {
                trackSpeed = 7.0f;
                keyHoldTime = 0.0f;
                isKeyHeld = true;
            }

            speed.text = "Track Speed: x " + trackSpeed.ToString("F1");
        }
        else
        {
            keyHoldTime = 0.0f;
            isKeyHeld = false;
        }

        finalSpeed = trackSpeed;
    }


    public void ResetSpeed()
    {
        trackSpeed = 1.0f;
        speed.text = "Track Speed: x " + trackSpeed.ToString("F1");
        finalSpeed = trackSpeed;
    }
}
