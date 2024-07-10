using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnFuction : MonoBehaviour
{
    public ButtonType currentType;

    public void OnBtnClick()
    {
        switch (currentType)
        {
            case ButtonType.SelectMusic:
                Debug.Log("곡 선택");
                //SceneManager.LoadScene("InGamePlay");
                break;
                
            case ButtonType.Retry:
                Debug.Log("다시시작");
                SceneManager.LoadScene("TitleScreen");
                break;

            case ButtonType.MainMenu:
                Debug.Log("메인메뉴");
                //SceneManager.LoadScene("Setting");
                break;
        }

    }
}
