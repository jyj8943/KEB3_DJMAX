using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShortNote : MonoBehaviour {
    public float PosX;
    public float PosY;
    public int noteID; //noteid는 일반노트가 0, 롱노트가 1
    public float arrvieDist; // <- PosY / 2

    private void Start()
    {
        PosX = transform.position.x;
        PosY = transform.position.y;
        noteID = 0;
        arrvieDist = PosY - EditorManager.instance.minNotePosY;
    }

}
