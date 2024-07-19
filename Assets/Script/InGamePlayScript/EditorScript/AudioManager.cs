using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class AudioManager : MonoBehaviour
{
    public EditorCamera editorCamera;
    public AudioSource bgm;
    public Slider slider;
    public GridList gridList;
    public HorizontalLineList horizontalLineList;

    public RawImage movieScreen;
    public VideoPlayer videoPlayer;

    [SerializeField]
    private bool isPaused = true;
    public bool isLoadedSong = false;

    private void Update()
    {
        if (isLoadedSong && !Input.GetMouseButton(0))
        {
            //slider.value = bgm.time;
            slider.value = (float)videoPlayer.time;
        }
    }   

    public void SliderValueChanged()
    {
        // 기능은 구현한 것 같지만 조건을 다시 설정해야함
        // 재생 중일때 카메라, 슬라이더 모두 움직이여함
        // 정지중일때 하나에 맞춰서 모두가 움직여야함
        if (isLoadedSong && Input.GetMouseButton(0))
        {
            if (slider.value > (float)videoPlayer.length)
            {
                editorCamera.isPlaying = false;

                editorCamera.transform.position = new Vector3(editorCamera.transform.position.x
                        , slider.value * EditorManager.instance.userChartSpeed * EditorManager.instance.defaultChartSpeed
                        , editorCamera.transform.position.z);
            }
            else
            {
                videoPlayer.Pause();
                editorCamera.isPlaying = false;

                videoPlayer.time = (double)slider.value;

                editorCamera.transform.position = new Vector3(editorCamera.transform.position.x
                    , slider.value * EditorManager.instance.userChartSpeed * EditorManager.instance.defaultChartSpeed
                    , editorCamera.transform.position.z);
            }
        }

        if (isLoadedSong && Input.GetMouseButtonUp(0))
        {
            videoPlayer.Play();
            editorCamera.isPlaying = true;
        }
    }

    public void InitSong()
    {
        Debug.Log(videoPlayer.length.ToString());
        int bgmLength = 0;
        
        if (videoPlayer != null)
        {
            bgmLength = Mathf.RoundToInt((float)videoPlayer.length);
            slider.maxValue = bgmLength;
            slider.minValue = 0;
            slider.value = 0;
        }

        videoPlayer.Play();
        videoPlayer.Pause();
        //videoPlayer.time = 10f;

        EditorManager.instance.songTime = bgmLength;
        EditorManager.instance.InitInstance();

        gridList.MakeGrids();
        horizontalLineList.MakeHorizontalLine();
    }

    public void playSong()
    {
        //bgm.UnPause();
        videoPlayer.Play();
        isPaused = false;
    }

    public void pauseSong()
    {
        //bgm.Pause();
        videoPlayer.Pause();
        isPaused = true;
    }
}
