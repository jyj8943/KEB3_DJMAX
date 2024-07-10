using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteList : MonoBehaviour
{
    public List<GameObject> noteList = new();

    public GameObject shortNote;

    public Vector3 mousePos;
    public Vector3 targetPos;

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.localPosition = Vector3.zero;

        // ������ ���� ���� ��Ʈ ����
        mousePos = Input.mousePosition;
        targetPos = Camera.main.ScreenToWorldPoint(mousePos + new Vector3(0f, 0f, 10f));

        if (EditorManager.instance.isInsertShortNote && Input.GetMouseButtonDown(0))
        {
            if (targetPos.x >= EditorManager.instance.minNotePosX && targetPos.x <= EditorManager.instance.maxNotePosX 
                && targetPos.y >= EditorManager.instance.minNotePosY)
            {
                correctPosX(targetPos.x);
                var tempNote = Instantiate(shortNote, targetPos, Quaternion.identity);
                tempNote.transform.SetParent(transform, false);
                noteList.Add(tempNote);
            }
        }
    }

    public void correctPosX(float posX)
    {
        if (posX >= 0 && posX < ( EditorManager.instance.maxNotePosX / 2 ))
        {
            targetPos.x = 0.5f;
        }
        else if (posX >= ( EditorManager.instance.maxNotePosX / 2) && posX < EditorManager.instance.maxNotePosX)
        {
            targetPos.x = 1.5f;
        }
        else if (posX >= (EditorManager.instance.minNotePosX / 2) && posX < 0)
        {
            targetPos.x = -0.5f;
        }
        else if (posX >= EditorManager.instance.minNotePosX && posX < ( EditorManager.instance.minNotePosX / 2 ))
        {
            targetPos.x = -1.5f;
        }
        else
            return;
    }

    public void clickInsertButton()
    {
        if (EditorManager.instance.isInsertShortNote)
            EditorManager.instance.isInsertShortNote = false;
        else if (!EditorManager.instance.isInsertShortNote)
            EditorManager.instance.isInsertShortNote = true;
    }

    public void changeNotePosY()
    {
        for (int i = noteList.Count - 1; i >= 0; i--)
        {
            var tempNote = noteList[i];

            tempNote.GetComponent<Transform>().position = new Vector3(tempNote.transform.position.x,
                EditorManager.instance.minNotePosY + tempNote.GetComponent<ShortNote>().defaultDist * 
                EditorManager.instance.userChartSpeed, tempNote.transform.position.x);
        }
    }
}
