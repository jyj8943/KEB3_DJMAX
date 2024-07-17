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

        if (isLoadedSong)
        {
            slider.value = editorCamera.transform.position.y / (EditorManager.instance.userChartSpeed * EditorManager.instance.defaultChartSpeed);
            //slider.value = bgm.time;
        }
    }   

    public void SliderValueChanged()
    {
        if (isLoadedSong)
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
        bgm.Stop();

        EditorManager.instance.songTime = bgmLength;
        EditorManager.instance.InitInstance();

        gridList.MakeGrids();
        horizontalLineList.MakeHorizontalLine();
    }

    public IEnumerator waitAndPlaySong()
    {
        yield return new WaitForSeconds(0.5f);
    }

    public void playSong()
    {
        if (isPaused)
        {
            waitAndPlaySong();
        }
        bgm.Play();
        isPaused = false;
    }

    public void pauseSong()
    {
        bgm.Pause();
        isPaused = true;
    }
}
