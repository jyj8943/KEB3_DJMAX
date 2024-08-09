using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public InGamePlayManager GM;
    public Image rank;
    public TextMeshProUGUI score;
    public TextMeshProUGUI combo;

    private void Awake()
    {
        GM = InGamePlayManager.instance;
    }

    void Start()
    {
        score.text = GM.tempScore.ToString("F0");
        combo.text = GM.maxCombo.ToString("F0");

        string rankPath = "Rank/rank";
        Sprite imageSprite = LoadSprite(rankPath, RankJudge(GM.tempScore));
        rank.sprite = imageSprite;
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

}
