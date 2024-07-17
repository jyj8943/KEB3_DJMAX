using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalLineList : MonoBehaviour
{
    public List<GameObject> horizontalLineList = new();

    public GameObject horizontalLine;

    void Start()
    {
        transform.localPosition = Vector3.zero;

        MakeHorizontalLine();
    }

    void Update()
    {
        transform.localPosition = Vector3.zero;
    }

    public void changeHorizontalLineHeight()
    {
        for (int i = horizontalLineList.Count - 1; i >= 0; i--)
        {
            var tempLine = horizontalLineList[i];
            tempLine.GetComponent<Transform>().position = new Vector3(0f, -3f + i * EditorManager.instance.verticalLineHeight, 0f);
        }
    }

    public void MakeHorizontalLine()
    {
        foreach (var tempHorizontalLine in horizontalLineList)
        {
            Destroy(tempHorizontalLine.gameObject);
        }
        horizontalLineList.Clear();

        for (int i = 0; i < (EditorManager.instance.maxGridCount * 4 + 1); i++)
        {
            var tempLine = Instantiate(horizontalLine) as GameObject;
            tempLine.transform.SetParent(transform, false);
            tempLine.transform.position = new Vector3(0f, -3f + i * EditorManager.instance.verticalLineHeight, 0f);
            horizontalLineList.Add(tempLine);
        }
    }
}
