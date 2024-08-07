using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using UnityEngine.UI;

public class BackVideo : MonoBehaviour
{
    public VideoPlayer video;
    public RawImage image;
    public TextMeshProUGUI title;
    public TextMeshProUGUI artist;

    private string tempTitle;
    private string tempArtist;
    
    void Update()
    {
        if (tempArtist != artist.text && tempTitle != title.text)
        {
            tempArtist = artist.text;
            tempTitle = title.text;
            
            PlayVideo();
        }
    }

    private void PlayVideo()
    {
        string videoPath = "AlbumVideo/" + tempTitle + "_" + tempArtist;
        
        var videoClip = Resources.Load<VideoClip>(videoPath);
        
        if (videoClip != null)
        {
            video.source = VideoSource.VideoClip;
            video.clip = videoClip;
            Debug.Log("Video Loaded from Resources");
            video.Play();
        }
        else
        {
            var localPath = Path.Combine(Application.persistentDataPath, "SongVideo",
                tempTitle + tempArtist + ".mp4");

            if (File.Exists(localPath))
            {
                video.source = VideoSource.Url;
                video.url = localPath;
                Debug.Log("Video Loaded from Local Folder");
                video.Play();
            }
        }
    }
}