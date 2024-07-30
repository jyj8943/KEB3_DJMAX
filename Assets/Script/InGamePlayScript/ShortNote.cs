using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShortNote : MonoBehaviour {
    public float posX;
    public float posY;
    // public float arrvieDist; // 판정선까지의 거리
    //
    // public float defaultDist; // 1배속일 때의 판정선까지의 거리 구하기 -> 이 값을 데이터 파싱해서 저장하면 되지 않을까

    // shortNote에 저장되어야할 정보: railNum, noteID, 눌러야하는 시간
    // longNote에 저장되어야할 정보(shortNote 상속): 끝까지 눌러야 할 시간
    public int railNum;
    public int noteID; //noteid는 일반노트가 0, 롱노트가 1
    public float noteStartingTime; // 노트를 눌러야할 시간
    public float noteHoldingTime; // 롱노트에서 노트를 끝까지 눌러야할 시간
    
    public virtual void InitNote(float tempUpPos = 0f)
    {
        posX = transform.position.x;
        posY = transform.position.y;
        noteID = 0;
        //arrvieDist = PosY - TotalManager.instance.minNotePosY; //EditorManager.instance.minNotePosY;

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

        //defaultDist = arrvieDist / TotalManager.instance.userChartSpeed; //EditorManager.instance.userChartSpeed;

        noteStartingTime = (posY - TotalManager.instance.minNotePosY) / TotalManager.instance.finalChartSpeed;
        noteHoldingTime = 0f;
    }

    public virtual SongData.NoteData GetNoteData()
    {
        return new SongData.NoteData
        {
            railNum = railNum,
            noteID = noteID,
            noteStartingTime = noteStartingTime,
            noteHoldingTime = noteHoldingTime
            // posY = PosY,
            // scale = transform.localScale.y,
            // defaultDist = defaultDist,
            // distUpPosY = defaultDist
        };
    }
}
