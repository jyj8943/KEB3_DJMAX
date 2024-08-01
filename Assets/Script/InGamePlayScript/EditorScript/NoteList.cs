using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NoteList : MonoBehaviour
{
    public List<GameObject> noteList = new();

    public GameObject shortNote;
    public GameObject longNote;

    public Vector3 mousePos;
    public Vector3 targetPos;
    
    public GameObject judgementBar;
    
    public bool isMaking = false;
    public GameObject tempLongNote;
    public GameObject nameCanvas;
    public GameObject pauseCanvas;
    public GameObject helpCanvas;
    //생성할 때 중복으로 노트가 생성되지 않겠금 해야함
    void Update()
    {
        transform.localPosition = Vector3.zero;

        mousePos = Input.mousePosition;
        //mousePos = new Vector3(Input.mousePosition.x, Mathf.Floor(Input.mousePosition.y * 100f) / 100f, Input.mousePosition.z);
        
        var tempTargetPos = Camera.main.ScreenToWorldPoint(mousePos + new Vector3(0f, 0f, 10f));
        targetPos = new Vector3(tempTargetPos.x, Mathf.Round(tempTargetPos.y * 100f) / 100f, tempTargetPos.z);

        if (!nameCanvas.activeSelf && !helpCanvas.activeSelf && !pauseCanvas.activeSelf)
        {
            if (Input.GetMouseButtonDown(0) && EditorManager.instance.isInsertShortNote)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    SnapNotePosY();
                }

                insertShortNote();
            }
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                InsertNoteAtRail(1);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                InsertNoteAtRail(2);
            }
            if (Input.GetKeyDown(KeyCode.Semicolon))
            {
                InsertNoteAtRail(3);
            }
            if (Input.GetKeyDown(KeyCode.Quote))
            {
                InsertNoteAtRail(4);
            }
            
        
            if (Input.GetMouseButtonDown(0) && EditorManager.instance.isInsertLongNote)
            {
                if (!isMaking)
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        SnapNotePosY();
                    }

                    // 첫 번째 클릭
                    InsertLongNoteDown();
                }
                else
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        SnapNotePosY();
                    }

                    //2번째 클릭
                    insertLongNoteUp();
                }
            }

            if (Input.GetMouseButtonDown(0) && EditorManager.instance.isDeleteNote)
            {
                deleteNote();
            }
        }
        

        
    }
    private void InsertNoteAtRail(int railNum)
    {
        float posX = 0f;
       
        switch (railNum)
        {
            case 1:
                posX = -1.5f;
                break;
            case 2:
                posX = -0.5f;
                break;
            case 3:
                posX = 0.5f;
                break;
            case 4:
                posX = 1.5f;
                break;
        }
        targetPos.x = posX;
        targetPos.y = judgementBar.transform.position.y;
        insertShortNote();
    }

    // noteList의 note들을 note의 defaultDist순으로 noteList에 삽입
    public void SortingNotes(GameObject tempNote)
    {
        float defaultDist = 0;
        float noteListDist = 0;
        int noteListNum = -1;

        if (noteList.Count == 0)
            noteList.Add(tempNote);
        else
        {
            if (tempNote.tag == "ShortNote")
                defaultDist = tempNote.GetComponent<ShortNote>().noteStartingTime;
            else if (tempNote.tag == "LongNote")
                defaultDist = tempNote.GetComponent<LongNote>().noteStartingTime;

            for (int i = 0; i < noteList.Count; i++)
            {
                if (noteList[i].tag == "ShortNote")
                    noteListDist = noteList[i].GetComponent<ShortNote>().noteStartingTime;
                else if (noteList[i].tag == "LongNote")
                    noteListDist = noteList[i].GetComponent<LongNote>().noteStartingTime;

                if (defaultDist < noteListDist || defaultDist == noteListDist)
                {
                    noteListNum = i;
                    break;
                }
            }

            if (noteListNum != -1)
                noteList.Insert(noteListNum, tempNote);
            else if (noteListNum == -1)
                noteList.Add(tempNote);
        }
    }

    // 스내핑 기능 -> shortNote에만 반응하도록 구현하였는데 longNote에도 반응해도록 해야함
    // longNote의 경우 위 또는 아래 거리에 따라 달리 해야함
    public void SnapNotePosY()
    {
        if (noteList == null) return;
        
        Debug.Log("snapping");

        var minDist = EditorManager.instance.gridHeight * EditorManager.instance.maxGridCount;
        int checkNote = -1;
        GameObject tempNote = null;

        foreach (var note in noteList)
        {
            if (note.transform.tag == "ShortNote")
            {
                var tempDist = Mathf.Abs(note.transform.position.y - targetPos.y);

                if (tempDist < minDist)
                {
                    minDist = tempDist;
                    tempNote = note;
                    checkNote = 1;
                }
            }
            else if (note.transform.tag == "LongNote")
            {
                var tempDist1 = Mathf.Abs(note.transform.position.y - targetPos.y);
                var tempDist2 = Mathf.Abs(note.transform.position.y + note.transform.localScale.y - targetPos.y);

                if (tempDist1 < tempDist2)
                {
                    if (tempDist1 < minDist)
                    {
                        Debug.Log("tempdist1");
                        minDist = tempDist1;
                        tempNote = note;
                        checkNote = 1;
                    }
                }
                else if (tempDist2 < tempDist1)
                {
                    if (tempDist2 < minDist)
                    {
                        Debug.Log("tempdist2");
                        minDist = tempDist2;
                        tempNote = note;
                        checkNote = 2;
                    }
                }
            }
        }

        switch (checkNote)
        {
            case 1:
                targetPos.y = tempNote.transform.position.y;
                break;
            case 2:
                targetPos.y = tempNote.transform.position.y + tempNote.transform.localScale.y;
                    //* TotalManager.instance.userChartSpeed;
                break;
        }
    }

    // 노트 삭제 기능
    public void deleteNote()
    {
        Vector2 rayPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(rayPoint, Vector2.zero);

        if (hit.transform == null) return;

        if (hit.transform.tag == "ShortNote" || hit.transform.tag == "LongNote")
        {
            Debug.Log(hit.transform.name);

            // Destroy할 때 리스트에 빈 공간이 남아 빈 공간도 제거해야함
            for (int i = noteList.Count - 1; i >= 0; i--)
            {
                if (noteList[i] == hit.transform.gameObject)
                {
                    Destroy(hit.transform.gameObject);
                    noteList.RemoveAt(i);
                }
            }
        }
    }

    public void insertShortNote()
    {
        if (targetPos.x >=TotalManager.instance.minNotePosX && targetPos.x <= TotalManager.instance.maxNotePosX
                && targetPos.y >= TotalManager.instance.minNotePosY)
        {
            correctPosX(targetPos.x);
            var tempNote = Instantiate(shortNote, targetPos, Quaternion.identity);

            tempNote.transform.SetParent(transform, false);

            tempNote.GetComponent<ShortNote>().InitNote();

            //noteList.Add(tempNote);
            SortingNotes(tempNote);
        }
    }

    public void insertLongNoteUp()
    {
        var tempDist = targetPos.y - tempLongNote.transform.position.y;
        tempLongNote.transform.localScale = new Vector3(1f, tempDist, 1f);

        tempLongNote.transform.SetParent(transform, false);
        isMaking = false;

        tempLongNote.GetComponent<LongNote>().InitNote(targetPos.y);

        //noteList.Add(tempLongNote);
        SortingNotes(tempLongNote);
        
        tempLongNote = null;
    }

    public void InsertLongNoteDown()
    {
        // 롱노트를 생성하는 함수 -> 두번째 클릭시 스케일을 변경하도록 짜야할듯

        if (targetPos.x >= TotalManager.instance.minNotePosX && targetPos.x <= TotalManager.instance.maxNotePosX
                                                              && targetPos.y >= TotalManager.instance.minNotePosY)
        {
            correctPosX(targetPos.x);
            tempLongNote = Instantiate(longNote, targetPos, Quaternion.identity);
            isMaking = true;
        }
    }

    public void correctPosX(float posX)
    {
        if (posX >= 0 && posX < ( TotalManager.instance.maxNotePosX / 2 ))
        {
            targetPos.x = 0.5f;
        }
        else if (posX >= ( TotalManager.instance.maxNotePosX / 2) && posX < TotalManager.instance.maxNotePosX)
        {
            targetPos.x = 1.5f;
        }
        else if (posX >= (TotalManager.instance.minNotePosX / 2) && posX < 0)
        {
            targetPos.x = -0.5f;
        }
        else if (posX >= TotalManager.instance.minNotePosX && posX < ( TotalManager.instance.minNotePosX / 2 ))
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
                    TotalManager.instance.minNotePosY + tempNote.GetComponent<ShortNote>().noteStartingTime *
                    TotalManager.instance.finalChartSpeed,
                    tempNote.transform.position.z);
            }
            else if (tempNote.tag == "LongNote")
            {
                tempNote.transform.position = new Vector3(tempNote.transform.position.x,
                    TotalManager.instance.minNotePosY + tempNote.GetComponent<LongNote>().noteStartingTime *
                    TotalManager.instance.finalChartSpeed,
                    tempNote.transform.position.z);

                tempNote.transform.localScale = new Vector3(1f, tempNote.GetComponent<LongNote>().noteHoldingTime * 
                                                                TotalManager.instance.finalChartSpeed, 1f);
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
