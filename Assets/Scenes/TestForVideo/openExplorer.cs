using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEditor;
using TMPro;

public class openExplorer : MonoBehaviour
{
    string path;
    public RawImage background;
    public VideoPlayer videoPlayer;
    public RenderTexture renderTexture;
    public TextMeshProUGUI statusText;  // TextMeshPro 상태 표시 텍스트

    void Start()
    {
        // VideoPlayer 초기화
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        videoPlayer.targetTexture = renderTexture;

        // RenderTexture를 RawImage에 할당
        background.texture = renderTexture;

        // RenderTexture 초기화 (검정색)
        ClearRenderTexture();

        // VideoPlayer 준비 완료 이벤트 핸들러 등록
        videoPlayer.prepareCompleted += OnVideoPrepared;

        // 초기 상태 텍스트 설정
        statusText.text = "Paused";
    }

    public void OpenExplorer()
    {
        path = EditorUtility.OpenFilePanel("Select Video", "", "mp4");
        GetVideo();
    }

    void GetVideo()
    {
        if (!string.IsNullOrEmpty(path))
        {
            UpdateVideo();
        }
    }

    void UpdateVideo()
    {
        videoPlayer.url = "file:///" + path;
        videoPlayer.Prepare();
        ClearRenderTexture();
    }

    void OnVideoPrepared(VideoPlayer source)
    {
        // Video 준비가 완료된 후 첫 번째 프레임을 0초로 설정
        videoPlayer.time = 0;
        UpdateStatusText();  // 상태 텍스트 업데이트
    }

    void Update()
    {
        // 스페이스바를 누르면 동영상 재생/정지
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
            }
            else
            {
                videoPlayer.Play();
            }
            UpdateStatusText();  // 상태 텍스트 업데이트
        }
    }

    void ClearRenderTexture()
    {
        RenderTexture rt = RenderTexture.active;
        RenderTexture.active = renderTexture;
        GL.Clear(true, true, Color.black);
        RenderTexture.active = rt;
    }

    void UpdateStatusText()
    {
        if (videoPlayer.isPlaying)
        {
            statusText.text = "Playing";
        }
        else
        {
            statusText.text = "Paused";
        }
    }
}
