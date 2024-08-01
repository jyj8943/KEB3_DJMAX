using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShortNote : MonoBehaviour {
    public float posX;
    public float posY;

    public int railNum;
    public int noteID; //noteid는 일반노트가 0, 롱노트가 1
    public float noteStartingTime; // 노트를 눌러야할 시간
    public float noteHoldingTime; // 롱노트에서 노트를 끝까지 눌러야할 시간
    
    public virtual void InitNote(float tempUpPos = 0f)
    {
        posX = transform.position.x;
        posY = transform.position.y;
        noteID = 0;

        switch (posX)
        {
            case (-1.5f):
                railNum = 1;
                break;
            case (-0.5f):
                railNum = 2;
                break;
            case (0.5f):
                railNum = 3;
                break;
            case (1.5f):
                railNum = 4;
                break;
        }
        
        noteStartingTime = Mathf.Round(((posY - TotalManager.instance.minNotePosY) / 
                                         TotalManager.instance.finalChartSpeed) * 100f) / 100f;
        noteHoldingTime = 0f;
    }

    public virtual void SetNoteData(int railNum, int noteID, float noteStartingTime, float noteHoldingTime)
    {
        this.railNum = railNum;
        this.noteID = noteID;
        this.noteStartingTime = noteStartingTime;
        this.noteHoldingTime = noteHoldingTime;
    }
    
    public virtual SongData.NoteData GetNoteData()
    {
        return new SongData.NoteData
        {
            railNum = railNum,
            noteID = noteID,
            noteStartingTime = noteStartingTime,
            noteHoldingTime = noteHoldingTime
        };
    }
}
