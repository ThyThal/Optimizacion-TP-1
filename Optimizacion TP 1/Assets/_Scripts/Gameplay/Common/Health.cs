using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Health
{
    [SerializeField] private int _maxHealth = 100;
    private int _currentHealth = 0;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;

    public void DoHeal(int heal)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + heal, 0, _maxHealth);
    }

    public void DoDamage(int damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);

        if (_currentHealth <= 0)
        {
            DoDeath();
        }
    }

    private void DoDeath()
    {

    }
}
