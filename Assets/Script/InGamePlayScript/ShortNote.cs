using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShortNote : MonoBehaviour {
    public float PosX;
    public float PosY;
    public float arrvieDist; // 판정선까지의 거리

    public int noteID; //noteid는 일반노트가 0, 롱노트가 1
    public float defaultDist; // 1배속일 때의 판정선까지의 거리 구하기 -> 이 값을 데이터 파싱해서 저장하면 되지 않을까

    private void Start()
    {
        PosX = transform.position.x;
        PosY = transform.position.y;
        noteID = 0;
        arrvieDist = PosY - EditorManager.instance.minNotePosY;

        defaultDist = arrvieDist / EditorManager.instance.userChartSpeed;
    }
}
