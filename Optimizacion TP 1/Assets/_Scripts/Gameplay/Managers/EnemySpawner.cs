using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviourGameplay
{
    [SerializeField] private int _spawnAmount = 4;
    [SerializeField] private float _spawnFrequency = 2;
    [SerializeField] private List<Spawner> _availableSpawnLocations;

    [SerializeField] private Pool _enemiesPool;
    [SerializeField] private int _maxEnemies;
    [SerializeField] private int _maxAliveEnemies = 25;
    [SerializeField] private int _spawnedEnemies;

    [SerializeField] float _timer = 0f; 

    public int MaxEnemies => _maxEnemies;
    public int SpawnedEnemies => _spawnedEnemies;

    public Pool EnemyPool => _enemiesPool;

    public override void Awake()
    {
        base.Awake();
        _enemiesPool.InitializePool(10);
    }

    public override void ManagedUpdate()
    {
        if (_spawnedEnemies >= MaxEnemies) return;

        if (_timer < _spawnFrequency)
        {
            _timer += Time.deltaTime;
        }
        else if(GameManager.Instance.AliveEnemies < _maxAliveEnemies) 
        {
            _timer = 0;
            SpawnEnemy();
        }
    }

    // Logic for Spawning and Enabling Enemy.
    [ContextMenu("Spawn Enemy from Pool")]
    public void SpawnEnemy()
    {
        if (_spawnedEnemies >= MaxEnemies) return;

        var instance = _enemiesPool.GetFromPool();

        instance.transform.position = _availableSpawnLocations[Random.Range(0, _availableSpawnLocations.Count)].transform.position;
        instance.SetActive(true);
        instance.GetComponent<Enemy>().OnPreSpawn();

        GameManager.Instance.SpawnedEnemy();
        _spawnedEnemies++;
    }

    private void Start()
    {
        for (int i = 0; i < _spawnAmount; i++)
        {
            SpawnEnemy();
        }
    }


    public void DisableSpawner(Spawner spawner)
    {
        _availableSpawnLocations.Remove(spawner);
    }

    public void EnableSpawner(Spawner spawner)
    {
        _availableSpawnLocations.Add(spawner);
    }
}
