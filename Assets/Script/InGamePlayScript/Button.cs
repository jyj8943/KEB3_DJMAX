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
    private int clickedRailNum;
    private ShortNote note;
    
    private bool isHolding = false;
    private float judgeTime = 0f;
    //private String FirstJudgeLongNote;
    private float holdingTime = 0f;
    
    public SpriteRenderer buttonImage;
    public Sprite upImage;
    public Sprite downImage;

    private static Dictionary<KeyCode, int> keyCodeToNum = new()
    {
        { KeyCode.A, 0 },
        { KeyCode.S, 1 },
        { KeyCode.Semicolon, 2 },
        { KeyCode.Quote, 3 },
    };

    private void Awake()
    {
        clickedRailNum = keyCodeToNum[Key];
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(Key))
    //     {
    //         buttonEffect.SetActive(true);
    //         buttonImage.sprite = downImage;
    //
    //         isHolding = true;
    //         JudgeNotes();
    //     }
    //     
    //     if (isHolding && note != null)
    //     {
    //         if (note.noteID == 1)
    //         // 롱노트 첫 입력 이후 홀드 시의 판정 처리
    //             JudgeLongNoteHolding(note as LongNote);
    //     }
    //     
    //     if (Input.GetKeyUp(Key))
    //     {
    //         buttonEffect.SetActive(false);
    //         buttonImage.sprite = upImage;
    //
    //         isHolding = false;
    //         
    //         // 롱노트의 마지막 입력 판정 처리
    //         if (note != null && note.noteID == 1)
    //             JudgeLongNoteEnd(note as LongNote);
    //     }
    // }
    //
    // public void JudgeNotes()
    // {
    //     if (InGamePlayManager.instance.noteListinRail[clickedRailNum].Count == 0)
    //         return;
    //
    //     note = InGamePlayManager.instance.GetFirstNote(clickedRailNum);
    //     switch (note.noteID)
    //     {
    //         case 0:
    //         {
    //             JudgeShortNoteAccuracy(note);
    //             
    //             note.gameObject.SetActive(false);
    //             note = null;
    //             
    //             InGamePlayManager.instance.noteListinRail[clickedRailNum].RemoveAt(0);
    //
    //             break;
    //         }
    //         case 1:
    //         {
    //             JudgeLongNoteAccuracy(note as LongNote);
    //                 
    //             break;
    //         }
    //     }
    // }
    //
    //  public void JudgeShortNoteAccuracy(ShortNote tempNote)
    //  {
    //     // 버튼을 눌렀을 때의 시간을 ( 판정선의 y값 / 기본 속도 * 배속 )으로 계산
    //     // ( 노트의 defaultDist / 기본 속도 )와 비교하여 판정 처리
    //     judgeTime = GetJudgeTime();
    //     var noteTime = tempNote.defaultDist / TotalManager.instance.defaultChartSpeed;
    //
    //     if (judgeTime >= noteTime - 0.15f && judgeTime <= noteTime + 0.04f)
    //     {
    //         Debug.Log("judgeTime: " + judgeTime);
    //         Debug.Log("noteTime: " + noteTime);
    //         Debug.Log("PERFECT");
    //     }
    //     // else if (~) {~} 세부 판정 작성해야함
    //  }
    //
    //  public void JudgeLongNoteAccuracy(LongNote tempNote)
    //  {
    //      judgeTime = GetJudgeTime();
    //      var noteTime = tempNote.defaultArrivePosY / TotalManager.instance.defaultChartSpeed;
    //      
    //      if (judgeTime >= noteTime - 0.15f && judgeTime <= noteTime + 0.04f)
    //      {
    //          Debug.Log("judgeTime: " + judgeTime);
    //          Debug.Log("noteTime: " + noteTime);
    //          Debug.Log("PERFECT");
    //          //FirstJudgeLongNote = "PERFECT";
    //      }
    //  }
    //
    //  public void JudgeLongNoteHolding(LongNote tempNote)
    //  {
    //      holdingTime += Time.deltaTime;
    //      if (holdingTime >= 0.1f)
    //      {
    //          Debug.Log("PASS");
    //
    //          holdingTime = 0;
    //      }
    //
    //      note.transform.localScale =
    //          new Vector3(1f, tempNote.defaultArriveUpPosY - InGamePlayManager.instance.judgeBar.transform.position.y, 1f);
    //      note.transform.position = new Vector3(note.transform.position.x,
    //          InGamePlayManager.instance.judgeBar.transform.position.y, note.transform.position.z);
    //  }
    //
    //  public void JudgeLongNoteEnd(LongNote tempNote)
    //  {
    //      judgeTime = GetJudgeTime();
    //      var noteTime = tempNote.defaultArriveUpPosY / TotalManager.instance.defaultChartSpeed;
    //      
    //      if (judgeTime >= noteTime - 0.15f && judgeTime <= noteTime + 0.04f)
    //      {
    //          Debug.Log("judgeTime: " + judgeTime);
    //          Debug.Log("noteTime: " + noteTime);
    //          Debug.Log("PERFECT");
    //      }
    //      
    //      note.gameObject.SetActive(false);
    //      note = null;
    //      InGamePlayManager.instance.noteListinRail[clickedRailNum].RemoveAt(0);
    //  }
    //
    //  private float GetJudgeTime()
    //  {
    //      return (InGamePlayManager.instance.judgeBar.transform.position.y -
    //              TotalManager.instance.minNotePosY)
    //             / (TotalManager.instance.defaultChartSpeed * TotalManager.instance.userChartSpeed);
    //  }
}