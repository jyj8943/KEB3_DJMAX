using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PauseCanvas : MonoBehaviour
{
    public GameObject pauseCanvas;
    public RawImage movieScreen;
    public VideoPlayer videoPlayer;

    private bool isPauseCanvasOn = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseCanvas != null && isPauseCanvasOn)
            {
                pauseCanvas.SetActive(false);

                Time.timeScale = 1f;
                videoPlayer.Play();

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
        // Help 버튼 클릭시 도움말 패널이 뜨도록 구현 예정
    }

    public void OnClickExitBtn()
    {
        // Exit 버튼 클릭시 메인메뉴로 가게끔 구현 예정
    }
}
