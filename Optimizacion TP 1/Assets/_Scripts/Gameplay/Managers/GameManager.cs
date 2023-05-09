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
    [SerializeField] private CanvasLevel _canvasLevel;

    private int _playerDeaths = 0;
    private int _killedEnemies = 0;
    private bool _finished = false;
    private float gameTime = 0f;

    public EnemySpawner EnemySpawner => enemySpawner;
    public Pool BulletPool => bulletSpawner;
    public CanvasLevel CanvasLevel => _canvasLevel;
    public int PlayerDeaths => _playerDeaths;

    public bool GameFinished => _finished;

    private void Update()
    {
        if (GameFinished) return;
        gameTime += Time.deltaTime;
    }

    public void SpawnBullet(Character owner)
    {
        var instance = bulletSpawner.GetFromPool();
        instance.GetComponent<Bullet>().GenerateBullet(owner);
    }

    public void FinishGame()
    {
        CanvasLevel.Popup.ShowPopup();
    }

    public void KilledEnemy()
    {
        _killedEnemies++;
        _canvasLevel.UpdateKilled(_killedEnemies);

        if (_killedEnemies >= EnemySpawner.MaxEnemies)
        {
            _finished = true;
            FinishGame();
        }
    }

    public void SpawnedEnemy()
    {

    }

    public void PlayerDeath()
    {
        _playerDeaths++;
    }

    

    public string GetFormattedTime()
    {
        int hours = Mathf.FloorToInt(gameTime / 3600);
        int minutes = Mathf.FloorToInt((gameTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);

        string formattedTime = $"{hours}h {minutes}m {seconds}s";
        return formattedTime;
    }
}
