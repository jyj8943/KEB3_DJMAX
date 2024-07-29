using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MainCanvasBlurControl : MonoBehaviour
{
    public GameObject globalVolumeObject; 
    public List<GameObject> excludeObjects; 

    private Volume globalVolume;
    private Dictionary<GameObject, int> originalLayers = new Dictionary<GameObject, int>(); 
    
    void Start()
    {
        if (globalVolumeObject != null)
        {
            globalVolume = globalVolumeObject.GetComponent<Volume>();
           
        }

        // ExcludeBlur 레이어가 존재하는지 확인
        if (LayerMask.NameToLayer("ExcludeBlur") == -1)
        {
            Debug.LogError("ExcludeBlur 레이어가 존재하지 않습니다. 프로젝트 설정에서 레이어를 추가하세요.");
        }

        
    }

    public void EnableBlur()
    {
        if (globalVolumeObject != null)
        {
            // 블러 처리 제외를 위해 오브젝트에 ExcludeBlur 레이어를 설정
            foreach (GameObject obj in excludeObjects)
            {
                if (obj != null)
                {
                    // 오브젝트의 원래 레이어를 저장
                    if (!originalLayers.ContainsKey(obj))
                    {
                        originalLayers[obj] = obj.layer;
                    }

                    obj.layer = LayerMask.NameToLayer("ExcludeBlur");
                }
            }

            // 블러 활성화
            globalVolumeObject.SetActive(true);
        }
    }

    public void DisableBlur()
    {
        if (globalVolumeObject != null)
        {
            // 블러 비활성화
            globalVolumeObject.SetActive(false);

            foreach (GameObject obj in excludeObjects)
            {
                if (obj != null && originalLayers.ContainsKey(obj))
                {
                    // 블러 처리 제외를 해제하기 위해 원래 레이어로 복원
                    obj.layer = originalLayers[obj];
                }
            }

            // 원래 레이어 정보를 초기화
            originalLayers.Clear();
        }
    }
}