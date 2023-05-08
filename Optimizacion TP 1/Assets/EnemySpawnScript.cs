using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{

    [SerializeField]private GameObject enemyPrefab;
    private List<GameObject> enemytPool = new List<GameObject>();
    float timer;
    float enemyCreateTimer = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > enemyCreateTimer) 
        {
            timer = 0;
            CreateEnemy();
        }
    }

    void CreateEnemy()
    {
        
        GameObject enemy = GetPooledObject();

        if (enemy != null)
        {
            enemy.transform.position = transform.position;
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
}
