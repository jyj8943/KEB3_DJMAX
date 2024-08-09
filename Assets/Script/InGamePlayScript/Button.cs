using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.tvOS;

public class Button : MonoBehaviour
{
    public InGamePlayManager GM;
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

    public static string judgeResult = "";
    public static bool isJudged = false;
    
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

    private void Start()
    {
        GM = InGamePlayManager.instance;
    }

    private void Update()
     {
         if (note == null) isRecieved = false;
         
         if (GM.noteListinRail[clickedRailNum].Count != 0)
         {
             note = GM.GetFirstNote(clickedRailNum);

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
         if (Input.GetKeyDown(Key) && InGamePlayManager.instance.isPlaying)
         {
             buttonEffect.SetActive(true);
             buttonImage.sprite = downImage;
    
             Debug.Log("Key: " + Key + ", Time: " + GM.video.time);
             isHolding = true;
             JudgeNotes();
         }
         
         if (Input.GetKeyUp(Key) && InGamePlayManager.instance.isPlaying)
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
         if (GM.noteListinRail[clickedRailNum].Count == 0)
             return;
         
         // 판정의 범위 제한 ( 필요시 조정 가능 )
         if (judgeTime <= note.noteStartingTime - 0.34f || judgeTime >= note.noteStartingTime + 0.34f)
             return;
    
         // note = GM.GetFirstNote(clickedRailNum);
         switch (note.noteID)
         {
             case 0:
             {
                 JudgeNoteStart();
                 
                 note.gameObject.SetActive(false);
                 note = null;
                 
                 GM.noteListinRail[clickedRailNum].RemoveAt(0);
        
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
              GM.ResetTempCombo();
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
                  GM.judgeBar.transform.position.y, note.transform.position.z);
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
              GM.noteListinRail[clickedRailNum].RemoveAt(0);
          }
      }

      // 노트 판정 종류: PERFECT, GREAT, GOOD, MISS, PASS (테스트하면서 판정 조절해야할듯 )
      // PERFECT: +- 0.04f
      // GREAT: +- 0.14f
      // GOOD: +- 0.24f
      // MISS: 그 외에 누른 경우 ( 정해진 범위 내에서만 눌렀을 때, 그렇지 않으면 반응없게 해야할듯 )
      // PASS: longNote가 judgeInterval때 눌렸는지 아닌지 판정
      private void GetNoteAccuracy(float noteTime)
      {
          if (judgeTime >= noteTime - 0.04f && judgeTime <= noteTime + 0.04f)
          {
              Debug.Log("noteTime: " + noteTime);
              Debug.Log("PERFECT");
              
              GM.GetJudgeCount("PERFECT");
              GM.PlusTempCombo();
              GM.GetTempScore(1f);
              judgeResult = "PERFECT";
          }
          else if (judgeTime >= noteTime - 0.14f && judgeTime <= noteTime + 0.14f)
          {
              Debug.Log("noteTime: " + noteTime);
              Debug.Log("GREAT");
              
              GM.GetJudgeCount("GREAT");
              GM.PlusTempCombo();
              GM.GetTempScore(0.9f);
              judgeResult = "GREAT";
          }
          else if (judgeTime >= noteTime - 0.24f && judgeTime <= noteTime + 0.24f)
          {
              Debug.Log("noteTime: " + noteTime);
              Debug.Log("GOOD");
              
              GM.GetJudgeCount("GOOD");
              GM.PlusTempCombo();
              GM.GetTempScore(0.8f);
              judgeResult = "GOOD";
          }
          else
          {
              Debug.Log("MISS");
              
              GM.GetJudgeCount("MISS");
              GM.ResetTempCombo();
              judgeResult = "MISS";
          }
          isJudged = true;
      }
      
      private float GetJudgeTime()
      {
          return (float)GM.video.time;
      }
}