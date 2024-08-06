using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ChangeButtonState : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image basicImage;
    public Image changedImage;

    void Start()
    {
        // 초기 상태 설정
        basicImage.gameObject.SetActive(true);
        changedImage.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        basicImage.gameObject.SetActive(false);
        changedImage.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        basicImage.gameObject.SetActive(true);
        changedImage.gameObject.SetActive(false);
    }
}
