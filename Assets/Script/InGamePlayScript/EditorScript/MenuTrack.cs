using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTrack : MonoBehaviour
{
    public void GoToTitleMenu()
    {
        SceneManager.LoadScene("TitleMenu");
    }

    public void GoToTrackSelect()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
