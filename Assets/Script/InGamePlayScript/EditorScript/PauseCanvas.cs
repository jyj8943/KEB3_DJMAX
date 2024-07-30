using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class PauseCanvas : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject helpCanvas;
    public RawImage movieScreen;
    public VideoPlayer videoPlayer;
    public EditorCamera EditorCamera;
    public GameObject volume;
    
    
    

    public static bool isPauseCanvasOn = false;
    public static bool isHelpCanvasOn = false;

    void Start()
    {
        pauseCanvas.SetActive(false);
        helpCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
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
        
        
        volume.SetActive(true); // 블러 활성화
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        videoPlayer.Pause();
        isPauseCanvasOn = true;
    }

    public void OnClickContinueBtn()
    {
        pauseCanvas.SetActive(false);
        volume.SetActive(false); // 블러 비활성화
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
        SceneManager.LoadScene("TitleMenu");
    }
}