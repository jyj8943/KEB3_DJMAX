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
    private bool isPassed = true;
    private float judgeInterval = 0f;

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
    
    // 유저가 노트를 아예 놓치는 경우 발생하는 MISS에 대해 판정
    private void NoteMiss()
    {
        foreach (var noteList in noteListinRail)
        {
            if (noteList.Count == 0) continue;
            
            if ( video.time >= noteList[0].noteStartingTime + 0.25f)
            {
                if (noteList[0].noteID == 0)
                {
                    // shortNote의 경우 그대로 없어지며 MISS 판정이 뜨지만, longNote의 경우 그대로 내려가며 계속 MiSS가 떠야함
                    Debug.Log("ShortNote MISS");
                    noteList[0].gameObject.SetActive(false);
                    noteList.RemoveAt(0);
                }
                else if (noteList[0].noteID == 1)
                {
                    if (isPassed) 
                    {
                        Debug.Log("LongNote Starting MISS");
                        isPassed = false;
                    }
                    
                    if (video.time >= noteList[0].noteStartingTime + noteList[0].noteHoldingTime + 0.25f)
                    {
                        Debug.Log("LongNote End MISS");
                        noteList[0].gameObject.SetActive(false);
                        noteList.RemoveAt(0);
                        isPassed = true;
                    }
                }
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