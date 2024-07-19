using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongDB : MonoBehaviour
{
    public static SongDB Instance;
    
    public List<SongInfo> songs = new List<SongInfo>();
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 여기에 노래 정보를 추가합니다.
            songs.Add(new SongInfo {songTitle = "Butterfly", songArtist = "Digimon"});
            songs.Add(new SongInfo {songTitle = "Dynamite", songArtist = "BTS"});
            songs.Add(new SongInfo {songTitle = "Drama", songArtist = "Aespa"});
            songs.Add(new SongInfo {songTitle = "Song 3", songArtist = "Artist3"});
            songs.Add(new SongInfo {songTitle = "Celebrity", songArtist = "IU"});
            songs.Add(new SongInfo {songTitle = "Song 5", songArtist = "Artist5"});
            songs.Add(new SongInfo {songTitle = "Song 6", songArtist = "Artist6"});
            songs.Add(new SongInfo {songTitle = "Song 7", songArtist = "Artist7"});
            songs.Add(new SongInfo {songTitle = "Song 8", songArtist = "Artist8"});
            songs.Add(new SongInfo {songTitle = "Song 9", songArtist = "Artist9"});

        }
        else
        {
            Destroy(gameObject);
        }
    }
}

public class SongInfo
{
    public string songTitle;
    public string songArtist;
    public string AlbumImage;
}