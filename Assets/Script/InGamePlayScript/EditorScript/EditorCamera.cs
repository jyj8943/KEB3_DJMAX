using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EditorCamera : MonoBehaviour
{
    public HorizontalLineList horizontalLineList;
    public GridList gridList;
    public NoteList noteList;
    public AudioManager audioManager;
    public Slider slider;

    public bool isPlaying = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Editor");
        }

        if (Input.GetKeyDown(KeyCode.W) && transform.position.y < EditorManager.instance.maxGridCount * 8)
        {
            transform.Translate(0f, 8f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.S) && transform.position.y > 0)
        {
            transform.Translate(0f, -8f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isPlaying)
            {
                isPlaying = false;
                audioManager.pauseSong();
            }
            else
            {
                isPlaying = true;
                audioManager.playSong();
            }
        }
        if (isPlaying) transform.Translate(0f, EditorManager.instance.defaultChartSpeed
                                               * EditorManager.instance.userChartSpeed * Time.deltaTime, 0f);

        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput > 0 && transform.position.y < EditorManager.instance.maxGridCount * 8 - 7)
        {
            transform.Translate(0f, wheelInput * 3, 0f);

            if (transform.position.y > EditorManager.instance.maxGridCount * 8 - 7)
                transform.position = new Vector3(transform.position.x, EditorManager.instance.maxGridCount * 8 - 7, transform.position.z);
            
            audioManager.bgm.time = transform.position.y / (EditorManager.instance.userChartSpeed * EditorManager.instance.defaultChartSpeed);
        }
        else if (wheelInput < 0 && transform.position.y > 0)
        {
            transform.Translate(0f, wheelInput * 3, 0f);

            if (transform.position.y < 0)
                transform.position = new Vector3(transform.position.x, 0f, transform.position.z);

            audioManager.bgm.time = transform.position.y / (EditorManager.instance.userChartSpeed * EditorManager.instance.defaultChartSpeed);
        }

        if (Input.GetKeyDown(KeyCode.E) && EditorManager.instance.userChartSpeed < EditorManager.instance.maxUserChartSpeed)
        {
            var defaultCameraDist = transform.position.y /EditorManager.instance.userChartSpeed;

            EditorManager.instance.changeUserChartSpeed(0.5f);
            horizontalLineList.changeHorizontalLineHeight();
            gridList.changeGridHeight();
            noteList.changeNotePosY();


            transform.position = new Vector3(transform.position.x, defaultCameraDist * EditorManager.instance.userChartSpeed, transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.D) && EditorManager.instance.userChartSpeed > 1f)
        {
            var defaultCameraDist = transform.position.y / EditorManager.instance.userChartSpeed;

            EditorManager.instance.changeUserChartSpeed(-0.5f);
            horizontalLineList.changeHorizontalLineHeight();
            gridList.changeGridHeight();
            noteList.changeNotePosY();


            transform.position = new Vector3(transform.position.x, defaultCameraDist * EditorManager.instance.userChartSpeed, transform.position.z);
        }
    }
}
