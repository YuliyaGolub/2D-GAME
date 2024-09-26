using UnityEngine;
using UnityEngine.InputSystem;

public interface ICommand
{
    void Execute();
}

public interface ICommandReceiver
{
    void ReceiveCommand(ICommand command);
}

public class MoveCommand : ICommand
{
    private PlayerController playerController;
    public MoveCommand(PlayerController playerController)
    {
        this.playerController = playerController;
    }
    public void Execute()
    {
        float direction = playerController.ReadHorizontalInput();
        playerController.Move(direction);
        playerController.UpdateFacingDirection(direction);
    }
}

public class PickupItemCommand : ICommand
{
    private PlayerController playerController;
    private IItem item;
    public PickupItemCommand(PlayerController playerController, IItem item)
    {
        this.playerController = playerController;
        this.item = item;
    }
    public void Execute()
    {
        playerController.PickupItem(item);
    }
}


public class PlayerController : MonoBehaviour, ICommandReceiver
{
    [SerializeField] private float moveSpeed = 10;
    private ICommand moveCommand;
    private Player player;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        player = GetComponent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player.IsFacingRight = true;
        moveCommand = new MoveCommand(this);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameEvent == GameEvent.Running)
        {
            player.IsMoving = ReadHorizontalInput() != 0;
            bool isShooting = player.Weapon.IsShooting;

            if (player.IsMoving)
            {
                ReceiveCommand(moveCommand);
            }

            UpdateAnimation(player.IsMoving, isShooting);
        }
    }

    public void Move(float horizontalInput)
    {
        Vector2 movement = new Vector2(horizontalInput, 0);
        transform.position += new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);
    }

    public void UpdateFacingDirection(float horizontalInput)
    {
        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
            player.IsFacingRight = true;
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
            player.IsFacingRight = false;
        }
    }

    public float ReadHorizontalInput()
    {
        return Keyboard.current.dKey.ReadValue() - Keyboard.current.aKey.ReadValue();
    }

    public void PickupItem(IItem item)
    {
        Player.Instance.Inventory.AddItem(item);
    }

    public void ReceiveCommand(ICommand command)
    {
        command.Execute();
    }

    private void UpdateAnimation(bool isMoving, bool isShooting)
    {
        player.Animator.SetBool("Move", isMoving);
        if (player.AmmoCount > 0)
            player.Animator.SetBool("Shoot", isShooting);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Item item = collision.gameObject.GetComponent<Item>();
            if (item != null)
            {
                ReceiveCommand(new PickupItemCommand(this, item));
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.GameStateChange(GameEvent.EndGame);
        }
    }
}
