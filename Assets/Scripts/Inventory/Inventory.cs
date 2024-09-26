using System.Collections.Generic;

public class Inventory 
{
    private Dictionary<IItem, int> items = new Dictionary<IItem, int>();

    public void AddItem(IItem item)
    {
        if (item is BulletItem bullet && item.Quantity > 0)
        {
            Player.Instance.AmmoCount += item.Quantity;
            SoundsManager.Instance.PlayItemCollectSound();
            bullet.Collect();
        }
        else
        {
            if (items.ContainsKey(item))
            {
                items[item] += item.Quantity;
            }
            else
            { 
                items.Add(item, item.Quantity);
            }
            item.Collect();
        }
    }

    public void RemoveItem(IItem item)
    {
        if (items.ContainsKey(item))
        {
            items[item]--;
            if (items[item] <= 0)
                items.Remove(item);
        }
    }
}
