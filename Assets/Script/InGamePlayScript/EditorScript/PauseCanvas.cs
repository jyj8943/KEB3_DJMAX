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
    public GameObject firstPanel; 
    public GameObject secondPanel;
    public RawImage movieScreen;
    public VideoPlayer videoPlayer;
    public EditorCamera EditorCamera;
    public GameObject volume;

    public GameObject nameCanvas;

    public static bool isPauseCanvasOn = false;
    public static bool isHelpCanvasOn = false;

    public AudioSource audioSource;
    public AudioClip clip1;
    public AudioClip clip2;
    void Start()
    {
        pauseCanvas.SetActive(false);
        helpCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (nameCanvas.activeSelf)
            {
                nameCanvas.SetActive(false);
                volume.SetActive(false);
            }
            
            else
            {
                if (!isPauseCanvasOn)
                {
                    ActivatePauseCanvas();
                }
                else if (isHelpCanvasOn)
                {
                    OnClickCloseHelpBtn();
                    Time.timeScale = 0f;
                }
                else if (isPauseCanvasOn)
                {
                    OnClickContinueBtn();
                }
            }
        }
        

        // 마우스 스크롤을 무시하는 코드 (필요한 경우 사용)
        if (isPauseCanvasOn || nameCanvas.activeSelf)
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                return;
            }
        }
    }

    private void ActivatePauseCanvas()
    {
        audioSource.PlayOneShot(clip1);
        volume.SetActive(true); // 블러 활성화
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        videoPlayer.Pause();
        isPauseCanvasOn = true;
    }

    public void OnClickContinueBtn()
    {
        audioSource.PlayOneShot(clip1);
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
            audioSource.PlayOneShot(clip1);
            helpCanvas.SetActive(true);
            if (pauseCanvas != null)
            {
                pauseCanvas.SetActive(false);
            }

            firstPanel.SetActive(true);
            secondPanel.SetActive(false);
            isHelpCanvasOn = true;
        }
    }

    public void OnClickCloseHelpBtn()
    {
        if (helpCanvas != null)
        {
            audioSource.PlayOneShot(clip2);
            helpCanvas.SetActive(false);
            if (pauseCanvas != null)
            {
                pauseCanvas.SetActive(true);
            }
              
            isHelpCanvasOn = false;
        }
    }

    public void OnclickNextPageBtn()
    {
        audioSource.PlayOneShot(clip2);
        firstPanel.SetActive(false);
        secondPanel.SetActive(true);
    }
    
    public void OnclickPrevPageBtn()
    {
        audioSource.PlayOneShot(clip2);
        secondPanel.SetActive(false);
        firstPanel.SetActive(true);
    }

    public void OnClickExitBtn()
    {
        audioSource.PlayOneShot(clip1);
        SceneManager.LoadScene("TitleMenu");
    }
}
