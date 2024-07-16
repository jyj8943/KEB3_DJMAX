using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using SimpleFileBrowser;

public class ChartReader : MonoBehaviour
{
    public NoteList tempNoteList;

    public GameObject shorNotePrefab;
    public GameObject longNotePrefab;

    private void Start()
    {
        FileBrowser.SetDefaultFilter(".mp3");
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);
    }

    public void SaveData()
    {
        var data = new SongData("testSong", "ChartData/testSong");

        // 곡 이름이랑 재생시간 가져오도록 작성해야함
        data.songName = "testSong";
        data.songRunningTime = 120f;
        data.bpm = 120;

        var noteData = new SongData.NoteData[tempNoteList.noteList.Count];

        for (int i = 0; i < tempNoteList.noteList.Count; i++)
        {
            if (tempNoteList.noteList[i].tag == "ShortNote")
                noteData[i] = tempNoteList.noteList[i].GetComponent<ShortNote>().GetNoteData();
            else if (tempNoteList.noteList[i].tag == "LongNote")
                noteData[i] = tempNoteList.noteList[i].GetComponent<LongNote>().GetNoteData();
        }

        data.notes = noteData;

        SaveLoadHelper.SaveData(data);
    }

    public void LoadData()
    {
        // 유저가 로드 버튼을 눌렀을 때 로컬 파일 창이 열리면서 채보 파일을 가져오게 한 후
        // 그 채보 파일의 곡 이름과 같은 노래를 가져오도록 짜야함
        var jsonDir = EditorUtility.OpenFilePanel("Json File", Application.persistentDataPath + "/ChartData/testSong", "json");
        Debug.Log(jsonDir);

        var jsonName = Path.GetFileName(jsonDir);
        Debug.Log(jsonName);

        if (string.IsNullOrEmpty(jsonDir)) return;

        var data = SaveLoadHelper.LoadData<SongData>(jsonName, "ChartData/testSong");

        foreach (var tempNote in tempNoteList.noteList)
        {
            Destroy(tempNote.gameObject);
        }
        tempNoteList.noteList.Clear();



        for (int i = 0; i < data.notes.Length; i++)
        {
            var noteData = data.notes[i];

            var railNum = noteData.railNum;
            var posY = noteData.posY;
            var noteID = noteData.noteID;
            var scale = noteData.scale;
            var defaultDist = noteData.defaultDist;
            var distUpPosY = noteData.distUpPosY;

            float posX = 0f;
            switch (railNum)
            {
                case 1:
                    posX = -1.5f;
                    break;
                case 2:
                    posX = -0.5f;
                    break;
                case 3:
                    posX = 0.5f;
                    break;
                case 4:
                    posX = 1.5f;
                    break;
            }

            var pos = new Vector3(posX, posY, 1f);

            if (noteID == 0)
            {
                var shortNote = Instantiate(shorNotePrefab, pos, Quaternion.identity);

                shortNote.transform.SetParent(tempNoteList.transform, false);

                tempNoteList.noteList.Add(shortNote);
            }
            else if (noteID == 1)
            {
                var longNote = Instantiate(longNotePrefab, pos, Quaternion.identity);

                longNote.transform.SetParent(tempNoteList.transform, false);

                longNote.transform.localScale = new Vector3(1f, scale, 1f);
                longNote.GetComponent<LongNote>().InitLongNote(distUpPosY);

                tempNoteList.noteList.Add(longNote);
            }
        }
    }

    // SimpleFileBrowser 에셋으로 파일 브라우저 기능을 구현
    // https://github.com/yasirkula/UnitySimpleFileBrowser?tab=readme-ov-file
    public void LoadSong()
    {
        var filters = FileBrowser.SetDefaultFilter(".mp3");

        FileBrowser.SetFilters(false);

        FileBrowser.ShowLoadDialog( (paths) => { Debug.Log("Selected: " + paths[0]); },
            () => { Debug.Log("Canceled");  },
            FileBrowser.PickMode.Files, false, null, null, "Select MP3 File", "Select");
    }

    public delegate void OnSuccess(string[] paths);
    public delegate void OnCancel();
}
