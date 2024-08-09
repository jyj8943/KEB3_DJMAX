using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class TotalManager : MonoBehaviour
{
    public static TotalManager instance;

    public float defaultChartSpeed = 2f;
    public float userChartSpeed = 1f;
    public float finalChartSpeed;

    public float maxUserChartSpeed = 7f;

    public float minNotePosY = -3f;
    public float minNotePosX = -2f;
    public float maxNotePosX = 2f;

    public int songTime = 80;

    // PlayerData를 위한 데이터들
    public string tempSongName;
    public string tempSongArtist;
    
    public int tempSongBestScore;
    public int tempSongBestCombo;
    public int tempPerfectCount;
    public int tempGreatCount;
    public int tempGoodCount;
    public int tempMissCount;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
        finalChartSpeed = defaultChartSpeed * userChartSpeed;
    }

    private void Update()
    {
        finalChartSpeed = defaultChartSpeed * userChartSpeed;
    }

    public void ChangeSpeed(float speed)
    {
        userChartSpeed += speed;
        finalChartSpeed = defaultChartSpeed * userChartSpeed;
    }

    // PlayerData의 데이터들을 저장하거나 초기화하는 함수들
    public void InitTempSong()
    {
        tempSongName = null;
        tempSongArtist = null;
    }
    
    public void SetTempSong(string songName, string songArtist)
    {
        tempSongName = songName;
        tempSongArtist = songArtist;
    }

    public void InitTempScoreAndCombo()
    {
        tempSongBestScore = 0;
        tempSongBestCombo = 0;
    }

    public void SetTempScoreAndCombo(int tempScore, int tempCombo)
    {
        tempSongBestScore = tempScore;
        tempSongBestCombo = tempCombo;
    }

    public void InitAccuracyCount()
    {
        tempPerfectCount = 0;
        tempGreatCount = 0;
        tempGoodCount = 0;
        tempMissCount = 0;
    }
    
    public void SetAccuracyCount(int perfectCount, int greatCount, int goodCount, int missCount)
    {
        tempPerfectCount = perfectCount;
        tempGreatCount = greatCount;
        tempGoodCount = goodCount;
        tempMissCount = missCount;
    }
}
