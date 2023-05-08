using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramScript : MonoBehaviour
{
    public bool isEnemyInTheHologram;

    private void OnTriggerEnter(Collider other)
    {
        if (!isEnemyInTheHologram) 
        {
            isEnemyInTheHologram = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isEnemyInTheHologram)
        {
            isEnemyInTheHologram = false;
        }
    }
}
