using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Pool _bulletsPool;
    [SerializeField] private CanvasLevel _canvasLevel;

    private int _playerDeaths = 0;
    private int _killedEnemies = 0;
    private int _aliveEnemies = 0;
    private bool _finished = false;
    private float gameTime = 0f;

    public int PlayerDeaths => _playerDeaths;
    public int AliveEnemies => _aliveEnemies;
    public bool GameFinished => _finished;
    public EnemySpawner EnemySpawner => _enemySpawner;
    public Pool BulletPool => _bulletsPool;


    public void Awake()
    {
        GameManager.Instance.SetLevelManager(this);
    }

    private void Update()
    {
        if (GameFinished) return;
        gameTime += Time.deltaTime;
    }

    /// <summary>
    /// Sp
    /// </summary>
    public void OnSpawnedEnemy()
    {
        _canvasLevel.SpawnedEnemy();
        _aliveEnemies++;
    }

    public void SpawnBullet(Character owner)
    {
        var instance = _bulletsPool.GetFromPool();
        instance.SetActive(true);
        instance.GetComponent<Bullet>().GenerateBullet(owner);
    }

    /// <summary>
    /// Adds Player Death.
    /// </summary>
    public void PlayerDeath()
    {
        _playerDeaths++;
    }

    /// <summary>
    /// Shows Level Complete Popup.
    /// </summary>
    public void LevelComplete()
    {
        _finished = true;
        _canvasLevel.Popup.ShowPopup();
    }

    /// <summary>
    /// Keeps Track of Killed Enemies and Other Stats.
    /// </summary>
    public void KilledEnemy()
    {
        _killedEnemies++;
        _aliveEnemies--;
        _canvasLevel.UpdateKilled(_killedEnemies);

        if (_killedEnemies >= _enemySpawner.MaxEnemies)
        {
            LevelComplete();
        }
    }

    /// <summary>
    /// Formts In-Game Time.
    /// </summary>
    /// <returns></returns>
    public string GetFormattedTime()
    {
        int hours = Mathf.FloorToInt(gameTime / 3600);
        int minutes = Mathf.FloorToInt((gameTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);

        string formattedTime = $"{hours}h {minutes}m {seconds}s";
        return formattedTime;
    }
   

    public void CheatKill()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (var item in enemies)
        {
            item.OnDie();
        }
    }

}
