// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.EventSystems;

// public class GameSettingManager : MonoBehaviour
// {
//     public TotalManager TM;

//     public GameObject syncBtn;
//     public TextMeshProUGUI sync;
//     private float sync_ = 1.0f;

//     public GameObject trackSpeedBtn;
//     public TextMeshProUGUI trackSpeed;
//     private float trackSpeed_ = 1.0f;

//     private float holdingTime = 0.15f;
//     private float timer = 0f;
//     private bool isHoldingUp = false;
//     private bool isHoldingDown = false;

//     void Awake()
//     {
//         TM = TotalManager.instance;
//     }

//     void Start()
//     {
//         sync.text = sync_.ToString("F1");
//         trackSpeed.text = trackSpeed_.ToString("F1");
//     }

//     void Update()
//     {
//         GameObject selectedObj = EventSystem.current.currentSelectedGameObject;

//         if(Input.GetKeyDown(KeyCode.RightArrow))
//         {
//             if(selectedObj == syncBtn)
//             {
//                 SetSyncUp();
//             }
//             else if(selectedObj == trackSpeedBtn)
//             {
                
//             }
//         }
//         else if(Input.GetKeyDown(KeyCode.LeftArrow))
//         {
//             if(selectedObj == syncBtn)
//             {
//                 SetSyncDown();
//             }
//             else if(selectedObj == trackSpeedBtn)
//             {
//                 trackSpeed_ -= 0.1f;
//                 trackSpeed.text = trackSpeed_.ToString("F1");
//             }
//         }
//     }

//     void SetSyncUp()
//     {
//         sync_ += 1f;
//         sync.text = sync_.ToString("F1");
//     }

//     void SetSyncDown()
//     {
//         sync_ -= 1f;
//         sync.text = sync_.ToString("F1");
//     }

//     void SetTrackSpeedUp()
//     {
//     }

//     void SetTrackSpeedDown()
//     {
//     }

// }
