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
            songs.Add(new SongInfo {songTitle = "Butterfly", songArtist = "Digimon", songLevel = 3, songBestScore = 1234567, songBestCombo = 2024});
            songs.Add(new SongInfo {songTitle = "Dynamite", songArtist = "BTS", songLevel = 5});
            songs.Add(new SongInfo {songTitle = "Drama", songArtist = "Aespa", songLevel = 4});
            songs.Add(new SongInfo {songTitle = "Song 3", songArtist = "Artist3", songLevel = 1});
            songs.Add(new SongInfo {songTitle = "Celebrity", songArtist = "IU", songLevel = 2});
            songs.Add(new SongInfo {songTitle = "Song 5", songArtist = "Artist5", songLevel = 1});
            songs.Add(new SongInfo {songTitle = "Song 6", songArtist = "Artist6", songLevel = 1});
            songs.Add(new SongInfo {songTitle = "Song 7", songArtist = "Artist7", songLevel = 1});
            songs.Add(new SongInfo {songTitle = "Song 8", songArtist = "Artist8", songLevel = 1});
            songs.Add(new SongInfo {songTitle = "Song 9", songArtist = "Artist9", songLevel = 1});

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
    public int songLevel;
    public int songBestScore;
    public int songBestCombo;
}