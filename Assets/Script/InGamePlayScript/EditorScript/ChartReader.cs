using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using SimpleFileBrowser;
using UnityEngine.Networking;
using UnityEngine.Audio;
using UnityEngine.Video;

public class ChartReader : MonoBehaviour
{
    public NoteList tempNoteList;
    public AudioManager audioManager;

    public RawImage movieScreen;
    public VideoPlayer videoPlayer;
    public RenderTexture renderTexture;

    public GameObject shorNotePrefab;
    public GameObject longNotePrefab;
    public GameObject basicScreen;
    public string jsonDir;

    private void Awake()
    {
        movieScreen.texture = null;
    }

    public void SaveData()
    {
        var data = new SongData("testSong", "ChartData/testSong");

        // 곡 이름이랑 재생시간 가져오도록 작성해야함
        data.songName = "testSong";
        data.songRunningTime = 120f; // 노래 시간 가져와야함
        data.bpm = 120; // 해당 노래 bpm 가져와야함
        data.difficulty = 3; // 해당 노래 채보의 레벨 가져와함 (1 ~ 10 예상중)

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
        FileBrowser.SetFilters(false, "json");

        StartCoroutine(ShowLoadDialogCoroutineForJson());
        
    }

    private IEnumerator ShowLoadDialogCoroutineForJson()
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, "Load Json File", "Load");

        if (FileBrowser.Success)
        {
            jsonDir = FileBrowser.Result[0];
        }
        Debug.Log(jsonDir);

        var jsonName = Path.GetFileName(jsonDir);
        Debug.Log(jsonName);

        if (string.IsNullOrEmpty(jsonDir)) yield break;

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
            var noteID = noteData.noteID;
            var noteStartingTime = noteData.noteStartingTime;
            var noteHoldingTime = noteData.noteHoldingTime;
            
            // var posY = noteData.posY;
            // var scale = noteData.scale;
            // var defaultDist = noteData.defaultDist;
            // var distUpPosY = noteData.distUpPosY;

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

            float posY = TotalManager.instance.minNotePosY + TotalManager.instance.userChartSpeed *
                TotalManager.instance.defaultChartSpeed * noteStartingTime;
            
            var pos = new Vector3(posX, posY, 1f);

            if (noteID == 0)
            {
                var shortNote = Instantiate(shorNotePrefab, pos, Quaternion.identity);

                shortNote.transform.SetParent(tempNoteList.transform, false);
                shortNote.GetComponent<ShortNote>().InitNote();

                //tempNoteList.noteList.Add(shortNote);
                tempNoteList.GetComponent<NoteList>().SortingNotes(shortNote);
            }
            else if (noteID == 1)
            {
                var longNote = Instantiate(longNotePrefab, pos, Quaternion.identity);

                longNote.transform.SetParent(tempNoteList.transform, false);

                longNote.transform.localScale = new Vector3(1f, noteHoldingTime * TotalManager.instance.userChartSpeed, 1f);

                var distUpPosY = (pos.y + longNote.transform.localScale.y);
                longNote.GetComponent<LongNote>().InitNote(distUpPosY);

                //tempNoteList.noteList.Add(longNote);
                tempNoteList.GetComponent<NoteList>().SortingNotes(longNote);
            }
        }
    }

    // SimpleFileBrowser 에셋으로 파일 브라우저 기능을 구현
    // https://github.com/yasirkula/UnitySimpleFileBrowser?tab=readme-ov-file
    public void LoadSong()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Videos", ".mp4", ".avi", ".mov"));

        FileBrowser.SetDefaultFilter(".mp4");

        FileBrowser.ShowLoadDialog(OnFileSelected, null, FileBrowser.PickMode.Files, false, null, null, "Select Video File", "Select");
    }

    void OnFileSelected(string[] paths)
    {
        if (paths.Length > 0)
        {
            string filePath = paths[0];
            basicScreen.SetActive(false);
            StartCoroutine(DownloadAndPlayVideo(filePath));
        }
    }

    IEnumerator DownloadAndPlayVideo(string url)
    {
        using (UnityWebRequest uwr = UnityWebRequest.Get(url))
        {
            string filePath = System.IO.Path.Combine(Application.persistentDataPath, "downloadedVideo.mp4");
            uwr.downloadHandler = new DownloadHandlerFile(filePath);

            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(uwr.error);
            }
            else
            {
                Debug.Log("Video successfully downloaded and saved to " + filePath);

                videoPlayer.url = filePath;
                videoPlayer.Prepare();

                while (!videoPlayer.isPrepared)
                {
                    yield return null;
                }

                if (movieScreen.texture != null)
                    movieScreen.texture = null;

                movieScreen.texture = videoPlayer.texture;
                audioManager.InitSong();
                audioManager.isLoadedSong = true;
            }
        }
    }
}
