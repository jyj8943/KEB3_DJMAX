using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Button : MonoBehaviour
{
    public GameObject buttonEffect;
    public KeyCode Key;

    public SpriteRenderer buttonImage;
    public Sprite upImage;
    public Sprite downImage;

    void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            buttonEffect.SetActive(true);
            buttonImage.sprite = downImage;

            // 노트에 대한 판정 제작중

        }
        
        if(Input.GetKeyUp(Key))
        {
            buttonEffect.SetActive(false);
            buttonImage.sprite = upImage;
        }
    }
}