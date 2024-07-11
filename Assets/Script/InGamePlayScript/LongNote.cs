using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNote : MonoBehaviour
{
    public float PosX;
    public float PosY; //롱노트의 경우 제일 밑의 y값
    public int noteID; //noteid는 일반노트가 0, 롱노트가 1
    public float arrvieDist; // 판정선까지의 거리
    public float UpPosY;

    private void Start()
    {
        PosX = transform.position.x;
        PosY = transform.position.y;
        noteID = 1;
        arrvieDist = PosY - EditorManager.instance.minNotePosY;
    }
}
