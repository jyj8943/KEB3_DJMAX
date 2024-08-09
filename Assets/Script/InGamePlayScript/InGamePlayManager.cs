using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.Video;
using UnityEngine.UI;

public class InGamePlayManager : MonoBehaviour
{
    public TotalManager TM;
    
    public static InGamePlayManager instance;

    public GameObject judgeBar;
    public VideoPlayer video;
    public Camera inGameCamera;
    public Button buttonA;
    public Button buttonS;
    public Button buttonSemiColon;
    public Button buttonQuotes;
    public TextMeshProUGUI tempComboText;
    public TextMeshProUGUI tempScoreText;
    public GameObject readyPanel;
    public TextMeshProUGUI countDown;
    
    public bool isPlaying = false;
    private bool isPassed = true;
    public bool isEmpty = false;
    
    public int countdownTime = 3;
    public int maxCombo = 0;
    public int tempCombo = 0;
    public int tempHighestCombo = 0;
    
    public float maxScore = 1000000f;
    public float tempScore = 0f;
    public float scoreOfOneNote = 0f;

    public int perfectCount = 0;
    public int greatCount = 0;
    public int goodCount = 0;
    public int missCount = 0;
    
    
    public List<GameObject> noteList = new();
    public List<ShortNote>[] noteListinRail = new List<ShortNote>[]{
        new List<ShortNote>(), new List<ShortNote>(), new List<ShortNote>(), new List<ShortNote>()
    };

    public ShortNote GetFirstNote(int i) => noteListinRail[i][0];


    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        TM = TotalManager.instance;
        
        if (maxCombo != 0)
        {
            scoreOfOneNote = maxScore / maxCombo;
        }
        
        StartCoroutine(StartChart());
    }

    private void Update()
    {
        NoteMiss();
        
        DisplayTempCombo();
        DisplayTempScore();
        
        if (isPlaying) video.Play();
        else if (!isPlaying) video.Pause();
        
        // noteListInRail이 모두 비어 게임이 끝나는 기능
        foreach (var noteList in noteListinRail)
        {
            if (noteList.Count != 0)
            {
                isEmpty = false;
                break;
            }

            isEmpty = true;
        }

        if (isEmpty)
        {
            StartCoroutine(GameFinish());
        }
    }
    
    // 유저가 노트를 아예 놓치는 경우 발생하는 MISS에 대해 판정
    private void NoteMiss()
    {
        foreach (var noteList in noteListinRail)
        {
            if (noteList.Count == 0) continue;

            Button tempButton = null;
            switch (noteList[0].railNum)
            {
                case (1):
                {
                    tempButton = buttonA;
                    break;
                }
                case (2):
                {
                    tempButton = buttonS;
                    break;
                }
                case (3):
                {
                    tempButton = buttonSemiColon;
                    break;
                }
                case (4):
                {
                    tempButton = buttonQuotes;
                    break;
                }
            }
            
            if ( video.time >= noteList[0].noteStartingTime + 0.25f)
            {
                if (noteList[0].noteID == 0)
                {
                    // shortNote의 경우 그대로 없어지며 MISS 판정이 뜨지만, longNote의 경우 그대로 내려가며 계속 MiSS가 떠야함
                    Debug.Log("ShortNote MISS");
                    noteList[0].gameObject.SetActive(false);
                    noteList.RemoveAt(0);
                    
                    ResetTempCombo();
                    missCount++;
                }
                else if (noteList[0].noteID == 1)
                {
                    if (isPassed && !tempButton.isLongNoteClicked) 
                    {
                        Debug.Log(tempButton.name);
                        Debug.Log("LongNote Starting MISS");
                        
                        isPassed = false;
                        missCount++;
                        
                        ResetTempCombo();
                    }
                    
                    if (video.time >= noteList[0].noteStartingTime + noteList[0].noteHoldingTime + 0.25f)
                    {
                        Debug.Log(tempButton.name);
                        Debug.Log("LongNote End MISS");
                        
                        noteList[0].gameObject.SetActive(false);
                        noteList.RemoveAt(0);
                        
                        isPassed = true;
                        missCount++;
                        
                        ResetTempCombo();
                    }
                }
            }
        }
    }
    
    public void DivideList()
    {
        if (noteList == null) return;

        foreach (var note in noteList)
        {
            noteListinRail[note.GetComponent<ShortNote>().railNum - 1].Add(note.GetComponent<ShortNote>());
        }
    }

    public void DisplayTempCombo()
    {
        if (tempCombo == 0)
        {
            tempComboText.gameObject.SetActive(false);
        }
        else
        {
            tempComboText.gameObject.SetActive(true);
            tempComboText.SetText(tempCombo.ToString());
        }
    }

    public void PlusTempCombo()
    {
        tempCombo += 1;

        if (tempCombo >= tempHighestCombo)
        {
            tempHighestCombo = tempCombo;
        }
    }

    public void ResetTempCombo()
    {
        tempCombo = 0;
    }

    public void DisplayTempScore()
    {
        tempScoreText.SetText(Mathf.Round(tempScore).ToString());
    }

    public void GetTempScore(float accuracyRate)
    {
        tempScore += (scoreOfOneNote * accuracyRate);

        if (tempScore > maxScore) tempScore = maxScore;
    }

    public void GetJudgeCount(string judge)
    {
        switch (judge)
        {
            case ("PERFECT"):
            {
                perfectCount++;
                break;
            }
            case ("GREAT"):
            {
                greatCount++;
                break;
            }
            case ("GOOD"):
            {
                goodCount++;
                break;
            }
            case ("MISS"):
            {
                missCount++;
                break;
            }
        }
    }

    private IEnumerator StartChart()
    {
        while (countdownTime > 0)
        {
            countDown.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        readyPanel.gameObject.SetActive(false);
        countDown.gameObject.SetActive(false);

        isPlaying = true;
    }

    private IEnumerator GameFinish()
    {
        yield return new WaitForSeconds(3f);
        
        TM.SetTempScoreAndCombo(Mathf.RoundToInt(tempScore), tempHighestCombo);
        TM.SetAccuracyCount(perfectCount, greatCount, goodCount, missCount);
        
        Debug.Log("Game Finish!");
        SceneManager.LoadScene("Result");
    }

    public void LoadVideo(string tempTitle, string tempArtist)
    {
        string videoPath = "AlbumVideo/" + tempTitle + "_" + tempArtist;

        var videoClip = Resources.Load<VideoClip>(videoPath);

        if (videoClip != null)
        {
            video.source = VideoSource.VideoClip;
            video.clip = videoClip;
            Debug.Log("Video Loaded from Resources!");
            video.Play();
        }
        else
        {
            var localPath = Path.Combine(Application.persistentDataPath, "SongVideo",
                tempTitle + tempArtist + ".mp4");

            if (File.Exists(localPath))
            {
                video.source = VideoSource.Url;
                video.url = localPath;
                Debug.Log("Video Loaded from Local Folder!");
                video.Play();
            }
            else
            {
                Debug.LogError("Video doesn't Exist in Local Folder!");
            }
        }
    }
}