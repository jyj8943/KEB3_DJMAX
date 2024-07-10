using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class JudgementBar : MonoBehaviour
{
    public GameObject nearNote;
    public int spaceCount = 0;

    public GameObject gameManager;

    public GameObject[] tempNotes;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tempNotes = (GameObject[])gameManager.GetComponent<GameManager>().shortnoteArray.Clone();

        if (Input.GetKeyDown(KeyCode.Space) && spaceCount < tempNotes.Length)
        {
            nearNote = tempNotes[spaceCount];

            checkJudege(nearNote);

            spaceCount++;
        }
    }

    public void checkJudege(GameObject nearNote)
    {
        //if (nearNote.GetComponent<ShortNote>().noteTimer > (nearNote.GetComponent<ShortNote>().noteArriveTime * 0.99f)
        //    && nearNote.GetComponent<ShortNote>().noteTimer < (nearNote.GetComponent<ShortNote>().noteArriveTime * 1.01f))
        //{
        //    Debug.Log("MAX 100%");
        //    Destroy(nearNote);
        //}
        //else if (nearNote.GetComponent<ShortNote>().noteTimer > (nearNote.GetComponent<ShortNote>().noteArriveTime * 0.97f)
        //    && nearNote.GetComponent<ShortNote>().noteTimer < (nearNote.GetComponent<ShortNote>().noteArriveTime * 1.03f))
        //{
        //    Debug.Log("MAX 90%");
        //    Destroy(nearNote);
        //}
        //else if (nearNote.GetComponent<ShortNote>().noteTimer > (nearNote.GetComponent<ShortNote>().noteArriveTime * 0.95f)
        //    && nearNote.GetComponent<ShortNote>().noteTimer < (nearNote.GetComponent<ShortNote>().noteArriveTime * 1.05f))
        //{
        //    Debug.Log("MAX 80%");
        //    Destroy(nearNote);
        //}
        //else if (nearNote.GetComponent<ShortNote>().noteTimer > (nearNote.GetComponent<ShortNote>().noteArriveTime * 0.90f)
        //    && nearNote.GetComponent<ShortNote>().noteTimer < (nearNote.GetComponent<ShortNote>().noteArriveTime * 1.10f))
        //{
        //    Debug.Log("MAX 70%");
        //    Destroy(nearNote);
        //}
        //else if (nearNote.GetComponent<ShortNote>().noteTimer > (nearNote.GetComponent<ShortNote>().noteArriveTime * 0.80f)
        //    && nearNote.GetComponent<ShortNote>().noteTimer < (nearNote.GetComponent<ShortNote>().noteArriveTime * 1.20f))
        //{
        //    Debug.Log("MAX 1%");
        //    Destroy(nearNote);
        //}
        //else
        //{
        //    Debug.Log("BREAK");
        //    Destroy(nearNote);
        //}
    }

}
