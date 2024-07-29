using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongListManager : MonoBehaviour
{
    public GameObject songPrefab;
    public Transform contentPanel;

    void Awake()
    {
        PopulateSongList();
    }

    void PopulateSongList()
    {
        foreach (var song in SongDB.Instance.songs)
        {   
            var songListElement = Instantiate(songPrefab, contentPanel).GetComponent<SongPrefabMaker>();
            songListElement.FillContent(song);
        }
    }
}