using UnityEngine;

public class PaddleBounds : MonoBehaviour
{
    public float minY = -4.2f;
    public float maxY =  4.2f;
    public float fixedX = -8f;

    void LateUpdate()
    {
        Vector3 p = transform.position;
        p.y = Mathf.Clamp(p.y, minY, maxY);
        p.x = fixedX;
        transform.position = p;
    }
}