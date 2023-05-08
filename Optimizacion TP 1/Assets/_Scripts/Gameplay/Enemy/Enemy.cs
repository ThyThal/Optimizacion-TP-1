using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : Character
{
    [SerializeField] float speed;
    [SerializeField] float shootFrequency;
    [SerializeField] GameObject bulletPrefab;
    private Rigidbody _rb;

    [SerializeField] private List<Renderer> _renderers;
    [SerializeField] private float _spawnTime = 0f;
    [SerializeField] private bool _enabled = false;

    [SerializeField] private List<EnemyRayCheck> _availableDirections;

    private Dictionary<EnemyRayCheck.EnemyRotateDirection, EnemyRayCheck> _directionDic = new Dictionary<EnemyRayCheck.EnemyRotateDirection, EnemyRayCheck>();

    public override void Awake()
    {
        base.Awake();

        var a = GetComponentsInChildren<EnemyRayCheck>();

        foreach (var item in a)
        {
            _directionDic.Add(item.RotateDirection, item);
        }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public override void ManagedUpdate()
    {
        if (_enabled)
        {
            Move();
        }

        else
        {
            if (_spawnTime >= 1)
            {
                // Create Normal Material.
                foreach (var renderer in _renderers)
                {
                    Color currentColor = renderer.material.color;
                    currentColor.a = 1f;
                    renderer.material.color = currentColor;
                }

                _enabled = true;
            }

            _spawnTime += Time.deltaTime;
        }
    }

    public void PreSpawn()
    {
        _enabled = false;
        _spawnTime = 0;
        Health.DoHeal(Health.MaxHealth);

        // Create Ghost Material.
        foreach (var renderer in _renderers)
        {
            Color currentColor = renderer.material.color;
            currentColor.a = 0.25f;
            renderer.material.color = currentColor;
        }
    }

    public void Move()
    {
        _rb.velocity = transform.forward * speed;
    }
    
    public void Shoot()
    {

    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (!IsAlive())
        {
            Die();
        }
    }

    public void Die()
    {
        GameManager.Instance.EnemySpawner.EnemyPool.Recycle(this.gameObject);
    }


    private void DoRotation()
    {
        _availableDirections = new List<EnemyRayCheck>();

        foreach (var enemyRay in _directionDic.Values)
        {
            if (!enemyRay.IsObstructed())
            {
                _availableDirections.Add(enemyRay);
            }
        }

        // Get Random Direction and Rotate
        var a = _availableDirections[Random.Range(0, _availableDirections.Count)];

        switch (a.RotateDirection)
        {
            case EnemyRayCheck.EnemyRotateDirection.Forward:
                transform.rotation = Quaternion.LookRotation(transform.forward);
                return;

            case EnemyRayCheck.EnemyRotateDirection.Back:
                transform.rotation = Quaternion.LookRotation(-transform.forward);
                return;

            case EnemyRayCheck.EnemyRotateDirection.Left:
                transform.rotation = Quaternion.LookRotation(-transform.right);
                return;

            case EnemyRayCheck.EnemyRotateDirection.Right:
                transform.rotation = Quaternion.LookRotation(transform.right);
                return;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall") && _directionDic.GetValueOrDefault(EnemyRayCheck.EnemyRotateDirection.Forward).IsObstructed())
        {
            DoRotation();
        }

        else if (collision.collider.CompareTag("Breakable") && _directionDic.GetValueOrDefault(EnemyRayCheck.EnemyRotateDirection.Forward).IsObstructed())
        {
            DoRotation();
        }

        else if(collision.collider.CompareTag("Enemy") && _directionDic.GetValueOrDefault(EnemyRayCheck.EnemyRotateDirection.Forward).IsObstructed())
        {
            DoRotation();
        }
    }
}
