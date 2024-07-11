using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public EditorCamera editorCamera;
    public AudioSource bgm;

    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator waitAndPlaySong()
    {
        yield return new WaitForSeconds(0.5f);
    }

    public void playSong()
    {
        if (isPaused)
        {
            waitAndPlaySong();
        }
        bgm.Play();
    }

    public void pauseSong()
    {
        bgm.Pause();
    }
}
