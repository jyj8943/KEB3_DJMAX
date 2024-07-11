using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditorCamera : MonoBehaviour
{
    public bool isPlaying = false;

    public HorizontalLineList horizontalLineList;
    public GridList gridList;
    public NoteList noteList;

    public AudioManager audioManager;

    void Start()
    {
        
    }

    void Update()
    {
        // �׽�Ʈ�� �ٽý��� ��ư
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Editor");
        }

        // ī�޶� �����̱�
        if (Input.GetKeyDown(KeyCode.W) && transform.position.y < EditorManager.instance.maxGridCount * 8)
        {
            transform.Translate(0f, 8f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.S) && transform.position.y > 0)
        {
            transform.Translate(0f, -8f, 0f);
        }

        // ��� ���
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isPlaying)
            {
                isPlaying = false;
                audioManager.pauseSong();
            }
            else
            {
                isPlaying = true;
                audioManager.playSong();
            }

        }
        if (isPlaying) transform.Translate(0f, EditorManager.instance.defaultChartSpeed
            * EditorManager.instance.userChartSpeed * Time.deltaTime, 0f);

        // ���콺 �ٿ� ���� ��ũ�� ���
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput > 0 && transform.position.y < EditorManager.instance.maxGridCount * 8 - 7)
        {
            transform.Translate(0f, wheelInput * 3, 0f);
        }
        else if (wheelInput < 0 && transform.position.y > 0)
        {
            transform.Translate(0f, wheelInput * 3, 0f);
        }

        // userChartSpeed�� 0.5��ŭ ������
        if (Input.GetKeyDown(KeyCode.E) && EditorManager.instance.userChartSpeed < EditorManager.instance.maxUserChartSpeed)
        {
            EditorManager.instance.changeUserChartSpeed(0.5f);
            horizontalLineList.changeHorizontalLineHeight();
            gridList.changeGridHeight();
            noteList.changeNotePosY();
        }
        // userChartSpeed�� 0.5��ŭ ������
        if (Input.GetKeyDown(KeyCode.D) && EditorManager.instance.userChartSpeed > 1f)
        {
            EditorManager.instance.changeUserChartSpeed(-0.5f);
            horizontalLineList.changeHorizontalLineHeight();
            gridList.changeGridHeight();
            noteList.changeNotePosY();
        }

        //�뷡 ��� ��ũ��Ʈ�� �ϴ� ī�޶� ���� ���߿� ���� ��ũ��Ʈ �ۼ� �ʿ��ҵ�

    }
}
