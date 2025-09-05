using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    private int scoreP1 = 0, scoreP2 = 0;

    public Transform ball;

    void Update()
    {
        if (ball.position.x < -9f)
        {
            scoreP2++;
            ResetBall();
        }
        else if (ball.position.x > 9f)
        {
            scoreP1++;
            ResetBall();
        }

        scoreText.text = scoreP1 + " : " + scoreP2;
    }

    void ResetBall()
    {
        ball.position = Vector2.zero;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ball.GetComponent<Ball>().SendMessage("Launch");
    }
}