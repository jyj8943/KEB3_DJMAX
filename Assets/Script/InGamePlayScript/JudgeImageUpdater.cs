using UnityEngine;
using UnityEngine.UI;

public class JudgeImageUpdater : MonoBehaviour
{
    public Image judgeImage; // UI 이미지 컴포넌트
    public Sprite perfectSprite;
    public Sprite greatSprite;
    public Sprite goodSprite;
    public Sprite missSprite;
    
    private float timer;
    private const float inactiveTime = 1.0f;
    
    private void Start()
    {
        judgeImage.enabled = false;
    }
    private void Update()
    {
        if (Button.isJudged)
        {
            string judgeResult = Button.judgeResult;

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
            }
            timer = 0.0f;
            Button.isJudged = false;
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
