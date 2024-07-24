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

public class InGameChartReader : MonoBehaviour
{
    public GameObject shortNotePrefab; // 프리팹 이름 수정
    public GameObject longNotePrefab;

    public string jsonFileName = "testSong.json"; // JSON 파일 이름을 스크립트에 직접 저장
    public string jsonFilePath = "ChartData/testSong"; // JSON 파일 경로를 스크립트에 직접 저장

    private void Start()
    {
        LoadData();
        InGamePlayManager.instance.DivideList();
    }

    public void LoadData()
    {
        // 파일 탐색기를 사용하지 않고 지정된 파일 이름과 경로를 사용하여 JSON 파일을 로드
        var jsonDir = Path.Combine(Application.persistentDataPath, jsonFilePath, jsonFileName);
        Debug.Log("JSON 파일 경로: " + jsonDir);

        if (string.IsNullOrEmpty(jsonDir)) return;

        var data = SaveLoadHelper.LoadData<SongData>(jsonFileName, jsonFilePath);

        foreach (var tempNote in InGamePlayManager.instance.noteList)
        {
            Destroy(tempNote.gameObject);
        }
        InGamePlayManager.instance.noteList.Clear();

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
                var shortNote = Instantiate(shortNotePrefab, pos, Quaternion.identity); // 프리팹 이름 수정

                shortNote.transform.SetParent(InGamePlayManager.instance.transform, false);
                shortNote.GetComponent<ShortNote>().InitNote();

                InGamePlayManager.instance.noteList.Add(shortNote);
            }
            else if (noteID == 1)
            {
                var longNote = Instantiate(longNotePrefab, pos, Quaternion.identity);

                longNote.transform.SetParent(InGamePlayManager.instance.transform, false);

                longNote.transform.localScale = new Vector3(1f, scale, 1f);
                longNote.GetComponent<LongNote>().InitNote(distUpPosY);

                InGamePlayManager.instance.noteList.Add(longNote);
            }
        }
    }
}