using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultTest : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F10))
        {
            SceneManager.LoadScene("Result");
        }
    }
}
