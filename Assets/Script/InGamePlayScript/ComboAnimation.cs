using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAnimation : MonoBehaviour
{
    public Animator animator; // Animator 컴포넌트

    // 이 함수를 호출하면 애니메이션이 실행됩니다.
    public void ShowText()
    {
        animator.SetTrigger("SlideUp"); // SlideUp 트리거를 설정하여 애니메이션 시작
    }
}
