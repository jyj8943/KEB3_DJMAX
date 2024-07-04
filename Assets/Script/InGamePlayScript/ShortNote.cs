using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShortNote : MonoBehaviour
{
    public GameObject judgementBar;
    public GameObject failBar;

    public float timeScaler;
    public float noteTimer;
    public float noteDistance;
    public float noteArriveTime;

    // Start is called before the first frame update
    void Start()
    {
        noteTimer = 0f;
        noteDistance = transform.position.y - judgementBar.transform.position.y;
        noteArriveTime = noteDistance / timeScaler;
    }

    // Update is called once per frame
    void Update()
    {
        noteTimer += Time.deltaTime;
        transform.Translate(0f, -timeScaler * Time.deltaTime, 0f, Space.World);

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    checkJudege(noteTimer);
        //}
    }

    //public void checkJudege(float spaceTimer)
    //{
    //    if (noteTimer > (noteArriveTime * 0.99f) && noteTimer < (noteArriveTime * 1.01f))
    //    {
    //        Debug.Log("MAX 100%");
    //        Destroy(gameObject);
    //    }
    //    else if (noteTimer > (noteArriveTime * 0.97f) && noteTimer < (noteArriveTime * 1.03f))
    //    {
    //        Debug.Log("MAX 90%");
    //        Destroy(gameObject);
    //    }
    //    else if (noteTimer > (noteArriveTime * 0.95f) && noteTimer < (noteArriveTime * 1.05f))
    //    {
    //        Debug.Log("MAX 80%");
    //        Destroy(gameObject);
    //    }
    //    else if (noteTimer > (noteArriveTime * 0.90f) && noteTimer < (noteArriveTime * 1.10f))
    //    {
    //        Debug.Log("MAX 70%");
    //        Destroy(gameObject);
    //    }
    //    else if (noteTimer > (noteArriveTime * 0.80f) && noteTimer < (noteArriveTime * 1.20f))
    //    {
    //        Debug.Log("MAX 1%");
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        Debug.Log("BREAK");
    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FailBar")
        {
            Debug.Log("BREAK");
            Destroy(gameObject);
        }
    }
}
