using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChartReader : MonoBehaviour
{
    public NoteList tempNoteList;

    public void SaveData()
    {
        var data = new SongData("TestData", "Test/Bootcamp");

        // 곡 이름이랑 재생시간 가져오도록 작성해야함
        data.songName = "example1";
        data.songRunningTime = 120f;
        data.bpm = 120;

        var noteData = new SongData.NoteData[tempNoteList.noteList.Count];

        for (int i = 0; i < tempNoteList.noteList.Count; i++)
        {
            noteData[i] = tempNoteList.noteList[i].GetComponent<ShortNote>().GetNoteData();
        }

        data.notes = noteData;

        SaveLoadHelper.SaveData(data);
    }
}
