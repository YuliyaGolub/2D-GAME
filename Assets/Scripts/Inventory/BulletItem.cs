using TMPro;
using UnityEngine;

public class BulletItem : Item
{
    [SerializeField] TextMeshProUGUI textQuantity;
    [SerializeField] Vector3 offset = new Vector3(0, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        Quantity = Random.Range(1, 10);
        textQuantity.text = Quantity.ToString();
        SetPosition();
    }

    private void LateUpdate() => SetPosition();

    private void SetPosition()
    {
        Vector3 position = Camera.main.WorldToScreenPoint(transform.position + offset);
        textQuantity.transform.position = position;
    }
}
