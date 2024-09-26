using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed  = 10.0f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifetime = 2f; // in seconds

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Vector2 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        direction = GetDirection();
        rb.velocity = direction * speed;

        if (direction == Vector2.left)
            sprite.flipX = true;

        Destroy(gameObject, lifetime); // or coroutine?
    }

    private Vector2 GetDirection()
    {
        return Player.Instance.IsFacingRight ? Vector2.right : Vector2.left;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
                enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Walls"))
        {
            Destroy(gameObject);
        }
    }
}
