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

    private MainCanvasBlurControl mainCanvasBlurControl; // 메인 캔버스 블러 제어 스크립트 참조

    public static bool isPauseCanvasOn = false;
    public static bool isHelpCanvasOn = false;

    void Start()
    {
        // MainCanvasBlurControl 스크립트를 찾아서 참조
        mainCanvasBlurControl = FindObjectOfType<MainCanvasBlurControl>();

        // 블러 처리 제외 리스트에 pauseCanvas와 helpCanvas 추가
        if (mainCanvasBlurControl != null)
        {
            mainCanvasBlurControl.excludeObjects.Add(pauseCanvas);
            mainCanvasBlurControl.excludeObjects.Add(helpCanvas);
        }

        pauseCanvas.SetActive(false);
        helpCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // if (isHelpCanvasOn)
            // {
            //     OnClickCloseHelpBtn(); // HelpCanvas 상태에서는 ESC 키 입력 시 Return 버튼과 같은 동작
            // }
            // else if (isPauseCanvasOn)
            // {
            //     OnClickContinueBtn(); // PauseCanvas 상태에서는 ESC 키 입력 시 Continue 버튼과 같은 동작
            // }
            // else
            // {
            //     ActivatePauseCanvas();
            // }
            if (!isPauseCanvasOn)
            {
                ActivatePauseCanvas();
            }
            else if (isHelpCanvasOn)
            {
                OnClickCloseHelpBtn();
            }
            else if (isPauseCanvasOn)
            {
                OnClickContinueBtn();
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

    private void ActivatePauseCanvas()
    {
        
        if (mainCanvasBlurControl != null)
        {
            mainCanvasBlurControl.EnableBlur(); // 블러 활성화
        }
        pauseCanvas.SetActive(true);

        Time.timeScale = 0f;
        videoPlayer.Pause();

        isPauseCanvasOn = true;
    }

    public void OnClickContinueBtn()
    {
        pauseCanvas.SetActive(false);
        if (mainCanvasBlurControl != null)
        {
            mainCanvasBlurControl.DisableBlur(); // 블러 비활성화
        }

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
            isHelpCanvasOn = true;
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
              
            isHelpCanvasOn = false;
        }
    }

    public void OnClickExitBtn()
    {
        // Exit 버튼 클릭 시 메인메뉴로 가게끔 구현 예정
    }
}
