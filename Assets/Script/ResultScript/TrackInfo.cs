using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrackInfo : MonoBehaviour
{
    public GameObject backgroundImage;
    public Image albumImage;
    public TextMeshProUGUI trackTitle;
    public TextMeshProUGUI trackArtist;
    public TextMeshProUGUI trackDifficulty;
    public TextMeshProUGUI trackLevel;

    void Start()
    {
        trackTitle.text = Selector.selectedTrackTitle;
        trackArtist.text = Selector.selectedTrackArtist;

        string imagePath = "AlbumImage/" + trackTitle.text + "_" + trackArtist.text;
        Sprite imageSprite = Resources.Load<Sprite>(imagePath);
        albumImage.sprite = imageSprite;
        backgroundImage.GetComponent<SpriteRenderer>().sprite = imageSprite;

        if (TrackSelector.level > 0 && TrackSelector.level < 3)
        {
            trackDifficulty.text = "EASY";
        }
        else if (TrackSelector.level >= 3 && TrackSelector.level < 5)
        {
            trackDifficulty.text = "NORMAL";
        }
        else if (TrackSelector.level >= 5)
        {
            trackDifficulty.text = "HARD";
        }

        trackLevel.text = "";
        for (int i = 0 ; i < TrackSelector.level ; i++)
        {
            trackLevel.text += "<sprite=0>";
        }
    }
}

