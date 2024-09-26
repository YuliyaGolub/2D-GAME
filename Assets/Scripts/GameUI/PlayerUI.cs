using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bulletCountText;

    void Start()
    {
        Player.Instance.AmmoCountChanged += AmmoCountChanged;
        AmmoCountChanged(Player.Instance.AmmoCount);
    }

    private void AmmoCountChanged(int newAmmoCount)
    {
        bulletCountText.text = newAmmoCount.ToString();
    }
}
