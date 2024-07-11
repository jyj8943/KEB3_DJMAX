using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNote : MonoBehaviour
{
    public void changeScale(float tempScale)
    {
        transform.localScale = new Vector3(1f, tempScale, 1f);
    }
}
