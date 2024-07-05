using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChartReader : MonoBehaviour
{

    public GameObject chart;
    public GameObject gridList;
    public float gridCount;

    public bool isPlaying = false;
    
    void Awake()
    {
        // 초기화
        gridCount = 0;
    }

    void Update()
    {
        /*// 그리드 한칸 올리기 -> 메인카메라가 이동하도록 메인 카메라에 스크립트 작성
        if (Input.GetKeyDown(KeyCode.W) && gridCount < EditorManager.instance.maxGridCount)
        {
            gridList.transform.Translate(0f, -8f, 0f);
            gridCount++;
        }
        // 그리드 한칸 내리기 -> 메인카메라가 이동하도록 메인 카메라에 스크립트 작성
        if (Input.GetKeyDown(KeyCode.S) && gridCount > 0)
        {
            gridList.transform.Translate(0f, 8f, 0f);
            gridCount--;
        }*/

        // 그리드 기본 속도로 재생 및 멈춤 -> 메인카메라가 이동하도록 메인 카메라에 스크립트 작성
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    if (isPlaying) 
        //        isPlaying = false;
        //    else 
        //        isPlaying = true;

        //}
        //if (isPlaying) gridList.transform.Translate(0f, -EditorManager.instance.defaultChartSpeed 
        //    * EditorManager.instance.userChartSpeed * Time.deltaTime, 0f);

        // 마우스 휠에 따른 스크롤 기능 -> 메인카메라가 이동하도록 메인 카메라에 스크립트 작성
        float wheelInput = Input.GetAxis("Mouse ScrollWheel"); 
        if (wheelInput > 0 && gridCount < EditorManager.instance.maxGridCount )
        {
            gridList.transform.Translate(0f, -wheelInput * 3, 0f);
        }
        else if (wheelInput < 0 && gridCount >= 0 ) 
        {
            gridList.transform.Translate(0f, -wheelInput * 3, 0f);
        }

        //그리드 카운트 계산 -> 메인카메라가 이동하도록 메인 카메라에 스크립트 작성
        gridCount = -gridList.transform.position.y / (8 * EditorManager.instance.userChartSpeed);

        // 유저의 배속에 따라 그리드의 높이 재구성

    }
}
