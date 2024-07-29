using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ShowVideo : MonoBehaviour
{
    private bool isClicked = false;

    public VideoPlayer videoPlayer;
    public RawImage movieScreen;
    public TextMeshProUGUI btnText;
    public GameObject basicScreen;
    void Start()
    {
        basicScreen.SetActive(true);
    }

    void Update()
    {
        
    }

    public void ShowVideoOnClick()
    {
        if (videoPlayer != null)
        {
            if (!isClicked)
            {
                isClicked = true;

                Color tempColor = movieScreen.color;
                tempColor.a = 0f;
                movieScreen.color = tempColor;

                btnText.text = "Video Off";
                basicScreen.SetActive(true);
            }
            else if (isClicked)
            {
                isClicked = false;

                Color tempColor = movieScreen.color;
                tempColor.a = 255f;
                movieScreen.color = tempColor;

                btnText.text = "Video On";
                basicScreen.SetActive(false);
            }
        }
    }
}
