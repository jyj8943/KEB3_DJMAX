using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNote : ShortNote
{
    //public float upPosY; // 롱노트의 제일 윗부분의 Y값 = PosY + scale.y

    public override void InitNote(float tempUpPos)
    {
        posX = transform.position.x;

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

        noteID = 1;

        var tempPosY = transform.position.y;

        // 처음 클릭 좌표가 두번째 클릭 좌표보다 낮으면 그대로 진행
        if (tempPosY < tempUpPos)
        {
            posY = tempPosY;
            //upPosY = tempUpPos;
        }
        // 처음 클릭 좌표가 두번째 클릭 좌표보다 높으면 반대로 뒤집고 위치 조정
        else if (tempPosY > tempUpPos) 
        {
            posY = tempUpPos;
            //upPosY = tempPosY;

            transform.position = new Vector3(transform.position.x, tempUpPos, transform.position.z);

            transform.localScale = new Vector3(1f, -transform.localScale.y, 1f);

        }

        noteStartingTime = Mathf.Round(((posY - TotalManager.instance.minNotePosY) 
                                        / TotalManager.instance.finalChartSpeed) * 100f) / 100f;
        noteHoldingTime = Mathf.Round((transform.localScale.y / TotalManager.instance.finalChartSpeed) * 100f) / 100f;
    }
    
    public override void SetNoteData(int railNum, int noteID, float noteStartingTime, float noteHoldingTime)
    {
        this.railNum = railNum;
        this.noteID = noteID;
        this.noteStartingTime = noteStartingTime;
        this.noteHoldingTime = noteHoldingTime;
    }

    public override SongData.NoteData GetNoteData()
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
