using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public float speed = 10f;
    public float speedIncrement = 0.5f;
    public float maxSpeed = 25f;
    [Range(5f, 44f)] public float minLaunchAngle = 20f;
    [Range(10f, 75f)] public float maxBounceAngle = 60f;

    Rigidbody2D rb;
    Vector2 startPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        rb.gravityScale = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void Start() => Launch();

void FixedUpdate()
{
    Vector2 vel = rb.velocity;

    // Si la velocidad es casi cero, aplica un impulso mínimo en una dirección aleatoria
    if (vel.sqrMagnitude < 0.01f)
    {
        float angle = Random.Range(minLaunchAngle, 90f - minLaunchAngle) * Mathf.Deg2Rad;
        float dirX = Random.value < 0.5f ? -1f : 1f;
        Vector2 nudge = new Vector2(dirX * Mathf.Cos(angle), Mathf.Sin(angle)).normalized * 0.5f;
        rb.velocity = nudge;
        return;
    }

    // Si la componente X es muy baja, aplica un pequeño empujón horizontal
    if (Mathf.Abs(vel.x) < 0.05f)
        vel.x = 0.2f * Mathf.Sign(vel.x == 0f ? Random.value < 0.5f ? 1f : -1f : vel.x);

    // Si la componente Y es muy baja, aplica un pequeño empujón vertical
    if (Mathf.Abs(vel.y) < 0.05f)
        vel.y = 0.2f * Mathf.Sign(vel.y == 0f ? Random.value < 0.5f ? 1f : -1f : vel.y);

    // Normaliza y aplica la velocidad deseada
    rb.velocity = vel.normalized * speed;
}

    void Launch()
    {
        transform.position = startPos;
        float angle = Random.Range(minLaunchAngle, 90f - minLaunchAngle) * Mathf.Deg2Rad;
        float dirX = Random.value < 0.5f ? -1f : 1f;
        Vector2 dir = new Vector2(dirX * Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
        rb.velocity = dir * speed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        var paddle = col.collider.GetComponent<Movement>() ?? col.collider.GetComponentInParent<Movement>();
        if (paddle == null) return;

        var contact = col.GetContact(0);
        float offset = Mathf.Clamp((contact.point.y - col.transform.position.y) / (col.collider.bounds.size.y * 0.5f), -1f, 1f);
        float angle = offset * maxBounceAngle * Mathf.Deg2Rad;
        bool left = col.transform.position.x < transform.position.x;
        Vector2 dir = new Vector2(left ? Mathf.Cos(angle) : -Mathf.Cos(angle), Mathf.Sin(angle)).normalized;

        speed = Mathf.Min(speed + speedIncrement, maxSpeed);
        rb.velocity = dir * speed;
    }

    public void ResetAndLaunch()
    {
        speed = Mathf.Max(speed, 8f);
        Launch();
    }
}