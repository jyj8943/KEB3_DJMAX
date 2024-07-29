using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Quitfn();
        }
    }

    public void Quitfn()
    {
        SceneManager.LoadScene("TitleMenu");
    }
}
