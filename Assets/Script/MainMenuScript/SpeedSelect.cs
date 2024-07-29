using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedSelect : MonoBehaviour
{
    public TextMeshProUGUI speed;
    public float[] speedList = new float[] {1.0f, 1.2f, 1.5f, 2.0f, 3.0f, 4.0f, 5.0f};
    public int index = 0;
    public static float finalSpeed;

    void Start()
    {
        speed.text = "Track Speed: x " + speedList[index].ToString("F1");
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(index < 6)
            {
                index += 1;
            }
            else if(index == 6)
            {
                index = 0;
            }
            speed.text = "Track Speed: x " + speedList[index].ToString("F1");
            finalSpeed = speedList[index];
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(index > 0)
            {
                index -= 1;
            }
            else if(index == 0)
            {
                index = 6;
            }
            speed.text = "Track Speed: x " + speedList[index].ToString("F1");
            finalSpeed = speedList[index];
        }
    }

    public void ResetSpeed()
    {
        index = 0;
        speed.text = "Track Speed: x " + speedList[index].ToString("F1");
        finalSpeed = speedList[index];
    }
}
