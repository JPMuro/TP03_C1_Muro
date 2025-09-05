using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [Header("Velocidad")]
    public float initialSpeed = 8f;
    public float speedIncrement = 0.2f;
    public float maxSpeed = 15f;

    [Header("Ángulo inicial")]
    [Range(0.3f, 0.7f)] public float minYDir = 0.3f;

    private Rigidbody2D rb;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = initialSpeed;
        Launch();
    }

    void Launch()
    {
        float x = Random.value < 0.5f ? -1f : 1f;
        float y = Random.Range(minYDir, 1f) * (Random.value < 0.5f ? -1f : 1f);
        Vector2 dir = new Vector2(x, y).normalized;
        rb.velocity = dir * currentSpeed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // Calcula el offset vertical relativo a la paleta
            float paddleY = col.transform.position.y;
            float contactY = transform.position.y;
            float halfHeight = col.collider.bounds.size.y / 2f;
            float offset = (contactY - paddleY) / halfHeight;

            // Cambia la dirección según el offset
            Vector2 dir = new Vector2(-rb.velocity.x, offset).normalized;

            // Incrementa velocidad pero respeta el límite
            currentSpeed = Mathf.Min(currentSpeed + speedIncrement, maxSpeed);
            rb.velocity = dir * currentSpeed;
        }
        else
        {
            // Rebote normal para paredes
            rb.velocity = rb.velocity.normalized * currentSpeed;
        }
    }

    // Opcional: reiniciar la pelota
    public void ResetBall()
    {
        currentSpeed = initialSpeed;
        transform.position = Vector2.zero;
        Launch();
    }
}