using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditorCamera : MonoBehaviour
{
    public bool isPlaying = false;

    public HorizontalLineList horizontalLineList;
    public GridList gridList;

    void Start()
    {
        
    }

    void Update()
    {
        // 테스트용 다시시작 버튼
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Editor");
        }

        // 카메라 움직이기
        if (Input.GetKeyDown(KeyCode.W) && transform.position.y < EditorManager.instance.maxGridCount * 8)
        {
            transform.Translate(0f, 8f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.S) && transform.position.y > 0)
        {
            transform.Translate(0f, -8f, 0f);
        }

        // 재생 기능
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isPlaying)
                isPlaying = false;
            else
                isPlaying = true;

        }
        if (isPlaying) transform.Translate(0f, EditorManager.instance.defaultChartSpeed
            * EditorManager.instance.userChartSpeed * Time.deltaTime, 0f);

        // 마우스 휠에 따른 스크롤 기능
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput > 0 && transform.position.y <= EditorManager.instance.maxGridCount * 8)
        {
            transform.Translate(0f, wheelInput * 3, 0f);
        }
        else if (wheelInput < 0 && transform.position.y >= 0)
        {
            transform.Translate(0f, wheelInput * 3, 0f);
        }

        // userChartSpeed를 0.5만큼 빠르게
        if (Input.GetKeyDown(KeyCode.E) && EditorManager.instance.userChartSpeed < EditorManager.instance.maxUserChartSpeed)
        {
            EditorManager.instance.changeUserChartSpeed(0.5f);
            horizontalLineList.changeHorizontalLineHeight();
            gridList.changeGridHeight();
        }
        // userChartSpeed를 0.5만큼 느리게
        if (Input.GetKeyDown(KeyCode.D) && EditorManager.instance.userChartSpeed > 1f)
        {
            EditorManager.instance.changeUserChartSpeed(-0.5f);
            horizontalLineList.changeHorizontalLineHeight();
            gridList.changeGridHeight();
        }
    }
}
