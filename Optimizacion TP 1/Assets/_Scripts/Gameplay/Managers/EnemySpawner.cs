using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviourGameplay
{
    [SerializeField] private List<Spawner> _availableSpawnLocations;
    [SerializeField] private Pool _enemiesPool;

    public Pool EnemyPool => _enemiesPool;

    public override void Awake()
    {
        _enemiesPool.InitializePool(10);
    }

    // Logic for Spawning and Enabling Enemy.
    [ContextMenu("Spawn Enemy from Pool")]
    private void SpawnEnemy()
    {
        var instance = _enemiesPool.GetFromPool();

        instance.transform.position = _availableSpawnLocations[Random.Range(0, _availableSpawnLocations.Count)].transform.position;
        instance.SetActive(true);
        instance.GetComponent<Enemy>().PreSpawn();
    }

    private void Start()
    {
        for (int i = 0; i < 4; i++)
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
