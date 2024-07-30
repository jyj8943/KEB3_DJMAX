using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData 
{
    private string fileName;
    private string directory;

    public SaveData(string _fileName, string _directory)
    {
        fileName = _fileName;
        directory = _directory;
    }

    public string GetFullPath() => GetDriectory() + "/" + fileName + ".json";

    public string GetDriectory() => Application.persistentDataPath + "/" + directory;
}

public class SongData : SaveData
{
    public string songName;
    public float songRunningTime;
    public int bpm;
    public int difficulty;
    public NoteData[] notes;

    public SongData(string _filename, string _directory) : base(_filename, _directory)
    {
        
    }

    [Serializable]
    public class NoteData
    {
        public float railNum;
        public int noteID;
        public float noteStartingTime; // 노트를 눌러야할 시간
        public float noteHoldingTime; // 롱노트에서 노트를 끝까지 눌러야할 시간
        //public float posY;
        // public float scale;
        // public float defaultDist;
        // public float distUpPosY;
    }
}
