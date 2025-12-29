using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;

    private ProgressBar _healthBar;
    private int _currentHealth;

    public event Action Died;

    void Awake()
    {
        var healthBarPrefab = Resources.Load<ProgressBar>("HealthBar");
        _healthBar = Instantiate(healthBarPrefab, transform);
        _healthBar.SetProgress(1f);
        _currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _healthBar.SetProgress((float)_currentHealth / maxHealth);
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Died?.Invoke();
        Destroy(gameObject);
    }
}
