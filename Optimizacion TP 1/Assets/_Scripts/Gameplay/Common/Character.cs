using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviourGameplay, IDamagable
{
    [SerializeField] private CharacterType _character;
    [SerializeField] private Health _health;

    public Health Health => _health;

    public bool IsAlive()
    {
        if (Health.CurrentHealth > 0)
        {
            return true;
        }

        return false;
    }

    public CharacterType GetCharacterType => _character;
    public enum CharacterType
    {
        Player,
        Enemy
    }

    public virtual void TakeDamage(int damage)
    {
        _health.DoDamage(damage);
    }
}


