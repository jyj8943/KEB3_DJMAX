using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Button : MonoBehaviour
{
    public GameObject buttonEffect;
    public KeyCode Key;
    //public GameObject effectC;
    //public GameObject effectD;

    public SpriteRenderer buttonImage;
    public Sprite upImage;
    public Sprite downImage;

    // public GameObject OnEffectX;
    // public GameObject OnEffectY; 
    // public GameObject OnEffectZ;
    // public GameObject OnEffectW;
    //
    // public GameObject DownEffectX;
    // public GameObject DownEffectY;
    // public GameObject DownEffectZ;
    // public GameObject DownEffectW;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            buttonEffect.SetActive(true);
            buttonImage.sprite = downImage;
        }
        
        if(Input.GetKeyUp(Key))
        {
            buttonEffect.SetActive(false);
            buttonImage.sprite = upImage;
        }
    }
}