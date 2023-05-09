using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasLevel : MonoBehaviourUI
{
    [SerializeField] private int cantEnemies;
    [SerializeField] private int currentTemp;
    [SerializeField] private Text textEnemy;
    private string textEnemyPredet = "ENEMY: ";
    public override void Awake()
    {
        base.Awake();

    }
    private void Update()
    {
       
    }
    public override void ManagedUpdate()
    {
        base.ManagedUpdate();
        textEnemy.text = textEnemyPredet + cantEnemies;
    }

}
