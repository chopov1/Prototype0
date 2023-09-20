using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    Queue<GameObject> pool;
    [SerializeField]
    GameObject player, enemyPrefab;
    [SerializeField]
    int maxEnemys;
    [SerializeField]
    float spawnRate;
    float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        setupPool();
        spawnEnemy();
    }

    void setupPool()
    {
        pool = new Queue<GameObject>();
        for(int i = 0; i < maxEnemys; i++)
        {
            GameObject go = Instantiate(enemyPrefab);
            if(go.TryGetComponent<Enemy>(out Enemy enemyScript))
            {
                enemyScript.SetPlayer(player);
            }
            go.SetActive(false);
            pool.Enqueue(go);
        }
    }

    void spawnEnemy()
    {
        if (pool.Peek().activeSelf)
        {
            return;
        }
        GameObject go = pool.Dequeue();
        go.transform.position = getRandomSpawnPos();
        if (go.TryGetComponent<Enemy>(out Enemy enemyScript))
        {
            enemyScript.ResetEnemy();
        }
        go.SetActive(true);
        pool.Enqueue(go);
    }

    Vector3 getRandomSpawnPos()
    {
        return new Vector3(Random.Range(-28, 28), 2, Random.Range(-35, 35));
    }

 
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if( currentTime >= spawnRate)
        {
            spawnEnemy();
            spawnRate--;
            currentTime = 0;
        }
    }
}
