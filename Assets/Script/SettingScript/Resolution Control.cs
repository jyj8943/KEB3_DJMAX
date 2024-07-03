using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ResolutionControl : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;  // 드롭다운 UI
    private Resolution[] resolutions;

    void Start()
    {
        // 지원하는 해상도 배열 가져오기
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
    
        // 해상도 옵션 목록 생성
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // 현재 해상도와 일치하는 옵션 찾기
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();
    
        SetResolution(resolutionDropdown.value);

        // 드롭다운 값 변경 시 SetResolution 호출
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", index);  // 해상도 인덱스를 저장
    }
}
