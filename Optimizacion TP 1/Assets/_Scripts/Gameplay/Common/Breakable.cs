using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviourGameplay, IDamagable
{
    [SerializeField] private Health _health;

    private void Start()
    {
        _health.DoHeal(_health.MaxHealth);
    }

    public void TakeDamage(int damage)
    {
        _health.DoDamage(damage);

        if (_health.CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
