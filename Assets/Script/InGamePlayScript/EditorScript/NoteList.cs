using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteList : MonoBehaviour
{
    public List<GameObject> noteList = new();

    public GameObject shortNote;
    public GameObject longNote;

    public Vector3 mousePos;
    public Vector3 targetPos;

    public bool isMaking = false;
    public GameObject tempLongNote;
    
    void Update()
    {
        transform.localPosition = Vector3.zero;

        mousePos = Input.mousePosition;
        targetPos = Camera.main.ScreenToWorldPoint(mousePos + new Vector3(0f, 0f, 10f));

        if (Input.GetMouseButtonDown(0) && EditorManager.instance.isInsertShortNote)
        {
            insertShortNote();
        }

        if (Input.GetMouseButtonDown(0) && EditorManager.instance.isInsertLongNote)
        {
            if (!isMaking) 
                // 첫 번째 클릭
                InsertLongNoteDown();
            else 
                //2번째 클릭
                insertLongNoteUp();
        }

        if (Input.GetMouseButtonDown(0) && EditorManager.instance.isDeleteNote)
        {
            deleteNote();
        }
    }

    public void deleteNote()
    {
        Vector2 rayPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(rayPoint, Vector2.zero);

        if (hit.transform == null) return;

        if (hit.transform.tag == "ShortNote" || hit.transform.tag == "LongNote")
        {
            Debug.Log(hit.transform.name);
            Destroy(hit.transform.gameObject);
        }
        
    }

    public void insertShortNote()
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

    public void insertLongNoteUp()
    {
        var tempDist = targetPos.y - tempLongNote.transform.position.y;
        tempLongNote.transform.localScale = new Vector3(1f, tempDist, 1f);

        tempLongNote.transform.SetParent(transform, false);
        isMaking = false;

        noteList.Add(tempLongNote);

        tempLongNote.GetComponent<LongNote>().InitLongNote(targetPos.y);

        tempLongNote = null;
    }

    public void InsertLongNoteDown()
    {
        // 롱노트를 생성하는 함수 -> 두번째 클릭시 스케일을 변경하도록 짜야할듯

        if (targetPos.x >= EditorManager.instance.minNotePosX && targetPos.x <= EditorManager.instance.maxNotePosX
                && targetPos.y >= EditorManager.instance.minNotePosY)
        {
            correctPosX(targetPos.x);
            tempLongNote = Instantiate(longNote, targetPos, Quaternion.identity);
            isMaking = true;
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

    public void changeNotePosY()
    {
        for (int i = noteList.Count - 1; i >= 0; i--)
        {
            var tempNote = noteList[i];

            if (tempNote.tag == "ShortNote")
            {
                tempNote.GetComponent<Transform>().position = new Vector3(tempNote.transform.position.x,
                        EditorManager.instance.minNotePosY + tempNote.GetComponent<ShortNote>().defaultDist *
                        EditorManager.instance.userChartSpeed, tempNote.transform.position.z);
            }
            else if (tempNote.tag == "LongNote")
            {
                tempNote.transform.position = new Vector3(tempNote.transform.position.x,
                    EditorManager.instance.minNotePosY + tempNote.GetComponent<LongNote>().defaultArrivePosY *
                    EditorManager.instance.userChartSpeed, tempNote.transform.position.z);

                tempNote.transform.localScale = new Vector3(1f, tempNote.GetComponent<LongNote>().defaultScale *
                    EditorManager.instance.userChartSpeed, 1f);
            }
        }
    }

    public void clickInsertLongNoteButton()
    {
        if (EditorManager.instance.isInsertLongNote)
        {
            EditorManager.instance.isInsertLongNote = false;

            EditorManager.instance.isInsertShortNote = false;
            EditorManager.instance.isDeleteNote = false;
        }
        else if (!EditorManager.instance.isInsertLongNote)
        {
            EditorManager.instance.isInsertLongNote = true;

            EditorManager.instance.isInsertShortNote = false;
            EditorManager.instance.isDeleteNote = false;
        }
    }

    public void clickInsertButton()
    {
        if (EditorManager.instance.isInsertShortNote)
        {
            EditorManager.instance.isInsertShortNote = false;

            EditorManager.instance.isInsertLongNote = false;
            EditorManager.instance.isDeleteNote = false;
        }
        else if (!EditorManager.instance.isInsertShortNote)
        {
            EditorManager.instance.isInsertShortNote = true;

            EditorManager.instance.isInsertLongNote = false;
            EditorManager.instance.isDeleteNote = false;
        }
    }

    public void clickDeleteButton()
    {
        if (EditorManager.instance.isDeleteNote)
        {
            EditorManager.instance.isDeleteNote = false;

            EditorManager.instance.isInsertShortNote = false;
            EditorManager.instance.isInsertLongNote = false;
        }
        else if (!EditorManager.instance.isDeleteNote)
        {
            EditorManager.instance.isDeleteNote = true;

            EditorManager.instance.isInsertShortNote = false;
            EditorManager.instance.isInsertLongNote = false;
        }
    }
}
