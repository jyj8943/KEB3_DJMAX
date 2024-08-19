using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SoundSettingManager : MonoBehaviour
{
    public TotalManager TM;
    
    public GameObject mainBtn;
    public AudioSource main;
    public TextMeshProUGUI mainLevel;
    public Slider mainSlider;

    public GameObject trackBtn;
    public AudioSource track;
    public TextMeshProUGUI trackLevel;
    public Slider trackSlider;

    public GameObject sfxBtn;
    public AudioSource sfx;
    public TextMeshProUGUI sfxLevel;
    public Slider sfxSlider;

    void Awake()
    {
        TM = TotalManager.instance;
        InitVolume();
    }

    void Update()
    {
        GameObject selectedObj = EventSystem.current.currentSelectedGameObject;

        if(selectedObj == mainBtn)
        {
            MainVolumeUpdate();
        }
        else if(selectedObj == trackBtn)
        {
            TrackVolumeUpdate();
        }
        else if(selectedObj == sfxBtn)
        {
            SfxVolumeUpdate();
        }

    }

    void MainVolumeUpdate()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            mainSlider.value += 0.1f * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            mainSlider.value -= 0.1f * Time.deltaTime;
        }
    }

    void TrackVolumeUpdate()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            trackSlider.value += 0.1f * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            trackSlider.value -= 0.1f * Time.deltaTime;
        }
    }

    void SfxVolumeUpdate()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            sfxSlider.value += 0.1f * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            sfxSlider.value -= 0.1f * Time.deltaTime;
        }
    }

    public void SetMainVolume(float volume)
    {
        main.volume = volume;
        mainLevel.text = (volume*100).ToString("F0") + '%';
        TM.mainVolume = volume;
    }

    public void SetTrackVolume(float volume)
    {
        // track.volume = volume;
        trackLevel.text = (volume*100).ToString("F0") + '%';
        TM.trackVolume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        // sfx.volume = volume;
        sfxLevel.text = (volume*100).ToString("F0") + '%';
        TM.sfxVolume = volume;
    }

    void InitVolume()
    {
        main.volume = TM.mainVolume;
        mainSlider.value = TM.mainVolume;
        mainLevel.text = (TM.mainVolume*100).ToString("F0") + '%';

        // track.volume = initTrack;
        trackSlider.value = TM.trackVolume;
        trackLevel.text = (TM.trackVolume*100).ToString("F0") + '%';

        // sfx.volume = initSfx;
        sfxSlider.value = TM.sfxVolume;
        sfxLevel.text = (TM.sfxVolume*100).ToString("F0") + '%';
    }

}
