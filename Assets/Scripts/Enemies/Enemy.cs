
using UnityEngine;
using UniRx;

public class FloatValue : ReactiveProperty<float>
{
    public float MaxValue { get; set; }
    public FloatValue(float initialValue)
    {
        Value = initialValue;
        MaxValue = initialValue;
    }
}

public abstract class Enemy : MonoBehaviour
{
    public abstract FloatValue Health { get; set; }

    public abstract void TakeDamage(int damage);
    public abstract void Die();
    public abstract void DropItem();
}
