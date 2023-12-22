using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] powerUpPrefabs;

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
            int randEnemy = Random.Range(0, powerUpPrefabs.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            nextSpawn = Time.time + spawnRate;
            Instantiate(powerUpPrefabs[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
        }
    }
}
