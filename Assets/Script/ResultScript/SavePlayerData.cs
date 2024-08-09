using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavePlayerData : MonoBehaviour
{
    public TotalManager TM;
    
    void Start()
    {
        TM = TotalManager.instance;

        CompareScoreAndCombo();
    }

    private void CompareScoreAndCombo()
    {
        var data = new PlayerData(TM.tempSongName, "PlayData");

        data.songName = TM.tempSongName;
        data.songArtist = TM.tempSongArtist;
        
        string dir = Path.Combine(Application.persistentDataPath, "PlayData");
        string playerDataPath = Path.Combine(dir, $"{TM.tempSongName}_PlayData.json");

        if (!File.Exists(playerDataPath))
        {
            data.bestScore = TM.tempSongBestScore;
            data.bestCombo = TM.tempSongBestCombo;
        }
        else if (File.Exists(playerDataPath))
        {
            var recordedData = SaveLoadHelper.LoadPlayerData<PlayerData>(playerDataPath);

            if (TM.tempSongBestScore >= recordedData.bestScore)
                data.bestScore = TM.tempSongBestScore;
            else
                data.bestScore = recordedData.bestScore;

            if (TM.tempSongBestCombo >= recordedData.bestCombo)
                data.bestCombo = TM.tempSongBestCombo;
            else
                data.bestCombo = recordedData.bestCombo;
        }
        
        SaveLoadHelper.SavePlayerData(data, TM.tempSongName);
    }
}
