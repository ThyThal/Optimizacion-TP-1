using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasLevel : MonoBehaviourUI
{
    [SerializeField] private int cantEnemies;
    [SerializeField] private Text textEnemy;
    [SerializeField] private Enemy enemy;
    private string textEnemyPredet = "ENEMY: ";
    public override void Awake()
    {
        base.Awake();
        textEnemy.text = textEnemyPredet + cantEnemies;
    }
    public override void ManagedUpdate()
    {
        base.ManagedUpdate();
        ChangeText();
    }

    private void ChangeText()
    {
        if (enemy.IsAlive())
        {
            cantEnemies--;
            textEnemy.text = textEnemyPredet + cantEnemies;
        }

    }
}
