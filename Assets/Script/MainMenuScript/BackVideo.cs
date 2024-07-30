using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using UnityEngine.UI;

public class BackVideo : MonoBehaviour
{
    private VideoPlayer video;
    public RawImage image;
    public TextMeshProUGUI title;
    public TextMeshProUGUI artist;

    void Start()
    {
        image.gameObject.SetActive(true);
    }

    void Update()
    {
        video = GetComponent<VideoPlayer>();

        string videoPath = "AlbumVideo/" + title.text + "_" + artist.text;
        
        VideoClip videoClip = Resources.Load<VideoClip>(videoPath);
        video.clip = videoClip;
    }
}