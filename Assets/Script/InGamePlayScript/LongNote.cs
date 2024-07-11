using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNote : MonoBehaviour
{
    public int noteID; //noteid는 일반노트가 0, 롱노트가 1
    public float PosX;
    public float PosY; //롱노트의 경우 제일 밑의 y값 (UpPosY와 비교하여 둘 중 낮은 y값을 PosY로 해야함
    public float UpPosY; // 롱노트의 제일 윗부분의 Y값 = PosY + scale.y - minY

    public float arrivePosYDist; // PosY와 판정선까지의 거리
    public float arriveUpPosYDist; // UpPosY와 판정선까지의 거리

    public float defaultScale;
    public float defaultArrivePosY;
    public float defaultArriveUpPosY;

    public void InitLongNote(float tempUpPos)
    {
        PosX = transform.position.x;
        noteID = 1;

        var tempPosY = transform.position.y;

        // 처음 클릭 좌표가 두번째 클릭 좌표보다 낮으면 그대로 진행
        if (tempPosY < tempUpPos)
        {
            PosY = tempPosY;
            UpPosY = tempUpPos;
        }
        // 처음 클릭 좌표가 두번째 클릭 좌표보다 높으면 반대로 뒤집고 위치 조정
        else if (tempPosY > tempUpPos) 
        {
            PosY = tempUpPos;
            UpPosY = tempPosY;

            transform.position = new Vector3(transform.position.x, tempUpPos, transform.position.z);

            transform.localScale = new Vector3(1f, -transform.localScale.y, 1f);

        }

        arrivePosYDist = PosY - EditorManager.instance.minNotePosY;
        arriveUpPosYDist = UpPosY - EditorManager.instance.minNotePosY;

        defaultScale = transform.localScale.y / EditorManager.instance.userChartSpeed;
        defaultArrivePosY = arrivePosYDist / EditorManager.instance.userChartSpeed;
        defaultArriveUpPosY = arriveUpPosYDist / EditorManager.instance.userChartSpeed;
    }

}
