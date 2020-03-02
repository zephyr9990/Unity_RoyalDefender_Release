using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public float spawnTime = 20f;
    public Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length - 1);

        Instantiate(enemyToSpawn, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
    }
}
