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
    private ShortNote note = null;
    
    private bool isHolding = false;
    private bool isRecieved = false;
    public bool isLongNoteClicked = false;
    
    private float judgeTime = 0f;
    private float judgeInterval = 0f;
    private float scaleOftempNote = 0f;
    
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
         if (note == null) isRecieved = false;
         
         if (InGamePlayManager.instance.noteListinRail[clickedRailNum].Count != 0)
         {
             note = InGamePlayManager.instance.GetFirstNote(clickedRailNum);

             if (!isRecieved)
             {
                scaleOftempNote = note.transform.localScale.y;
                isRecieved = true;
             }
         }
         
         judgeTime = GetJudgeTime();
         if (note != null && note.noteID == 1)
         {
             if (judgeTime >= note.noteStartingTime && judgeTime <= note.noteHoldingTime + note.noteStartingTime)
             {
                 judgeInterval += Time.deltaTime;
             }
             
             if (isHolding) ScalingLongNote();
             
             if (judgeInterval >= 0.5f)
             {
                 Debug.Log("JudgeInterval: " + judgeInterval);
                 // 롱노트 첫 입력 이후 홀드 시의 판정 처리
                 JudgeLongNoteHolding();
                 judgeInterval = 0f;
             }
         }
         
         // 인게임에 비디오를 구현해놨으니 비디오의 time(현재 재생 시간) 속성을 활용하여 정확도를 판별하도록 구현 예정
         if (Input.GetKeyDown(Key))
         {
             buttonEffect.SetActive(true);
             buttonImage.sprite = downImage;
    
             Debug.Log("Key: " + Key + ", Time: " + InGamePlayManager.instance.video.time);
             isHolding = true;
             JudgeNotes();
         }
         
         if (Input.GetKeyUp(Key))
         {
             buttonEffect.SetActive(false);
             buttonImage.sprite = upImage;

             judgeTime = GetJudgeTime();
             
             // 롱노트의 마지막 입력 판정 처리
             if (note != null && note.noteID == 1)
             {
                 var longNoteEndTime = note.noteStartingTime + note.noteHoldingTime;
                 if (judgeTime >= longNoteEndTime - 0.24f && judgeTime <= longNoteEndTime + 0.24f)
                 {
                    JudgeLongNoteEnd();
                 }
             }
             
             isHolding = false;
         }
     }
    
     private void JudgeNotes()
     {
         if (InGamePlayManager.instance.noteListinRail[clickedRailNum].Count == 0)
             return;
    
         // note = InGamePlayManager.instance.GetFirstNote(clickedRailNum);
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
    
      private void JudgeNoteStart()
      {
         judgeTime = GetJudgeTime();
         var noteTime = note.noteStartingTime;

         isLongNoteClicked = true;
         
         GetNoteAccuracy(noteTime);
      }

      private void JudgeLongNoteHolding()
      {
          if (isHolding)
          {
              Debug.Log("PASS");
          }
          else
          {
              Debug.Log("LongNote Holding MISS");
              InGamePlayManager.instance.ResetTempCombo();
          }
      }

      private void ScalingLongNote()
      {
          if (note == null) return;
          
          // 버튼을 누르고 있으면 롱노트가 계속 줄어들도록 연출
          if (judgeTime >= note.noteStartingTime && judgeTime < note.noteStartingTime + note.noteHoldingTime)
          {
              note.transform.localScale =
                  new Vector3(1f, scaleOftempNote - (judgeTime - note.noteStartingTime) 
                      * TotalManager.instance.finalChartSpeed, 1f);
              note.transform.position = new Vector3(note.transform.position.x,
                  InGamePlayManager.instance.judgeBar.transform.position.y, note.transform.position.z);
          }

          if (judgeTime >= (note.noteStartingTime + note.noteHoldingTime))
          {
              note.transform.localScale = new Vector3(1f, 0f, 1f);
          }
      }

      private void JudgeLongNoteEnd()
      {
          judgeTime = GetJudgeTime();
          var noteTime = note.noteStartingTime + note.noteHoldingTime;

          isLongNoteClicked = false;
          
          GetNoteAccuracy(noteTime);

          if (note != null)
          {
              note.gameObject.SetActive(false);
              note = null;
              InGamePlayManager.instance.noteListinRail[clickedRailNum].RemoveAt(0);
          }
      }

      private void GetNoteAccuracy(float noteTime)
      {
          if (judgeTime >= noteTime - 0.04f && judgeTime <= noteTime + 0.04f)
          {
              Debug.Log("noteTime: " + noteTime);
              Debug.Log("MAX 100%");
              
              InGamePlayManager.instance.PlusTempCombo();
              InGamePlayManager.instance.GetTempScore(1f);
          }
          else if (judgeTime >= noteTime - 0.14f && judgeTime <= noteTime + 0.14f)
          {
              Debug.Log("noteTime: " + noteTime);
              Debug.Log("MAX 90%");
              
              InGamePlayManager.instance.PlusTempCombo();
              InGamePlayManager.instance.GetTempScore(0.9f);
          }
          else if (judgeTime >= noteTime - 0.24f && judgeTime <= noteTime + 0.24f)
          {
              Debug.Log("noteTime: " + noteTime);
              Debug.Log("MAX 80%");
              
              InGamePlayManager.instance.PlusTempCombo();
              InGamePlayManager.instance.GetTempScore(0.8f);
          }
          else
          {
              Debug.Log("MISS");
              
              InGamePlayManager.instance.ResetTempCombo();
          }
      }
      
      private float GetJudgeTime()
      {
          return (float)InGamePlayManager.instance.video.time;
      }
}