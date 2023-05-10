using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CanvasLevel : MonoBehaviourUI
{
    [SerializeField] private int _killedEnemies = 0;
    [SerializeField] private int _aliveEnemies = 0;
    [SerializeField] private int _remainingEnemies = 0;

    [SerializeField] private TMP_Text _textKilled;
    [SerializeField] private TMP_Text _textRemaining;
    [SerializeField] private TMP_Text _textAlive;
    [SerializeField] private TMP_Text _textTimer;
    [SerializeField] private Popup _popup;

    public Popup Popup => _popup;

    private void Start()
    {
        _remainingEnemies = GameManager.Instance.LevelManager.EnemySpawner.MaxEnemies;
    }


    public override void ManagedUpdate()
    {
        _textKilled.text = $"Killed: {_killedEnemies}";
        _textAlive.text = $"Alive: {_aliveEnemies}";
        _textRemaining.text = $"Remaining: {_remainingEnemies}";
        _textTimer.text = $"Time: {GameManager.Instance.LevelManager.GetFormattedTime()}";
    }

    public void UpdateKilled(int killed)
    {
        _killedEnemies = killed;
        _aliveEnemies--;
    }

    public void SpawnedEnemy()
    {
        _aliveEnemies++;
        _remainingEnemies--;
    }
}

