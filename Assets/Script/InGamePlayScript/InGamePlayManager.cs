using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Serialization;

public class InGamePlayManager : MonoBehaviour
{
    public static InGamePlayManager instance;

    public List<GameObject> noteList = new();
    //public List<GameObject> 

    //public GameObject shortNotePrefab;
    //public GameObject longNotePrefab;

    void Awake()
    {
        instance = this;
    }

    // 버튼을 누를 때 리스트에 있는 노트들을 순서대로 처리하게끔 구현
    public void JudegeNotes()
    {

    }

    // list 정렬 함수 -> 리스트를 4개로 분할하여 각각 담도록 해야함
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
                defaultDist = tempNote.GetComponent<ShortNote>().defaultDist;
            else if (tempNote.tag == "LongNote")
                defaultDist = tempNote.GetComponent<LongNote>().defaultArrivePosY;

            for (int i = 0; i < noteList.Count; i++)
            {
                if (noteList[i].tag == "ShortNote")
                    noteListDist = noteList[i].GetComponent<ShortNote>().defaultDist;
                else if (noteList[i].tag == "LongNote")
                    noteListDist = noteList[i].GetComponent<LongNote>().defaultArrivePosY;

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
}
