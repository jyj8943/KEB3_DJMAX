using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Try : MonoBehaviour
{
    public TotalManager TM;

    public GameObject syncBtn;
    public TextMeshProUGUI syncText;

    public GameObject trackSpeedBtn;
    public TextMeshProUGUI trackSpeedText;

    private float holdingTime = 0.15f;
    private float timer = 0f;
    private bool isHoldingRight = false;
    private bool isHoldingLeft = false;

    public float sync_;
    public float trackSpeed_;

    void Awake()
    {
        TM = TotalManager.instance;
    }

    void Start()
    {
        syncText.text = sync_.ToString("F1");
        trackSpeedText.text = trackSpeed_.ToString("F1");
    }

    void Update()
    {
        GameObject selectedObj = EventSystem.current.currentSelectedGameObject;
        
        if (selectedObj == syncBtn)
        {
            SyncUpdate();
        }
        else if (selectedObj == trackSpeedBtn)
        {
            SpeedUpdate();
        }
    }

    void SyncUpdate()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(sync_ < TM.maxSync)
            {
                sync_ += 0.1f;
            }
            isHoldingRight = true;
            timer = 0f;
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(sync_ > 1f)
            {
                sync_ -= 0.1f;
            }
            isHoldingLeft = true;
            timer = 0f;
        }

        if(Input.GetKey(KeyCode.RightArrow) && isHoldingRight)
        {
            timer += Time.deltaTime;

            if(timer >= holdingTime)
            {
                if(sync_ < TM.maxSync)
                {
                    sync_ += 0.1f;
                }
                timer = 0f;
            }
        }
        if(Input.GetKey(KeyCode.LeftArrow) && isHoldingLeft)
        {
            timer += Time.deltaTime;

            if(timer >= holdingTime)
            {
                if(sync_ > 1f)
                {
                    sync_ -= 0.1f;
                }
                timer = 0f;
            }
        }

        if(Input.GetKeyUp(KeyCode.RightArrow))
        {
            isHoldingRight = false;
            timer = 0f;
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            isHoldingLeft = false;
            timer = 0f;
        }

        sync_ = Mathf.Round(sync_ * 10f) / 10f;

        syncText.text = sync_.ToString("F1");
    }

    void SpeedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (trackSpeed_ < TM.maxUserChartSpeed)
            {
                trackSpeed_ += 0.1f;
            }
            isHoldingRight = true;
            timer = 0f;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (trackSpeed_ > 1f)
            {
                trackSpeed_ -= 0.1f;
            }
            isHoldingLeft = true;
            timer = 0f;
        }
        
        if (Input.GetKey(KeyCode.RightArrow) && isHoldingRight)
        {
            timer += Time.deltaTime;

            if (timer >= holdingTime)
            {
                if (trackSpeed_ < TM.maxUserChartSpeed)
                {
                    trackSpeed_ += 0.1f;
                }
                timer = 0f;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow) && isHoldingLeft)
        {
            timer += Time.deltaTime;

            if (timer >= holdingTime)
            {
                if (trackSpeed_ > 1f)
                {
                    trackSpeed_ -= 0.1f;
                }
                timer = 0f;
            }
        }
        
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            isHoldingRight = false;
            timer = 0f;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            isHoldingLeft = false;
            timer = 0f;
        }

        trackSpeed_ = Mathf.Round(trackSpeed_ * 10f) / 10f;
        
        trackSpeedText.text = trackSpeed_.ToString("F1");
    }

}
