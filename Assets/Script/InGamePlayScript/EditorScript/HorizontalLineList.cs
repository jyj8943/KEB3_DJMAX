using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalLineList : MonoBehaviour
{
    public List<GameObject> horizontalLineList = new();

    public GameObject horizontalLine;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = Vector3.zero;

        // 2���� ������ ������ 1�ʸ� �ǹ��ϰԲ� �����ؾ���
        // ( �׸����� ���� * �ִ� �׸��� ���� ) / ( �⺻ ä�� �ӵ� )
        for (int i = 0; i < ( EditorManager.instance.maxGridCount * 4f ); i++)
        {
            var tempLine = Instantiate(horizontalLine) as GameObject;
            tempLine.transform.SetParent(transform, false);
            tempLine.transform.position = new Vector3(0f, -3f + i * EditorManager.instance.verticalLineHeight, 0f);
            horizontalLineList.Add(tempLine);
        }
    }

    // Update is called once per frame
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
}
