using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChartReader : MonoBehaviour
{
    public GameObject chart;
    public GameObject grids;
    public Scrollbar scrollbar;
    public float gridCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        chart.transform.position = new Vector3(0f, scrollbar.value, 0f);
    }
}
