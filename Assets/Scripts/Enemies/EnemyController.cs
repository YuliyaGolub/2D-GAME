using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    private SpriteRenderer spriteRenderer;

    private Enemy enemy;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameEvent == GameEvent.Running)
        {
            float direction = ReadHorizontalInput();
            Move(direction);
            UpdateFacingDirection(direction);
        }
    }

    public virtual void Move(float horizontalInput)
    {
        Vector2 movement = new Vector2(horizontalInput, 0);
        transform.position += new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);
    }

    public virtual void UpdateFacingDirection(float horizontalInput)
    {
        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    public virtual float ReadHorizontalInput()
    {
        return (Player.Instance.transform.position - transform.position).normalized.x;
    }
}
