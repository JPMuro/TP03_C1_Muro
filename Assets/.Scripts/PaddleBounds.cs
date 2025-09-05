using UnityEngine;

public class PaddleBounds : MonoBehaviour
{
    public float minY = -3.8f;
    public float maxY =  3.8f;
    public float fixedX = -8f;
    public float height = 1;

    void LateUpdate()
    {
        Vector3 p = transform.position;
        p.y = Mathf.Clamp(p.y, minY, maxY);
        p.x = fixedX;
        transform.position = p;
    }
}