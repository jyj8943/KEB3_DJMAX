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
        //�ʱ�ȭ
        gridCount = 0f;
        maxGridCount = 60f;
    }

    void Update()
    {
        //�׽�Ʈ�� �ٽý��� ��ư
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Editor");
        }

        //�׸��� ��ĭ �ø���
        if (Input.GetKeyDown(KeyCode.W) && gridCount <= maxGridCount)
        {
            grids.transform.Translate(0f, -8f, 0f);
            gridCount++;
        }
        //�׸��� ��ĭ ������
        if (Input.GetKeyDown(KeyCode.S) && gridCount > 0)
        {
            grids.transform.Translate(0f, 8f, 0f);
            gridCount--;
        }

        //�׸��� 1������� ���
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
