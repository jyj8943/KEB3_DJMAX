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
        var songTitle = selectedButton.GetComponent<SongPrefabMaker>().title;
        var songArtist = selectedButton.GetComponent<SongPrefabMaker>().artist;

        infoTitle.text = songTitle.text;
        infoArtist.text = songArtist.text;

        string imagePath = "AlbumImage/" + songTitle.text + "_" + songArtist.text;
        Sprite imageSprite = Resources.Load<Sprite>(imagePath);
        infoAlbumImage.sprite = imageSprite;

        LevelDifficultyInfo();
    }

    void LevelDifficultyInfo()
    {
        var level = selectedButton.GetComponent<SongPrefabMaker>().level;

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
    }
}
