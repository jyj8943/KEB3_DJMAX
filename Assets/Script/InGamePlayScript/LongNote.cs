using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNote : ShortNote
{
    //public float PosX;
    //public float PosY; //롱노트의 경우 제일 밑의 y값 (UpPosY와 비교하여 둘 중 낮은 y값을 PosY로 해야함
    public float upPosY; // 롱노트의 제일 윗부분의 Y값 = PosY + scale.y

    //public float arrivePosYDist; // PosY와 판정선까지의 거리
    //public float arriveUpPosYDist; // UpPosY와 판정선까지의 거리

    //public int railNum;
    //public int noteID; //noteid는 일반노트가 0, 롱노트가 1
    // public float defaultScale;
    // public float defaultArrivePosY;
    // public float defaultArriveUpPosY;
    // public float defaultUpPosY;
    
    // public int railNum;
    // public int noteID; //noteid는 일반노트가 0, 롱노트가 1
    // public float noteStartingTime; // 노트를 눌러야할 시간
    // public float noteHoldingTime; // 롱노트에서 노트를 끝까지 눌러야할 시간

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
            upPosY = tempUpPos;
        }
        // 처음 클릭 좌표가 두번째 클릭 좌표보다 높으면 반대로 뒤집고 위치 조정
        else if (tempPosY > tempUpPos) 
        {
            posY = tempUpPos;
            upPosY = tempPosY;

            transform.position = new Vector3(transform.position.x, tempUpPos, transform.position.z);

            transform.localScale = new Vector3(1f, -transform.localScale.y, 1f);

        }

        noteStartingTime = (posY - TotalManager.instance.minNotePosY) / TotalManager.instance.finalChartSpeed;
        noteHoldingTime = transform.localScale.y / TotalManager.instance.finalChartSpeed;

        // arrivePosYDist = posY - TotalManager.instance.minNotePosY;
        // arriveUpPosYDist = arrivePosYDist + transform.localScale.y;
        //
        // defaultScale = transform.localScale.y / TotalManager.instance.userChartSpeed;
        // defaultArrivePosY = arrivePosYDist / TotalManager.instance.userChartSpeed;
        // defaultArriveUpPosY = arriveUpPosYDist / TotalManager.instance.userChartSpeed;
        //
        // defaultUpPosY = TotalManager.instance.minNotePosY + defaultArriveUpPosY;
    }

    public override SongData.NoteData GetNoteData()
    {
        return new SongData.NoteData
        {
            railNum = railNum,
            noteID = noteID,
            noteStartingTime = noteStartingTime,
            noteHoldingTime = noteHoldingTime
            // posY = PosY,
            // scale = defaultScale,
            // defaultDist = defaultArrivePosY,
            // distUpPosY = defaultArriveUpPosY
        };
    }
}
