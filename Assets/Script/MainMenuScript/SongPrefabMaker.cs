using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SongPrefabMaker : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI artist;
    public Image albumimage;
    public int level;

    public void FillContent(SongInfo songInfo)
    {
        title.text = songInfo.songTitle;
        artist.text = songInfo.songArtist;

        string imagePath = "AlbumImage/" + title.text + "_" + artist.text;
        Sprite imageSprite = Resources.Load<Sprite>(imagePath);
        albumimage.sprite = imageSprite;
        
        level = songInfo.songLevel;
    }
}
        