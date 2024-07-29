using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.tvOS;

public class Button : MonoBehaviour
{
    public GameObject buttonEffect;
    public KeyCode Key;

    public SpriteRenderer buttonImage;
    public Sprite upImage;
    public Sprite downImage;

    public int clickedRailNum;

    private void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            buttonEffect.SetActive(true);
            buttonImage.sprite = downImage;
            
            JudgeNotes();
        }

        if (Input.GetKeyUp(Key))
        {
            buttonEffect.SetActive(false);
            buttonImage.sprite = upImage;
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

        if (InGamePlayManager.instance.noteListinRail[clickedRailNum].Count > 0 )
        { 
            JudgeAccuracy(InGamePlayManager.instance.GetFirstNote(clickedRailNum));
            
            InGamePlayManager.instance.GetFirstNote(clickedRailNum).gameObject.SetActive(false);
            InGamePlayManager.instance.noteListinRail[clickedRailNum].RemoveAt(0);
        }
        else
        {
            return;
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void JudgeAccuracy(ShortNote tempNote)
    {
        // 버튼을 눌렀을 때의 시간을 ( 판정선의 y값 / 기본 속도 * 배속 )으로 계산
        // ( 노트의 defaultDist / 기본 속도 )와 비교하여 판정 처리
        switch (tempNote.noteID)
        {
            case (0):
            {
                var judgeTime = (InGamePlayManager.instance.judgeBar.transform.position.y -
                                 TotalManager.instance.minNotePosY)
                                / (TotalManager.instance.defaultChartSpeed * TotalManager.instance.userChartSpeed);
                var noteTime = tempNote.defaultDist / TotalManager.instance.defaultChartSpeed;

                if (judgeTime >= noteTime - 0.15f && judgeTime <= noteTime + 0.04f)
                {
                    Debug.Log("judgeTime: " + judgeTime);
                    Debug.Log("noteTime: " + noteTime);
                    Debug.Log("PERFECT");
                }
                break;
            }
            case (1):
            {
                return;
            }
        }
    }
}