using UnityEngine;

public class Goal : MonoBehaviour
{
    public enum Side { Left, Right }
    public Side side;
    public GameManager manager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.attachedRigidbody) return;
        if (!other.attachedRigidbody.GetComponent<Ball>()) return;

        if (side == Side.Left) manager.GoalLeft();
        else manager.GoalRight();
    }
}