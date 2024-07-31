using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;

public class InGamePlayManager : MonoBehaviour
{
    public static InGamePlayManager instance;

    public VideoPlayer video;
    public Camera inGameCamera;
    public bool isPlaying = false;

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

    private void Start()
    {
        
    }

    private void Update()
    {
        //NoteMiss();
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isPlaying)
            {
                isPlaying = false;
            }
            else
            {
                isPlaying = true;
            }

            Debug.Log("Video Time: " + video.time);
        }
        if (isPlaying) video.Play();
        else if (!isPlaying) video.Pause();
    }
    
    public void NoteMiss()
    {
        foreach (var noteList in noteListinRail)
        {
            if (noteList.Count == 0) continue;
            
            if (noteList[0].transform.position.y <= judgeBar.transform.position.y - 0.5f)
            {
                // shortNote의 경우 그대로 없어지며 MISS 판정이 뜨지만, longNote의 경우 그대로 내려가며 계속 MiSS가 떠야함
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