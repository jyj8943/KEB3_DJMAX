using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayModeControl : MonoBehaviour
{
    public TMP_Dropdown screenModeDropdown;  // 화면 모드 드롭다운 UI

    void Start()
    {
        // 화면 모드 옵션 생성 및 설정
        List<string> screenModeOptions = new List<string> { "전체 화면", "창 모드", "테두리 없는 창 모드" };
        screenModeDropdown.ClearOptions();
        screenModeDropdown.AddOptions(screenModeOptions);
        screenModeDropdown.value = PlayerPrefs.GetInt("ScreenModeIndex", 0);
        screenModeDropdown.RefreshShownValue();

        SetScreenMode(screenModeDropdown.value);

        // 드롭다운 값 변경 시 SetScreenMode 호출
        screenModeDropdown.onValueChanged.AddListener(SetScreenMode);
    }

    void SetScreenMode(int index)
    {
        
        switch (index)
        {
            case 0: // 전체 화면
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1: // 창 모드
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            case 2: // 테두리 없는 창 모드
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
        }
        PlayerPrefs.SetInt("ScreenModeIndex", index);  // 화면 모드 인덱스를 저장
    }
}