using UnityEngine;

public class ZombyEnemy : Enemy
{
    [SerializeField] protected float healthMax = 3;
    [SerializeField] protected Item[] drop;
    private EnemyHealthBarUI healthBarUI;

    public override FloatValue Health { get; set; }

    private void Start()
    {
        Health = new FloatValue(healthMax);
        healthBarUI = GetComponentInChildren<EnemyHealthBarUI>();
        if (healthBarUI != null)
        {
            healthBarUI.Init(this);
        }
    }

    public override void TakeDamage(int damage)
    {
        Health.Value -= damage;
        if (Health.Value <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        SoundsManager.Instance.PlayKillEnemySound();
        DropItem();
        Destroy(gameObject);
    }

    public override void DropItem()
    {
        foreach (var item in drop)
        {
            GameObject droppedItem = Instantiate(item.gameObject, transform.position, Quaternion.identity);
        }
    }
}
