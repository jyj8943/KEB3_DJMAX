using System.Collections;
using System.Collections.Generic;
using System.IO;
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

            LoadJsonInResourcesFolder();
            LoadJsonInLocalFolder();

            // 여기에 노래 정보를 추가합니다.
            // songs.Add(new SongInfo {songTitle = "Butterfly", songArtist = "Digimon", songLevel = 3, songBestScore = 952000, songBestCombo = 4024});
            // songs.Add(new SongInfo {songTitle = "Dynamite", songArtist = "BTS", songLevel = 5, songBestScore = 920000, songBestCombo = 3024});
            // songs.Add(new SongInfo {songTitle = "Drama", songArtist = "Aespa", songLevel = 4, songBestScore = 883904, songBestCombo = 2024});
            // songs.Add(new SongInfo {songTitle = "Song 3", songArtist = "Artist3", songLevel = 1, songBestScore = 748302, songBestCombo = 1024});
            // songs.Add(new SongInfo {songTitle = "Celebrity", songArtist = "IU", songLevel = 2, songBestScore = 647382, songBestCombo = 1024});
            // songs.Add(new SongInfo {songTitle = "Song 5", songArtist = "Artist5", songLevel = 1, songBestScore = 547654, songBestCombo = 524});
            // songs.Add(new SongInfo {songTitle = "Song 6", songArtist = "Artist6", songLevel = 1, songBestScore = 487654, songBestCombo = 324});
            // songs.Add(new SongInfo {songTitle = "Song 7", songArtist = "Artist7", songLevel = 1, songBestScore = 387654, songBestCombo = 224});
            // songs.Add(new SongInfo {songTitle = "Song 8", songArtist = "Artist8", songLevel = 1});
            // songs.Add(new SongInfo {songTitle = "Song 9", songArtist = "Artist9", songLevel = 1});

        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadJsonInResourcesFolder()
    {
        TextAsset[] songDataFiles = Resources.LoadAll<TextAsset>("Json");

        if (songDataFiles.Length == 0) return;
        
        foreach (var songFile in songDataFiles)
        {
            var songData = JsonUtility.FromJson<SongData>(songFile.text);

            var songInfo = new SongInfo();

            songInfo.songTitle = songData.songName;
            songInfo.songArtist = songData.songArtist;
            songInfo.songLevel = songData.difficulty;
            
            LoadPlayerDataJson(songInfo);
            //songInfo.songBestScore = songData.bestScore;
            //songInfo.songBestCombo = songData.bestCombo;
            
            songs.Add(songInfo);
        }
    }

    private void LoadJsonInLocalFolder()
    {
        string directory = Path.Combine(Application.persistentDataPath, "ChartData");

        if (!Directory.Exists(directory)) return;

        string[] filePaths = Directory.GetFiles(directory, "*.json");

        if (filePaths.Length == 0) return;

        foreach (var songFile in filePaths)
        {
            var jsonFile = File.ReadAllText(songFile);
            var songData = JsonUtility.FromJson<SongData>(jsonFile);

            var songInfo = new SongInfo();
            
            songInfo.songTitle = songData.songName;
            songInfo.songArtist = songData.songArtist;
            songInfo.songLevel = songData.difficulty;
            
            LoadPlayerDataJson(songInfo);
            //songInfo.songBestScore = songData.bestScore;
            //songInfo.songBestCombo = songData.bestCombo;
            
            songs.Add(songInfo);
        }
    }

    private void LoadPlayerDataJson(SongInfo songInfo)
    {
        string dir = Path.Combine(Application.persistentDataPath, "PlayData");
    
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
            Debug.Log("PlayData 폴더가 생성되었습니다: " + dir);
        }

        string playerDataPath = Path.Combine(dir, $"{songInfo.songTitle}_PlayData.json");

        if (!File.Exists(playerDataPath))
        {
            Debug.Log("해당 곡에 대한 플레이 데이터가 없습니다: " + playerDataPath);
            songInfo.songBestScore = 0;
            songInfo.songBestCombo = 0;
        }
        else if (File.Exists(playerDataPath))
        {
            var playerData = SaveLoadHelper.LoadPlayerData<PlayerData>(playerDataPath);

            songInfo.songBestScore = playerData.bestScore;
            songInfo.songBestCombo = playerData.bestCombo;
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