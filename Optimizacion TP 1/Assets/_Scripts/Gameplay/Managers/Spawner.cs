using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private bool _enabled = true;
    [SerializeField] private EnemySpawner _enemySpawner;

    public bool IsEnabled()
    {
        return _enabled;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _enabled = false;
            _enemySpawner.DisableSpawner(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _enabled = true;
            _enemySpawner.EnableSpawner(this);
        }
    }
}
