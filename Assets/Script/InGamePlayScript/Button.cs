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
             judgeTime = GetJudgeTime();
             if (judgeTime >= note.noteStartingTime)
             {
                 holdingTime += Time.deltaTime;
             }
             if (InGamePlayManager.instance.video.time >= note.noteStartingTime)
             {
                 judgeInterval += Time.deltaTime;
             }
             
             ScalingLongNote();
             
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

             judgeTime = GetJudgeTime();
             
             // 롱노트의 마지막 입력 판정 처리
             if (note != null && note.noteID == 1)
                 JudgeLongNoteEnd();
             
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
                 JudgeNoteStart();
                 
                 note.gameObject.SetActive(false);
                 note = null;
                 
                 InGamePlayManager.instance.noteListinRail[clickedRailNum].RemoveAt(0);
        
                 break;
             }
             case 1:
             {
                 JudgeNoteStart();
                     
                 break;
             }
         }
     }
    
      public void JudgeNoteStart()
      {
         judgeTime = GetJudgeTime();
         var noteTime = note.noteStartingTime;
         
         GetNoteAccuracy(noteTime);
      }

      public void JudgeLongNoteHolding(LongNote tempNote)
      {
          Debug.Log("PASS");
      }

      public void ScalingLongNote()
      {
          if (note == null) return;
          
          // 버튼을 누르고 있으면 롱노트가 계속 줄어들도록 연출
          if (holdingTime < note.noteHoldingTime && judgeTime >= note.noteStartingTime)
          { 
              note.transform.position = new Vector3(note.transform.position.x,
                  InGamePlayManager.instance.judgeBar.transform.position.y, note.transform.position.z);
              note.transform.localScale =
                  new Vector3(1f, note.transform.position.y + (note.noteHoldingTime - holdingTime) 
                                  * TotalManager.instance.finalChartSpeed
                                  - InGamePlayManager.instance.judgeBar.transform.position.y, 1f);
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

      public void JudgeLongNoteEnd()
      {
          var noteTime = note.noteStartingTime + note.noteHoldingTime;
          
          GetNoteAccuracy(noteTime);
          
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
      }
      
      private float GetJudgeTime()
      {
          return (float)InGamePlayManager.instance.video.time;
      }
}