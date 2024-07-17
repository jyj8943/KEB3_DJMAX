using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridList : MonoBehaviour
{
    public List<GameObject> gridList = new();
    public List<GameObject> gridCountList = new();
    
    public GameObject grid;
    public GameObject gridCount;

    public float girdCount = 0;

    void Start()
    {
        transform.localPosition = Vector3.zero;

        MakeGrids();
    }

    void Update()
    {
        transform.localPosition = Vector3.zero;
    }

    public void changeGridHeight()
    {
        for (int i = gridList.Count - 1; i >= 0; i--)
        {
            var tempGrid = gridList[i];
            tempGrid.transform.localScale = new Vector3(4f, 8 * EditorManager.instance.userChartSpeed, 1f);
            tempGrid.GetComponent<Transform>().position = new Vector3(0f, -3f + 0.5f * EditorManager.instance.gridHeight 
                + i * EditorManager.instance.gridHeight, 0f);

            var tempCount = gridCountList[i];
            tempCount.GetComponent<Transform>().position = new Vector3(-1f, tempGrid.transform.position.y -0.5f + 
                EditorManager.instance.gridHeight / 2f, 0f);
            tempCount.GetComponent<Transform>().localScale = new Vector3(0.5f, 1 / (EditorManager.instance.gridHeight), 1f);
        }
    }

    public void MakeGrids()
    {
        foreach (var tempGrid in gridList)
        {
            Destroy(tempGrid.gameObject);
        }
        gridList.Clear();

        foreach (var tempGridCount in gridCountList)
        {
            Destroy(tempGridCount.gameObject);
        }
        gridCountList.Clear();

        for (int i = 0; i < EditorManager.instance.maxGridCount; i++)
        {
            var tempGrid = Instantiate(grid) as GameObject;
            tempGrid.transform.SetParent(transform, false);
            tempGrid.transform.position = new Vector3(0f, 1 + i * EditorManager.instance.gridHeight, 0f);

            gridList.Add(tempGrid);
        }

        for (int i = 0; i < EditorManager.instance.maxGridCount; i++)
        {
            var tempCount = Instantiate(gridCount) as GameObject;
            tempCount.transform.SetParent(transform, false);

            tempCount.GetComponentInChildren<TMP_Text>().SetText((4 * (i + 1)).ToString());

            tempCount.transform.SetParent(gridList[i].transform, true);

            var tempPos = gridList[i].transform.position.y;
            tempCount.transform.position = new Vector3(-1f, tempPos - 0.5f + EditorManager.instance.gridHeight / 2f, 0f);

            gridCountList.Add(tempCount);
        }
    }
}
