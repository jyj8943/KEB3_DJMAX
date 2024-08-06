using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TrackSelector : MonoBehaviour
{
    public Transform contentPanel;
    public ScrollRect scrollRect;

    public TextMeshProUGUI infoTitle;
    public TextMeshProUGUI infoArtist;
    public TextMeshProUGUI infoDifficulty;
    public TextMeshProUGUI infoLevel;
    public Image infoAlbumImage;

    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI bestCombo;
    public Image bestRank;

    public GameObject selectedButton;
    private int listIndex = 0;

    private float scrollDelay = 0.3f;
    private float initialDelay = 0.5f;
    private float lastScrollTime = 0f;
    private bool isKeyHeld = false;

    void Start()
    {
        selectedButton = contentPanel.transform.GetChild(0).gameObject;
        EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);
        InfoUpdate();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ScrollDown();
            lastScrollTime = Time.time;
            isKeyHeld = true;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ScrollUp();
            lastScrollTime = Time.time;
            isKeyHeld = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && isKeyHeld)
        {
            if (Time.time - lastScrollTime > initialDelay)
            {
                ScrollDown();
                lastScrollTime = Time.time - (initialDelay - scrollDelay);
                initialDelay = scrollDelay; 
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow) && isKeyHeld)
        {
            if (Time.time - lastScrollTime > initialDelay)
            {
                ScrollUp();
                lastScrollTime = Time.time - (initialDelay - scrollDelay);
                initialDelay = scrollDelay;
            }
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            isKeyHeld = false;
            initialDelay = 0.5f;
        }
    }

    void ScrollDown()
    {
        if (listIndex < contentPanel.childCount-1)
        {
            listIndex += 1;
            selectedButton = contentPanel.transform.GetChild(listIndex).gameObject;
            EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);
            scrollRect.verticalNormalizedPosition -= 0.1f;
            InfoUpdate();
        }
    }

    void ScrollUp()
    {
        if (listIndex > 0)
        {
            listIndex -= 1;
            selectedButton = contentPanel.transform.GetChild(listIndex).gameObject;
            EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);
            scrollRect.verticalNormalizedPosition += 0.1f;
            InfoUpdate();
        }
    }

    public void InfoUpdate()
    {
        infoTitle.text = selectedButton.GetComponent<SongPrefabMaker>().title.text;
        infoArtist.text = selectedButton.GetComponent<SongPrefabMaker>().artist.text;

        string imagePath = "AlbumImage/" + infoTitle.text + "_" + infoArtist.text;
        Sprite imageSprite = Resources.Load<Sprite>(imagePath);
        infoAlbumImage.sprite = imageSprite;

        LevelDifficultyInfo();
        BestInfo();
    }

    public static int level;

    void LevelDifficultyInfo()
    {
        level = selectedButton.GetComponent<SongPrefabMaker>().level;

        if (level > 0 && level < 3)
        {
            infoDifficulty.text = "EASY";
        }
        else if (level >= 3 && level < 5)
        {
            infoDifficulty.text = "NORMAL";
        }
        else if (level >= 5)
        {
            infoDifficulty.text = "HARD";
        }

        infoLevel.text = "";
        for (int i = 0 ; i < level ; i++)
        {
            infoLevel.text += "<sprite=0>";
        }
    }

    void BestInfo()
    {
        bestScore.text = selectedButton.GetComponent<SongPrefabMaker>().bestScore.ToString();
        bestCombo.text = selectedButton.GetComponent<SongPrefabMaker>().bestCombo.ToString();

        int score = selectedButton.GetComponent<SongPrefabMaker>().bestScore;
        string rank = RankJudge(score);
        AdjustSize(rank);

        string rankPath = "Rank/rank";
        Sprite imageSprite = LoadSprite(rankPath, rank);
        bestRank.sprite = imageSprite;
    }

    string RankJudge(int score)
    {
        if (score >= 950000) return "s";
        if (score >= 900000) return "a";
        if (score >= 800000) return "b";
        if (score >= 700000) return "c";
        if (score >= 600000) return "d";
        if (score < 600000 && score > 0) return "f";
        return "Not Played";
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

    void AdjustSize(string rank)
    {
        if (rank == "sss" || rank == "ss")
        {
            bestRank.rectTransform.sizeDelta = new Vector2(200, 150);
        }
        else
        {
            bestRank.rectTransform.sizeDelta = new Vector2(150, 150);
        }
    }
}
