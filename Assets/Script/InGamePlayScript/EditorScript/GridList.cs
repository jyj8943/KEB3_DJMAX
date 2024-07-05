using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridList : MonoBehaviour
{
    public List<GameObject> gridList = new();
    
    public GameObject grid;
    
    public ChartReader chartReader;

    void Start()
    {
        transform.localPosition = Vector3.zero;

        for (int i = 0; i < EditorManager.instance.maxGridCount; i++)
        {
            var tempGrid = Instantiate(grid) as GameObject;
            tempGrid.transform.SetParent(transform, false);
            tempGrid.transform.position = new Vector3(0f, 1 + i * EditorManager.instance.gridHeight, 0f);
            gridList.Add(tempGrid);
        }

    }

    void Update()
    {
        transform.localPosition = Vector3.zero;
    }

    public void changeGridHeight()
    {
        for (int i = gridList.Count - 1; i >= 0; i--)
        {
            var tempLine = gridList[i];
            tempLine.GetComponent<Transform>().position = new Vector3(0f, 1f + i * EditorManager.instance.gridHeight, 0f);
        }
    }
}
