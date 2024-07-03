using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public GameObject shortNote;

    public GameObject[] shortnoteArray;

    public int temp;

    private WaitForSeconds wait = new WaitForSeconds(0.5f);

    // Start is called before the first frame update
    void Start()
    {
        temp = Random.Range(1, 5);
        shortnoteArray = new GameObject[temp];

        StartCoroutine(makeDelay());
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    private IEnumerator makeDelay()
    {
        for (int i = 0; i < temp; i++)
        {
            shortnoteArray[i] = Instantiate(shortNote, transform.position, shortNote.transform.rotation);

            yield return wait;
        }
    }

    public GameObject[] getNoteArray()
    {
        return shortnoteArray;
    }
}
