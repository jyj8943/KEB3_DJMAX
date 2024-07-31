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
    private float holdingTime = 0f;
    private float judgeInterval = 0f;
    
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

     private void Update()
     {
         // 인게임에 비디오를 구현해놨으니 비디오의 time(현재 재생 시간) 속성을 활용하여 정확도를 판별하도록 구현 예정
         
         if (Input.GetKeyDown(Key))
         {
             buttonEffect.SetActive(true);
             buttonImage.sprite = downImage;
    
             Debug.Log("Key: " + Key + ", Time: " + InGamePlayManager.instance.video.time);
             isHolding = true;
             JudgeNotes();
         }
         
         if (isHolding && note != null)
         {
             holdingTime += Time.deltaTime;
             judgeInterval += Time.deltaTime;
             ScalingLongNote(note as LongNote);
             
             // 0.5초마다 누르는 도중의 판정이 이뤄지도록 작성
             if (note.noteID == 1 && judgeInterval >= 0.5f)
             {
                 Debug.Log("JudgeInterval: " + judgeInterval);
                 // 롱노트 첫 입력 이후 홀드 시의 판정 처리
                 JudgeLongNoteHolding(note as LongNote);
                 judgeInterval = 0f;
             }
         }
         
         if (Input.GetKeyUp(Key))
         {
             buttonEffect.SetActive(false);
             buttonImage.sprite = upImage;
             
             // 롱노트의 마지막 입력 판정 처리
             if (note != null && note.noteID == 1)
                 JudgeLongNoteEnd(note as LongNote);
             
             isHolding = false;
             holdingTime = 0f;
         }
     }
    
     public void JudgeNotes()
     {
         if (InGamePlayManager.instance.noteListinRail[clickedRailNum].Count == 0)
             return;
    
         note = InGamePlayManager.instance.GetFirstNote(clickedRailNum);
         switch (note.noteID)
         {
             case 0:
             {
                 JudgeShortNoteAccuracy(note);
                 
                 note.gameObject.SetActive(false);
                 note = null;
                 
                 InGamePlayManager.instance.noteListinRail[clickedRailNum].RemoveAt(0);
    
                 break;
             }
             case 1:
             {
                 JudgeLongNoteAccuracy(note as LongNote);
                     
                 break;
             }
         }
     }
    
      public void JudgeShortNoteAccuracy(ShortNote tempNote)
      {
         judgeTime = GetJudgeTime();
         var noteTime = tempNote.noteStartingTime;

         GetNoteAccuracy(noteTime);
      }
    
      public void JudgeLongNoteAccuracy(LongNote tempNote)
      {
          judgeTime = GetJudgeTime();
          var noteTime = tempNote.noteStartingTime;
          
          GetNoteAccuracy(noteTime);
      }

      public void JudgeLongNoteHolding(LongNote tempNote)
      {
          Debug.Log("PASS");
      }

      public void ScalingLongNote(LongNote tempNote)
      {
          judgeTime = GetJudgeTime();
          // 버튼을 누르고 있으면 롱노트가 계속 줄어들도록 연출
          if ( note != null && holdingTime < tempNote.noteHoldingTime)
          {
              if (judgeTime >= tempNote.noteStartingTime)
              {
                  note.transform.position = new Vector3(note.transform.position.x,
                       InGamePlayManager.instance.judgeBar.transform.position.y, note.transform.position.z);
                  note.transform.localScale =
                      new Vector3(1f, tempNote.transform.position.y + (tempNote.noteHoldingTime - holdingTime)
                                      * TotalManager.instance.finalChartSpeed
                                      - InGamePlayManager.instance.judgeBar.transform.position.y, 1f);
              }
              // else
              // {
              //     note.transform.position = new Vector3(note.transform.position.x,
              //          InGamePlayManager.instance.judgeBar.transform.position.y, note.transform.position.z);
              //     note.transform.localScale =
              //         new Vector3(1f, tempNote.transform.position.y + (tempNote.noteHoldingTime + holdingTime)
              //                         * TotalManager.instance.finalChartSpeed
              //                         - InGamePlayManager.instance.judgeBar.transform.position.y, 1f);
              // }
          }
      }

      public void JudgeLongNoteEnd(LongNote tempNote)
      {
          var noteTime = tempNote.noteHoldingTime;
          
          if (holdingTime >= noteTime - 0.04f && holdingTime <= noteTime + 0.04f)
          {
              Debug.Log("End holdingTime: " + holdingTime);
              Debug.Log("noteTime: " + noteTime);
              Debug.Log("PERFECT");
          }
          
          note.gameObject.SetActive(false);
          note = null;
          InGamePlayManager.instance.noteListinRail[clickedRailNum].RemoveAt(0);
      }

      private void GetNoteAccuracy(float noteTime)
      {
          if (judgeTime >= noteTime - 0.04f && judgeTime <= noteTime + 0.04f)
          {
              Debug.Log("noteTime: " + noteTime);
              Debug.Log("MAX 100%");
          }
          else if (judgeTime >= noteTime - 0.14f && judgeTime <= noteTime + 0.14f)
          {
              Debug.Log("noteTime: " + noteTime);
              Debug.Log("MAX 90%");
          }
          else if (judgeTime >= noteTime - 0.24f && judgeTime <= noteTime + 0.24f)
          {
              Debug.Log("noteTime: " + noteTime);
              Debug.Log("MAX 80%");
          }
          else
          {
              Debug.Log("MISS");
          }
          // else if (~) {~} 세부 판정 작성해야함
      }
      
      private float GetJudgeTime()
      {
          // return (InGamePlayManager.instance.judgeBar.transform.position.y -
          //         TotalManager.instance.minNotePosY)
          //        / (TotalManager.instance.defaultChartSpeed * TotalManager.instance.userChartSpeed);
          return (float)InGamePlayManager.instance.video.time;
      }
}