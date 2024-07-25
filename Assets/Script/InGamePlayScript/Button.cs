using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Button : MonoBehaviour
{
    public GameObject buttonEffect;
    public KeyCode Key;

    public SpriteRenderer buttonImage;
    public Sprite upImage;
    public Sprite downImage;

    private bool isKeyDown = false;

    public int clickedRailNum;

    void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            buttonEffect.SetActive(true);
            buttonImage.sprite = downImage;

            // 노트에 대한 판정 제작중
            isKeyDown = true;

            if (isKeyDown)
                JudgeNotes();
        }
        
        if(Input.GetKeyUp(Key))
        {
            buttonEffect.SetActive(false);
            buttonImage.sprite = upImage;

            isKeyDown = false;
        }
    }

    public void JudgeNotes()
    {
        switch (Key)
        {
            case (KeyCode.A):
                {
                    clickedRailNum = 0;
                    break;
                }
            case (KeyCode.S):
                {
                    clickedRailNum = 1;
                    break;
                }
            case (KeyCode.Semicolon):
                {
                    clickedRailNum = 2;
                    break;
                }
            case (KeyCode.Quote):
                {
                    clickedRailNum = 3;
                    break;
                }
        }

        InGamePlayManager.instance.noteListinRail[clickedRailNum][0].gameObject.SetActive(false);

        //JudgeAccuracy(InGamePlayManager.instance.noteListinRail[clickedRailNum][0].gameObject);

        InGamePlayManager.instance.noteListinRail[clickedRailNum].RemoveAt(0);
    }

    public void JudgeAccuracy(GameObject tempNote)
    {
        // 판정선과 노트의 거리가 0이면 Perfect 판정이 발생하도록 구현
    }
}