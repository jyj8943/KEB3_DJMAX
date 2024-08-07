using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int maxCombo = 0;
    public int tempCombo = 0;

    public float maxScore = 1000000f; // 총 최대 점수
    public float tempScore = 0f;      // 현재 점수
    public float scoreOfOneNote = 0f; // 한 노트당 점수

    public int perfectCount = 0;
    public int greatCount = 0;
    public int goodCount = 0;
    public int missCount = 0;

    // 정확도에 따른 점수 비율
    private float perfectScoreRate = 1.0f;
    private float greatScoreRate = 0.8f;
    private float goodScoreRate = 0.5f;
    private float missScoreRate = 0.0f;

    // 노트 수
    private int totalNotes = 0;

    private void Start()
    {
        // 한 노트당 점수 계산
        scoreOfOneNote = maxScore / totalNotes;
    }

    // 노트 판정 처리
    public void JudgeNote(string judgment)
    {
        switch (judgment)
        {
            case "Perfect":
                perfectCount++;
                tempCombo++;
                tempScore += scoreOfOneNote * perfectScoreRate;
                break;
            case "Great":
                greatCount++;
                tempCombo++;
                tempScore += scoreOfOneNote * greatScoreRate;
                break;
            case "Good":
                goodCount++;
                tempCombo++;
                tempScore += scoreOfOneNote * goodScoreRate;
                break;
            case "Miss":
                missCount++;
                tempCombo = 0;
                tempScore += scoreOfOneNote * missScoreRate;
                break;
        }

        // 최대 콤보 업데이트
        if (tempCombo > maxCombo)
        {
            maxCombo = tempCombo;
        }

        // UI 업데이트
        UpdateUI();
    }

    // UI 업데이트 메서드
    private void UpdateUI()
    {
        // 여기에 UI 업데이트 로직을 추가하세요.
        // 예를 들어, TextMeshPro 텍스트나 UI 텍스트를 업데이트합니다.
        // 예시:
        // scoreText.text = $"Score: {tempScore:F0}";
        // comboText.text = $"Combo: {tempCombo}";
        // perfectCountText.text = $"Perfect: {perfectCount}";
        // ...
    }
}