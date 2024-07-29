using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Serialization;

public class InGamePlayManager : MonoBehaviour
{
    public static InGamePlayManager instance;

    public List<GameObject> noteList = new();
    public List<ShortNote>[] noteListinRail = new List<ShortNote>[]{
        new List<ShortNote>(), new List<ShortNote>(), new List<ShortNote>(), new List<ShortNote>()
    };

    public ShortNote GetFirstNote(int i) => noteListinRail[i][0];

    public GameObject judgeBar;
    //public GameObject shortNotePrefab;
    //public GameObject longNotePrefab;

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        NoteMiss();
    }

     
    
    public void NoteMiss()
    {
        foreach (var noteList in noteListinRail)
        {
            if (noteList.Count == 0) return;
            
            if (noteList[0].transform.position.y <= judgeBar.transform.position.y - 0.5f)
            {
                Debug.Log("MISS");
                noteList[0].gameObject.SetActive(false);
                noteList.RemoveAt(0);
            }
        }
    }
    
    public void DivideList()
    {
        if (noteList == null) return;

        foreach (var note in noteList)
        {
            noteListinRail[note.GetComponent<ShortNote>().railNum - 1].Add(note.GetComponent<ShortNote>());
        }
    }
}

    // list 정렬 함수 -> 이미 에디터에서 정렬을 하기 때문에 필요없을듯
    //public void SortingNotes(GameObject tempNote)
    //{
    //    float defaultDist = 0;
    //    float noteListDist = 0;
    //    int noteListNum = -1;

    //    if (noteList.Count == 0)
    //        noteList.Add(tempNote);
    //    else
    //    {
    //        if (tempNote.GetComponent<ShortNote>().noteID == 0)
    //        {
    //            defaultDist = tempNote.GetComponent<ShortNote>().defaultDist;
    //        }
    //        else if (tempNote.GetComponent<LongNote>().noteID == 1)
    //        {
    //            defaultDist = tempNote.GetComponent<LongNote>().defaultArrivePosY;
    //        }

    //        for (int i = 0; i < noteList.Count; i++)
    //        {
    //            if (tempNote.GetComponent<ShortNote>().noteID == 0)
    //                noteListDist = noteList[i].GetComponent<ShortNote>().defaultDist;
    //            else if (tempNote.GetComponent<LongNote>().noteID == 1)
    //                noteListDist = noteList[i].GetComponent<LongNote>().defaultArrivePosY;

    //            if (defaultDist < noteListDist || defaultDist == noteListDist)
    //            {
    //                noteListNum = i;
    //                break;
    //            }
    //        }

    //        if (noteListNum != -1)
    //            noteList.Insert(noteListNum, tempNote);
    //        else if (noteListNum == -1)
    //            noteList.Add(tempNote);
    //    }
    //}