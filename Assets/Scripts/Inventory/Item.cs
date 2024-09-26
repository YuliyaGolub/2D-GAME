using UnityEngine;

public interface IItem
{
    public int Quantity { get; set; }
    void Collect();
}

public class Item: MonoBehaviour, IItem
{
    public int Quantity { get; set; }
    public virtual void Collect()
    {
        SoundsManager.Instance.PlayItemCollectSound();
        Destroy(this.gameObject);
    }
}
