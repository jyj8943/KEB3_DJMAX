using UnityEngine;
using UnityEngine.UI;

public class JudgeImageUpdater : MonoBehaviour
{
    public Image judgeImage; // UI 이미지 컴포넌트
    public Sprite perfectSprite;
    public Sprite greatSprite;
    public Sprite goodSprite;
    public Sprite missSprite;
    public Image comboImage;
    
    public Animator judgeAnimator;
    
    private float timer;
    private const float inactiveTime = 1.0f;

    public static string judgeResult;
    private void Start()
    {
        judgeImage.enabled = false;
        comboImage.enabled = false;
    }
    private void Update()
    {
        if (Button.isJudged)
        {
            comboImage.enabled = true;
           
            judgeResult = Button.judgeResult;

            switch (judgeResult)
            {
                case "PERFECT":
                    judgeImage.sprite = perfectSprite;
                    judgeImage.enabled = true;
                    break;
                case "GREAT":
                    judgeImage.sprite = greatSprite;
                    judgeImage.enabled = true;
                    break;
                case "GOOD":
                    judgeImage.sprite = goodSprite;
                    judgeImage.enabled = true;
                    break;
                case "MISS":
                    judgeImage.sprite = missSprite;
                    judgeImage.enabled = true;
                    break;
                case "PASS":
                    judgeImage.enabled = true;
                    break;
            }
            judgeAnimator.SetTrigger("Judge");
            timer = 0.0f;
            Button.isJudged = false;
        }
        else if (InGamePlayManager.isNoteMiss)
        {
            comboImage.enabled = true;
            
            judgeImage.sprite = missSprite;
            judgeImage.enabled = true;
            judgeAnimator.SetTrigger("Judge");
            timer = 0.0f;
            InGamePlayManager.isNoteMiss = false;
        }
        else
        {
            timer += Time.deltaTime;
            
            if (timer >= inactiveTime)
            {
                judgeImage.enabled = false;
            }
        }
    }
}
