using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourGameplay
{
    // Game Manager Instance
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private Pool bulletSpawner;

    public EnemySpawner EnemySpawner => enemySpawner;
    public Pool BulletPool => bulletSpawner;

    public void SpawnBullet(Character owner)
    {
        var instance = bulletSpawner.GetFromPool();
        instance.GetComponent<Bullet>().GenerateBullet(owner.GetCharacterType);
        instance.transform.position = owner.transform.position;
        instance.transform.rotation = owner.transform.rotation;
    }
}
