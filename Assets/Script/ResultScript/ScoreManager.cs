using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public InGamePlayManager GM;
    public TotalManager TM;
    
    public Animator scoreAnimator;
    public Animator detailAnimator;
    private bool isRight = false;

    public Image rank;
    public TextMeshProUGUI score;
    public TextMeshProUGUI combo;
    
    public TextMeshProUGUI perfect;
    public TextMeshProUGUI great;
    public TextMeshProUGUI good;
    public TextMeshProUGUI miss;

    public Image maxCombo;
    public GameObject newRecord;

    private void Awake()
    {
        GM = InGamePlayManager.instance;
        TM = TotalManager.instance;
    }

    void Start()
    {
        score.text = GM.tempScore.ToString("F0");
        combo.text = GM.tempHighestCombo.ToString("F0");

        perfect.text = GM.perfectCount.ToString("F0");
        great.text = GM.greatCount.ToString("F0");
        good.text = GM.goodCount.ToString("F0");
        miss.text = GM.missCount.ToString("F0");

        string rankPath = "Rank/rank";
        Sprite imageSprite = LoadSprite(rankPath, RankJudge(GM.tempScore));
        rank.sprite = imageSprite;

        if(GM.tempHighestCombo == GM.maxCombo)
        {
            maxCombo.transform.gameObject.SetActive(true);
        }

        if(GM.tempScore > CheckRecord())
        {
            newRecord.transform.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!isRight)
            {
                scoreAnimator.SetTrigger("MoveRight");
                detailAnimator.SetTrigger("FadeIn");
            }
            else
            {
                scoreAnimator.SetTrigger("MoveCenter");
                detailAnimator.SetTrigger("FadeOut");
            }
            isRight = !isRight;
        }
    }

    string RankJudge(float score)
    {
        if (score >= 950000) return "s";
        if (score >= 900000) return "a";
        if (score >= 800000) return "b";
        if (score >= 700000) return "c";
        if (score >= 600000) return "d";
        return "f";
    }

    Sprite LoadSprite(string basePath, string spriteName)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(basePath);
        foreach (var sprite in sprites)
        {
            if (sprite.name == spriteName)
            {
                return sprite;
            }
        }
        return null;
    }

    int CheckRecord()
    {
        var data = new PlayerData(TM.tempSongName, "PlayData");

        data.songName = TM.tempSongName;
        data.songArtist = TM.tempSongArtist;
        
        string dir = Path.Combine(Application.persistentDataPath, "PlayData");
        string playerDataPath = Path.Combine(dir, $"{TM.tempSongName}_PlayData.json");
        var recordedData = SaveLoadHelper.LoadPlayerData<PlayerData>(playerDataPath);

        return recordedData.bestScore;
    }
}
