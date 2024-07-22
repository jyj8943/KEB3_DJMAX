using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SongListManager : MonoBehaviour
{
    public GameObject songPrefab;
    public Transform contentPanel;
    public TextMeshProUGUI infoTitle;
    public TextMeshProUGUI infoArtist;
    public Image infoAlbumImage;
    private int listIndex = 0;

    void Start()
    {
        PopulateSongList();
        Button firstButton = contentPanel.transform.GetChild(listIndex).GetComponent<Button>();
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ScrollDown();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ScrollUp();
        }
    }

    void PopulateSongList()
    {
        foreach (var song in SongDB.Instance.songs)
        {   
            var songListElement = Instantiate(songPrefab, contentPanel).GetComponent<SongPrefabMaker>();
            songListElement.FillContent(song);
        }
    }

    void ScrollDown()
    {
        listIndex += 1;
        Button selectedButton = contentPanel.transform.GetChild(listIndex).GetComponent<Button>();
        EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);
    }

    void ScrollUp()
    {
        listIndex -= 1;
        Button selectedButton = contentPanel.transform.GetChild(listIndex).GetComponent<Button>();
        EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);
    }

    void InfoUpdate()
    {
        // infoTitle.text = 
    }
}