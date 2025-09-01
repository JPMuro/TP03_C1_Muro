using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [Header("Velocidad")]
    public float speed = 10f;             // velocidad objetivo
    public float speedIncrementOnHit = 0.5f; // opcional: acelera un poco en cada golpe

    [Header("Rango de ángulo inicial")]
    [Range(10f, 70f)] public float minLaunchAngle = 20f; // evita ángulos muy horizontales

    Rigidbody2D rb;
    Vector2 startPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        rb.gravityScale = 0f; // sin gravedad
    }

    void Start()
    {
        LaunchRandom();
    }

    void FixedUpdate()
    {
        // Mantener la velocidad constante
        rb.velocity = rb.velocity.normalized * speed;
    }

    void LaunchRandom()
    {
        transform.position = startPos;

        // Dirección horizontal aleatoria
        float dirX = Random.value < 0.5f ? -1f : 1f;
        float angle = Random.Range(minLaunchAngle, 90f - minLaunchAngle) * Mathf.Deg2Rad;

        Vector2 dir = new Vector2(dirX * Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
        rb.velocity = dir * speed;
    }

    // Lógica de rebote con paletas para controlar el ángulo
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Paddle"))
        {
            // punto de contacto y centro de la paleta
            float paddleY = col.transform.position.y;
            float contactY = rb.position.y;

            float halfHeight = col.collider.bounds.size.y * 0.5f;
            float offset = Mathf.Clamp((contactY - paddleY) / halfHeight, -1f, 1f);

            // nueva dirección: reflejar en X y sumar componente vertical según offset
            Vector2 dir = rb.velocity.normalized;
            dir.x = -dir.x;                // refleja en horizontal
            dir.y = offset;                // controla ángulo por punto de impacto
            dir = dir.normalized;

            // aumenta levemente la velocidad si querés juegos más intensos
            speed += speedIncrementOnHit;

            rb.velocity = dir * speed;
        }
    }

    public void ResetAndLaunch()
    {
        speed = Mathf.Max(speed, 8f); // asegúrate de no caer demasiado si cambias cosas
        LaunchRandom();
    }
}