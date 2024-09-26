using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarUI : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3 (0, 2, 0);
    private Slider slider;
    private Enemy enemy;

    private IDisposable healthSubscription;
    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
        slider = GetComponent<Slider>();
        slider.maxValue = enemy.Health.MaxValue;
        slider.value = enemy.Health.Value;

        // create subscribtion to the enemy's health changes
        healthSubscription = enemy.Health
            .Where(h => h > 0) // update when health above 0
            .Subscribe(UpdateHealthBar);

        SetPosition();

        transform.localScale = Vector3.one;
    }

    private void LateUpdate() => SetPosition();

    private void OnDestroy()
    {
        healthSubscription?.Dispose();
    }

    private void UpdateHealthBar(float health)
    {
        slider.value = health;
    }

    private void SetPosition()
    {
        Vector3 position = Camera.main.WorldToScreenPoint(enemy.transform.position + offset);
        slider.transform.position = position;
    }
}
