using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongScrollManager : MonoBehaviour
{
    public ScrollRect scrollRect;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ScrollUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ScrollDown();
        }
    }

    void ScrollUp()
    {
        scrollRect.verticalNormalizedPosition += 0.1f;
    }

    void ScrollDown()
    {
        scrollRect.verticalNormalizedPosition -= 0.1f;
    }
}
