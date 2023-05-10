using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : Character
{
    [SerializeField] private List<Renderer> _renderers;
    [SerializeField] Transform frontCheck;
    [SerializeField] LayerMask obstaclesMask;
    [SerializeField] RaycastHit[] obstacles = new RaycastHit[3];
    [SerializeField] private EnemyRayCheck[] _enemyRays;

    [Header("Cooldowns, Timers and Stats")]
    [SerializeField] private float _speed = 3;
    [SerializeField] private bool _preSpawning = true;
    [SerializeField] private float _spawnTimer = 0.5f;
    [SerializeField] private float _originalSpawnTime = 0.5f;
    [SerializeField] private float _shootTimer = 1f;
    [SerializeField] private float _originalShootTime = 1f;

    private Rigidbody _rb;
    private List<EnemyRayCheck> _availableDirections = new List<EnemyRayCheck>();
    private Dictionary<EnemyRayCheck.EnemyRotateDirection, EnemyRayCheck> _directionDic = new Dictionary<EnemyRayCheck.EnemyRotateDirection, EnemyRayCheck>();

    public override void Awake()
    {
        base.Awake();

        if (_enemyRays == null)
        {
            _enemyRays = GetComponentsInChildren<EnemyRayCheck>();
        }

        foreach (var item in _enemyRays)
        {
            _directionDic.Add(item.RotateDirection, item);
        }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _spawnTimer = _originalSpawnTime;
        _shootTimer = _originalShootTime;

        OnPreSpawn();
    }




    public override void ManagedUpdate()
    {
        if (GameManager.Instance.LevelManager.GameFinished) return;

        // Enemy Spawning.
        if (_preSpawning)
        {
            if (_spawnTimer > 0)
            {
                _spawnTimer -= Time.deltaTime;
            }

            else
            {
                // Do Final Spawn.
                OnSpawn();
            }
        }

        // Enemy Spawned.
        else
        {
            // Checks for Obstacles in Front.
            CheckForObstacles();

            // Moves with Velocity.
            OnMove();

            // Handles Shooting.
            HandleShoot();
        }


    }

    private void DoRotation()
    {
        if (!IsAlive()) return;

        // Find Available Rotations.
        foreach (var enemyRay in _directionDic.Values)
        {
            if (!enemyRay.IsObstructed() && enemyRay.RotateDirection != EnemyRayCheck.EnemyRotateDirection.Forward)
            {
                _availableDirections.Add(enemyRay);
            }
        }

        if (_availableDirections.Count > 0)
        {
            // Get Random Direction and Rotate
            var a = Random.Range(0, _availableDirections.Count);
            var b = _availableDirections[a];
            var collisionDirection = b;

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
        }

        // Clear List.
        _availableDirections.Clear();
    }

    /// <summary>
    /// Before Spawning Logic
    /// </summary>
    public void OnPreSpawn()
    {
        // Sets Enemy State.
        _preSpawning = true;
        _spawnTimer = _originalSpawnTime;

        // Creates Ghost Material
        SetMaterialsAlpha(0.25f);

        // Initializes Health.
        Health.DoHeal(Health.MaxHealth);

        // MUST BE IMPROVED.
        foreach (var item in _enemyRays)
        {
            item.CheckStartCollisions();
        }

        DoRotation();
    }

    /// <summary>
    /// Spawn Logic.
    /// </summary>
    private void OnSpawn()
    {
        // Sets Enemy State.
        _preSpawning = false;

        // Creates Ghost Material
        SetMaterialsAlpha(1f);
    }

    /// <summary>
    /// Handle Physics Movement.
    /// </summary>
    public void OnMove()
    {
        _rb.velocity = transform.forward * _speed;
    }

    /// <summary>
    /// Make the Enemy Take Damage.
    /// </summary>
    /// <param name="damage">Damage Amount.</param>
    public override void TakeDamage(int damage)
    {
        if (_preSpawning) return;

        base.TakeDamage(damage);

        if (!IsAlive())
        {
            OnDie();
        }
    }

    /// <summary>
    /// Enemy Death.
    /// </summary>
    public void OnDie()
    {
        if (_preSpawning) return;

        // Freezes Velocity.
        _rb.velocity = Vector3.zero;

        // MUST BE IMPROVED.
        foreach (var ay in _enemyRays)
        {
            ay.Reset();
        }

        // Manager Calls.
        GameManager.Instance.LevelManager.EnemySpawner.EnemyPool.Recycle(this.gameObject);
        GameManager.Instance.LevelManager.KilledEnemy();
        GameManager.Instance.LevelManager.EnemySpawner.SpawnEnemy();
    }

    /// <summary>
    /// Handles the Shooting.
    /// </summary>
    private void HandleShoot()
    {
        if (_shootTimer > 0)
        {
            _shootTimer -= Time.deltaTime;
        }

        else
        {
            _shootTimer = _originalShootTime;
            GameManager.Instance.LevelManager.SpawnBullet(this);
        }
    }

    /// <summary>
    /// Checks for Obstacles in Front.
    /// </summary>
    private void CheckForObstacles()
    {
        int currentObstacles = Physics.RaycastNonAlloc(transform.position, transform.forward, obstacles, 0.5f, obstaclesMask);
        if (currentObstacles > 0)
        {
            DoRotation();
        }
    }

    /// <summary>
    /// Changes Material Alphas
    /// </summary>
    /// <param name="alpha">Alpha Value for Materials</param>
    private void SetMaterialsAlpha(float alpha)
    {
        foreach (var renderer in _renderers)
        {
            Color currentColor = renderer.material.color;
            currentColor.a = alpha;
            renderer.material.color = currentColor;
        }
    }
}
