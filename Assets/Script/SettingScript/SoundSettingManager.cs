using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingManager : MonoBehaviour
{
    public TotalManager TM;

    public AudioSource bgm;
    public TextMeshProUGUI bgmLevel;
    public Slider bgmSlider;
    public static float initBgm = 0.5f;
    
    public AudioSource track;
    public TextMeshProUGUI trackLevel;
    public Slider trackSlider;
    public static float initTrack = 0.5f;

    public AudioSource sfx;
    public TextMeshProUGUI sfxLevel;
    public Slider sfxSlider;
    public static float initSfx = 0.5f;

    void Awake()
    {
        TM = TotalManager.instance;
        InitVolume();
    }

    public void SetBgmVolume(float volume)
    {
        bgm.volume = volume;
        bgmLevel.text = (volume*100).ToString("F0") + '%';
        initBgm = volume;
    }

    public void SetTrackVolume(float volume)
    {
        // track.volume = volume;
        trackLevel.text = (volume*100).ToString("F0") + '%';
        initTrack = volume;
    }

    public void SetSfxVolume(float volume)
    {
        // sfx.volume = volume;
        sfxLevel.text = (volume*100).ToString("F0") + '%';
        initSfx = volume;
    }

    void InitVolume()
    {
        bgm.volume = initBgm;
        bgmSlider.value = initBgm;
        bgmLevel.text = (initBgm*100).ToString("F0") + '%';

        // track.volume = initTrack;
        trackSlider.value = initTrack;
        trackLevel.text = (initTrack*100).ToString("F0") + '%';

        // sfx.volume = initSfx;
        sfxSlider.value = initSfx;
        sfxLevel.text = (initSfx*100).ToString("F0") + '%';
    }

}
