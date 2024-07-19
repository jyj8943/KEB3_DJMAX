using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public EditorCamera editorCamera;
    public AudioSource bgm;
    public Slider slider;
    public GridList gridList;
    public HorizontalLineList horizontalLineList;

    [SerializeField]
    private bool isPaused = true;
    public bool isLoadedSong = false;

    private void Update()
    {
        if (isLoadedSong && !Input.GetMouseButton(0))
        {
            slider.value = bgm.time;
        }
    }   

    public void SliderValueChanged()
    {
        // 기능은 구현한 것 같지만 조건을 다시 설정해야함
        // 재생 중일때 카메라, 슬라이더 모두 움직이여함
        // 정지중일때 하나에 맞춰서 모두가 움직여야함
        if (isLoadedSong && Input.GetMouseButton(0))
        {
            if (slider.value > bgm.clip.length)
            {
                editorCamera.transform.position = new Vector3(editorCamera.transform.position.x
                        , slider.value * EditorManager.instance.userChartSpeed * EditorManager.instance.defaultChartSpeed
                        , editorCamera.transform.position.z);
            }
            else
            {
                bgm.time = slider.value;

                editorCamera.transform.position = new Vector3(editorCamera.transform.position.x
                    , slider.value * EditorManager.instance.userChartSpeed * EditorManager.instance.defaultChartSpeed
                    , editorCamera.transform.position.z);
            }
        }
    }

    public void InitSong()
    {
        Debug.Log(bgm.clip.length.ToString());
        int bgmLength = 0;
        
        if (bgm.clip != null)
        {
            bgmLength = Mathf.RoundToInt(bgm.clip.length);
            slider.maxValue = bgmLength;
            slider.minValue = 0;
            slider.value = 0;
        }

        bgm.Play();
        bgm.Pause();

        EditorManager.instance.songTime = bgmLength;
        EditorManager.instance.InitInstance();

        gridList.MakeGrids();
        horizontalLineList.MakeHorizontalLine();
    }

    public void playSong()
    {
        bgm.UnPause();
        isPaused = false;
    }

    public void pauseSong()
    {
        bgm.Pause();
        isPaused = true;
    }
}
