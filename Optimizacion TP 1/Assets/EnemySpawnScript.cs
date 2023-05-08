using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{

    [SerializeField]private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyHologram;
    
    private List<GameObject> enemytPool = new List<GameObject>();
    [SerializeField]private List<Transform> spawnPositons = new List<Transform>();
    int indexSpawnPosition = 0;
    float timer;
    float enemyCreateTimer = 3f;
    float enemyDelaySpawn = 2f;
    public int totalEnemys = 0;

    

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > enemyCreateTimer) 
        {
            timer = 0;
            
            //activa el holograma para avisar al jugador de donde sale el proximo enemigo
            ActiveHologram();
            Invoke("SpawnEnemy", enemyDelaySpawn);
            
           
        }
    }

    void SpawnEnemy() 
    {
        totalEnemys++;
        enemyHologram.SetActive(false);
        GetEnemy();
        TranformSelection();
    }

    void GetEnemy()
    {
        //toma un gameobject ya sea desactivado o creado
        GameObject enemy = GetPooledObject();

        if (enemy != null)
        {
            //establece la posicion inicial y lo activa
            enemy.transform.position = spawnPositons[indexSpawnPosition].position;
            enemy.SetActive(true);

        }
    }

    public GameObject GetPooledObject()
    {
        //primero se fija si hay objeto desactivado en la lista. En caso que lo haya usa ese.
        for (int i = 0; i < enemytPool.Count; i++)
        {
            if (!enemytPool[i].activeInHierarchy)
            {

                return enemytPool[i];

            }
        }
        
        //En caso que no haya crea uno nuevo y se empieza a usar a partir de ahora
        GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        enemy.SetActive(false);
        enemytPool.Add(enemy);

        return enemy;

    }

    void ActiveHologram() 
    {
        
        enemyHologram.SetActive(true);
        enemyHologram.transform.position = spawnPositons[indexSpawnPosition].position;
    }

    void TranformSelection() 
    {
        indexSpawnPosition = Random.Range(0, spawnPositons.Count - 1);
        if (indexSpawnPosition >= spawnPositons.Count) 
        {
            indexSpawnPosition = 0;
        }
    }
}
