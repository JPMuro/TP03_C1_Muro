using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [Header("Velocidad")]
    public float speed = 10f;
    public float speedIncrementOnHit = 0.5f;

    [Header("Rango de ángulo inicial")]
    [Range(10f, 70f)] public float minLaunchAngle = 20f;

    Rigidbody2D rb;
    Vector2 startPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        rb.gravityScale = 0f;
    }

    void Start()
    {
        LaunchRandom();
    }

    void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * speed;
    }

    void LaunchRandom()
    {
        transform.position = startPos;
        float dirX = Random.value < 0.5f ? -1f : 1f;
        float angle = Random.Range(minLaunchAngle, 90f - minLaunchAngle) * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(dirX * Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
        rb.velocity = dir * speed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.GetComponent<Movement>() != null)
        {
            float paddleY = col.transform.position.y;
            float contactY = rb.position.y;
            float halfHeight = col.collider.bounds.size.y * 0.5f;
            float offset = Mathf.Clamp((contactY - paddleY) / halfHeight, -1f, 1f);

            Vector2 dir = rb.velocity.normalized;
            dir.x = -dir.x;       // refleja en horizontal
            dir.y = offset;       // cambia el ángulo según dónde pegó
            dir = dir.normalized;

            speed += speedIncrementOnHit; // acelera un poco en cada golpe
            rb.velocity = dir * speed;
        }
    }

    public void ResetAndLaunch()
    {
        speed = Mathf.Max(speed, 8f);
        LaunchRandom();
    }
}