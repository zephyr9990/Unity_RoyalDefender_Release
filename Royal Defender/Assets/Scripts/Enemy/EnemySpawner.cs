using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemiesToSpawn;
    public float spawnTime = 20f;
    public Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);
    }

    private void SpawnEnemy()
    {
        int enemySpawnIndex = Random.Range(0, enemiesToSpawn.Length);
        int spawnPositionIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(
            enemiesToSpawn[enemySpawnIndex],
            spawnPoints[spawnPositionIndex].position,
            spawnPoints[spawnPositionIndex].rotation
            );
    }
}
