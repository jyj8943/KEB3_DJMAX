using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameSettingManager : MonoBehaviour
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

    private float sync;
    private float trackSpeed;

    private void Awake()
    {
        TM = TotalManager.instance;
    }

    void Start()
    {
        sync = TM.userSync;
        trackSpeed = TM.userChartSpeedSetting;
        
        syncText.text = TM.userSync.ToString("F1");
        trackSpeedText.text = TM.userChartSpeedSetting.ToString("F1");
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

        if(Input.GetKeyDown(KeyCode.Return))
        {
            TM.SetSyncTrackSpeed(sync, trackSpeed);
            SceneManager.LoadScene(TM.prevScene);
        }
    }

    void SyncUpdate()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(sync < TM.maxUserSync)
            {
                sync += 0.1f;
            }
            isHoldingRight = true;
            timer = 0f;
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(sync > 1f)
            {
                sync -= 0.1f;
            }
            isHoldingLeft = true;
            timer = 0f;
        }

        if(Input.GetKey(KeyCode.RightArrow) && isHoldingRight)
        {
            timer += Time.deltaTime;

            if(timer >= holdingTime)
            {
                if(sync < TM.maxUserSync)
                {
                    sync += 0.1f;
                }
                timer = 0f;
            }
        }
        if(Input.GetKey(KeyCode.LeftArrow) && isHoldingLeft)
        {
            timer += Time.deltaTime;

            if(timer >= holdingTime)
            {
                if(sync > 1f)
                {
                    sync -= 0.1f;
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

        sync = Mathf.Round(sync * 10f) / 10f;

        syncText.text = sync.ToString("F1");
    }

    void SpeedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (trackSpeed < TM.maxUserChartSpeed)
            {
                trackSpeed += 0.1f;
            }
            isHoldingRight = true;
            timer = 0f;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (trackSpeed > 1f)
            {
                trackSpeed -= 0.1f;
            }
            isHoldingLeft = true;
            timer = 0f;
        }
        
        if (Input.GetKey(KeyCode.RightArrow) && isHoldingRight)
        {
            timer += Time.deltaTime;

            if (timer >= holdingTime)
            {
                if (trackSpeed < TM.maxUserChartSpeed)
                {
                    trackSpeed += 0.1f;
                }
                timer = 0f;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow) && isHoldingLeft)
        {
            timer += Time.deltaTime;

            if (timer >= holdingTime)
            {
                if (trackSpeed > 1f)
                {
                    trackSpeed -= 0.1f;
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

        trackSpeed = Mathf.Round(trackSpeed * 10f) / 10f;
        
        trackSpeedText.text = trackSpeed.ToString("F1");
    }
}
