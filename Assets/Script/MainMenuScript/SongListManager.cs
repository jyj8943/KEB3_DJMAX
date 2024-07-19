using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SongListManager : MonoBehaviour
{
    public GameObject songPrefab; // 곡 정보 Prefab
    public Transform contentPanel; // ScrollView의 Content

    void Start()
    {
        PopulateSongList();
        Button firstButton = contentPanel.transform.GetChild(0).GetComponent<Button>();
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
    }

    void PopulateSongList()
    {
        foreach (var song in SongDB.Instance.songs)
        {   
            GameObject newSong = Instantiate(songPrefab, contentPanel);
            TextMeshProUGUI titleText = newSong.transform.Find("Title").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI artistText = newSong.transform.Find("Artist").GetComponent<TextMeshProUGUI>();
            Image songImage = newSong.transform.Find("Image").GetComponent<Image>();

            titleText.text = song.songTitle;
            artistText.text = song.songArtist;

            string imagePath = "AlbumImage/" + song.songTitle + "_" + song.songArtist;
            Sprite imageSprite = Resources.Load<Sprite>(imagePath);
            songImage.sprite = imageSprite;
        }
    }
}