using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;
/////////////////////

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
            if (!isKeyHeld)
            {
                trackSpeed += 0.1f;
                isKeyHeld = true;
                keyHoldTime = 0.0f;
            }
            else
            {
                keyHoldTime += Time.deltaTime;
                if (keyHoldTime >= 1.0f)
                {
                    trackSpeed += 0.1f * Time.deltaTime * 20;
                }
            }

            if (trackSpeed >= 7.0f)
            {
                trackSpeed = 1.0f;
            }

            trackSpeed = Mathf.Round(trackSpeed * 10.0f) / 10.0f;

            speed.text = "Track Speed: x " + trackSpeed.ToString("F1");
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (!isKeyHeld)
            {
                trackSpeed -= 0.1f;
                isKeyHeld = true;
                keyHoldTime = 0.0f;
            }
            else
            {
                keyHoldTime += Time.deltaTime;
                if (keyHoldTime >= 1.0f)
                {
                    trackSpeed -= 0.1f * Time.deltaTime * 20;
                }
            }

            if (trackSpeed <= 1.0f)
            {
                trackSpeed = 7.0f;
            }

            trackSpeed = Mathf.Round(trackSpeed * 10.0f) / 10.0f;

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
