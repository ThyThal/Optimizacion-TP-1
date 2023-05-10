using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : Character
{
    [SerializeField] float speed;
    [SerializeField] float shootFrequency;
    private Rigidbody _rb;

    [SerializeField] private List<Renderer> _renderers;
    [SerializeField] private float _spawnTime = 0f;
    [SerializeField] private bool _enabled = false;

    [SerializeField] private List<EnemyRayCheck> _availableDirections = new List<EnemyRayCheck>();
    [SerializeField] Transform frontCheck;
    [SerializeField] LayerMask obstaclesMask;
    [SerializeField] RaycastHit[] obstacles = new RaycastHit[3];

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
        if (GameManager.Instance.GameFinished) return;

        if (_enabled)
        {
            Move();

            int currentObstacles = Physics.RaycastNonAlloc(transform.position, transform.forward, obstacles, 0.5f, obstaclesMask);
            if (currentObstacles > 0)
            {
                DoRotation();
            }
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
                Invoke("Shoot", Random.Range(1f, 3f));
                DoRotation();
            }

            _spawnTime += Time.deltaTime;
        }
    }
    

    public void PreSpawn()
    {
        //_rb.velocity = Vector3.zero;
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
        GameManager.Instance.SpawnBullet(this);
        Invoke("Shoot", Random.Range(1f, 3f));
    }

    public override void TakeDamage(int damage)
    {
        if (!_enabled) return;
        base.TakeDamage(damage);

        if (!IsAlive())
        {
            Die();
        }
    }

    public void Die()
    {
        CancelInvoke();
        GameManager.Instance.EnemySpawner.EnemyPool.Recycle(this.gameObject);
        GameManager.Instance.KilledEnemy();
        GameManager.Instance.EnemySpawner.SpawnEnemy();
        _rb.velocity = Vector3.zero;
    }


    private void DoRotation()
    {
        foreach (var enemyRay in _directionDic.Values)
        {
            if (!enemyRay.IsObstructed() && enemyRay.RotateDirection != EnemyRayCheck.EnemyRotateDirection.Forward)
            {
                _availableDirections.Add(enemyRay);
            }
        }

        // Get Random Direction and Rotate
        var collisionDirection = _availableDirections[Random.Range(0, _availableDirections.Count)];

        switch (collisionDirection.RotateDirection)
        {
            case EnemyRayCheck.EnemyRotateDirection.Forward:
                transform.rotation = Quaternion.LookRotation(transform.forward);
                Debug.Log("frente");
                break;

            case EnemyRayCheck.EnemyRotateDirection.Back:
                transform.rotation = Quaternion.LookRotation(-transform.forward);
                break;

            case EnemyRayCheck.EnemyRotateDirection.Left:
                transform.rotation = Quaternion.LookRotation(-transform.right);
                break;

            case EnemyRayCheck.EnemyRotateDirection.Right:
                transform.rotation = Quaternion.LookRotation(transform.right);
                break;
        }

        _availableDirections.Clear();
    }
    /*private void OnCollisionEnter(Collision collision)
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
    }*/
}
