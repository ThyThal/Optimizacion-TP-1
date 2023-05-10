using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviourGameplay
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private Character.CharacterType _owner;
    [SerializeField] private Character.CharacterType _target;
    [SerializeField] private Type _type;
    [SerializeField] private Rigidbody bulletBody;

    [SerializeField] private Material _enemyMaterial;
    [SerializeField] private Material _playerMaterial;
    [SerializeField] private Material _explosiveMaterial;

    [SerializeField] private Renderer _renderer;

    public int GetDamage => _damage;

    public enum Type
    {
        Normal,
        Explosive
    }

    public void GenerateBullet(Character owner)
    {
        _type = Type.Normal;
        _owner = owner.GetCharacterType;

        switch (_owner)
        {
            case Character.CharacterType.Player:
                _target = Character.CharacterType.Enemy;
                _renderer.material = _playerMaterial;

                if (Random.Range(1, 11) <= 1)
                {
                    _type = Type.Explosive;
                    _renderer.material = _explosiveMaterial;
                }

                break;

            case Character.CharacterType.Enemy:
                _target = Character.CharacterType.Player;
                _renderer.material = _enemyMaterial;
                break;
        }

        transform.position = owner.transform.position;
        transform.rotation = owner.transform.rotation;

        bulletBody.AddForce(transform.forward * _speed, ForceMode.Impulse);
    }

    // Create Sphere and Damage all Enemies
    private void DoExplosion()
    {
        // List of Damagables in Area.
        List<IDamagable> damagables = new List<IDamagable>();

        // Do Damage to Each Damagable.
        foreach (var damagable in damagables)
        {
            damagable.TakeDamage(100);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (_owner != _target)
        {
            if (other.CompareTag("Player") && _target == Character.CharacterType.Player)
            {
                other?.GetComponent<Player>().TakeDamage(100);
                GameManager.Instance.LevelManager.BulletPool.Recycle(this.gameObject);
                bulletBody.velocity = Vector3.zero;
            }

            else if (other.CompareTag("Enemy") && _target == Character.CharacterType.Enemy)
            {
                switch (_type)
                {
                    case Type.Normal:
                        other.GetComponent<Enemy>().TakeDamage(100);
                        break;

                    case Type.Explosive:
                        DoExplosion();
                        break;
                }

                GameManager.Instance.LevelManager.BulletPool.Recycle(this.gameObject);
                bulletBody.velocity = Vector3.zero;
            }
        }

        if (other.CompareTag("Wall"))
        {
            GameManager.Instance.LevelManager.BulletPool.Recycle(this.gameObject);
            bulletBody.velocity = Vector3.zero;
        }

        if (other.CompareTag("Breakable"))
        {
            other.GetComponent<Breakable>().TakeDamage(100);
            GameManager.Instance.LevelManager.BulletPool.Recycle(this.gameObject);
            bulletBody.velocity = Vector3.zero;
        }

    }
}
