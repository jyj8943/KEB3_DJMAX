using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SongSelect : MonoBehaviour
{
    public GameObject infoPanel;

    void Start()
    {
        infoPanel = GameObject.Find("SongInfo");
    }
    public void ClickBtn()
    {
        // Click한 버튼 정보 가져오기
        GameObject selectedSong = EventSystem.current.currentSelectedGameObject;
        TextMeshProUGUI songTitle = selectedSong.transform.Find("Title").GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI songArtist = selectedSong.transform.Find("Artist").GetComponentInChildren<TextMeshProUGUI>();

        // InfoPanel에 정보 입력
        TextMeshProUGUI infoTitle = infoPanel.transform.Find("Title").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI infoAritist = infoPanel.transform.Find("Artist").GetComponent<TextMeshProUGUI>();
        Image infoImage = infoPanel.transform.Find("Image").GetComponent<Image>();
        
        infoTitle.text = songTitle.text;
        infoAritist.text = songArtist.text;

        string imagePath = "AlbumImage/" + songTitle.text + "_" + songArtist.text;
        Sprite imageSprite = Resources.Load<Sprite>(imagePath);
        infoImage.sprite = imageSprite;
        
    }
}
        