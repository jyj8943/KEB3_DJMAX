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