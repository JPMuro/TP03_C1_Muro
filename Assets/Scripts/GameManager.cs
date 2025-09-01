using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Ball ball;

    [Header("UI")]
    public TMP_Text leftScoreText;
    public TMP_Text rightScoreText;

    int leftScore = 0;
    int rightScore = 0;

    public void GoalLeft()
    {
        rightScore++;
        UpdateUI();
        ball.ResetAndLaunch();
    }

    public void GoalRight()
    {
        leftScore++;
        UpdateUI();
        ball.ResetAndLaunch();
    }

    void UpdateUI()
    {
        if (leftScoreText) leftScoreText.text = leftScore.ToString();
        if (rightScoreText) rightScoreText.text = rightScore.ToString();
    }
}