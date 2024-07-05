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
        // �ʱ�ȭ
        gridCount = 0;
    }

    void Update()
    {
        /*// �׸��� ��ĭ �ø��� -> ����ī�޶� �̵��ϵ��� ���� ī�޶� ��ũ��Ʈ �ۼ�
        if (Input.GetKeyDown(KeyCode.W) && gridCount < EditorManager.instance.maxGridCount)
        {
            gridList.transform.Translate(0f, -8f, 0f);
            gridCount++;
        }
        // �׸��� ��ĭ ������ -> ����ī�޶� �̵��ϵ��� ���� ī�޶� ��ũ��Ʈ �ۼ�
        if (Input.GetKeyDown(KeyCode.S) && gridCount > 0)
        {
            gridList.transform.Translate(0f, 8f, 0f);
            gridCount--;
        }*/

        // �׸��� �⺻ �ӵ��� ��� �� ���� -> ����ī�޶� �̵��ϵ��� ���� ī�޶� ��ũ��Ʈ �ۼ�
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    if (isPlaying) 
        //        isPlaying = false;
        //    else 
        //        isPlaying = true;

        //}
        //if (isPlaying) gridList.transform.Translate(0f, -EditorManager.instance.defaultChartSpeed 
        //    * EditorManager.instance.userChartSpeed * Time.deltaTime, 0f);

        // ���콺 �ٿ� ���� ��ũ�� ��� -> ����ī�޶� �̵��ϵ��� ���� ī�޶� ��ũ��Ʈ �ۼ�
        float wheelInput = Input.GetAxis("Mouse ScrollWheel"); 
        if (wheelInput > 0 && gridCount < EditorManager.instance.maxGridCount )
        {
            gridList.transform.Translate(0f, -wheelInput * 3, 0f);
        }
        else if (wheelInput < 0 && gridCount >= 0 ) 
        {
            gridList.transform.Translate(0f, -wheelInput * 3, 0f);
        }

        //�׸��� ī��Ʈ ��� -> ����ī�޶� �̵��ϵ��� ���� ī�޶� ��ũ��Ʈ �ۼ�
        gridCount = -gridList.transform.position.y / (8 * EditorManager.instance.userChartSpeed);

        // ������ ��ӿ� ���� �׸����� ���� �籸��

    }
}
