using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;

    float randX;
    public float spawnRate = 2f;
    float nextSpawn = 20f;

    void Start()
    {

    }
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            int randEnemy = Random.Range(0, enemyPrefabs.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            nextSpawn = Time.time + spawnRate;
            Instantiate(enemyPrefabs[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
        }
    }
}
