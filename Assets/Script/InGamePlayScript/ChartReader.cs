using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChartReader : MonoBehaviour
{
    public GameObject chart;
    public GameObject grids;
    public float gridCount;
    public float maxGridCount;

    public bool isPlaying = false;
    public float chartSpeed;

    void Awake()
    {
        //초기화
        gridCount = 0f;
        maxGridCount = 60f;
    }

    void Update()
    {
        //테스트용 다시시작 버튼
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Editor");
        }

        //그리드 한칸 올리기
        if (Input.GetKeyDown(KeyCode.W) && gridCount <= maxGridCount)
        {
            grids.transform.Translate(0f, -8f, 0f);
            gridCount++;
        }
        //그리드 한칸 내리기
        if (Input.GetKeyDown(KeyCode.S) && gridCount > 0)
        {
            grids.transform.Translate(0f, 8f, 0f);
            gridCount--;
        }

        //그리드 1배속으로 재생
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isPlaying) 
                isPlaying = false;
            else 
                isPlaying = true;
        }
        if (isPlaying)
        {
            grids.transform.Translate(0f, -chartSpeed * Time.deltaTime, 0f);
        }
    }
}
