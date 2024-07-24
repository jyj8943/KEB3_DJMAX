using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PauseCanvas : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject helpCanvas;
    public RawImage movieScreen;
    public VideoPlayer videoPlayer;
    public EditorCamera EditorCamera;

    public static bool isPauseCanvasOn = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseCanvas != null && isPauseCanvasOn)
            {
                pauseCanvas.SetActive(false);

                Time.timeScale = 1f;
                //videoPlayer.Play();
                EditorCamera.isPlaying = false;

                isPauseCanvasOn = false;
            }
            else if (pauseCanvas != null && !isPauseCanvasOn)
            {
                pauseCanvas.SetActive(true);

                Time.timeScale = 0f;
                videoPlayer.Pause();

                isPauseCanvasOn = true;
            }
        }

        // 마우스 스크롤을 무시하는 코드 (필요한 경우 사용)
        if (isPauseCanvasOn)
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                return;
            }
        }
    }

    public void OnClickContinueBtn()
    {
        pauseCanvas.SetActive(false);

        Time.timeScale = 1f;
        videoPlayer.Play();

        isPauseCanvasOn = false;
    }

    public void OnClickHelpBtn()
    {
        if (helpCanvas != null)
        {
            helpCanvas.SetActive(true);
            if (pauseCanvas != null)
            {
                pauseCanvas.SetActive(false);
            }
        }
    }

    public void OnClickCloseHelpBtn()
    {
        if (helpCanvas != null)
        {
            helpCanvas.SetActive(false);
            if (pauseCanvas != null)
            {
                pauseCanvas.SetActive(true);
            }
            else
            {
                
                pauseCanvas.SetActive(true);
                Time.timeScale = 0f;
                videoPlayer.Pause();
                isPauseCanvasOn = true;
            }
        }
    }

    public void OnClickExitBtn()
    {
        // Exit 버튼 클릭 시 메인메뉴로 가게끔 구현 예정
    }
}