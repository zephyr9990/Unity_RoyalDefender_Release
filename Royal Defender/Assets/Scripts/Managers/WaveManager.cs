using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public int spawnTime;
    public int numOfWavesBeforeSpawnRateIncreased = 5;
    public Text waveText;
    public Text enemiesRemainingText;
    public EnemyInfo[] enemyRound;
    public Transform[] spawnPoints;

    private static WaveManager instance;
    private int currentWave;
    private int numOfEnemiesToSpawn;
    private int enemiesInWave;
    private int currentEnemies;
    private ArrayList enemiesToSpawn;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        currentWave = 1;
        numOfEnemiesToSpawn = 1;
        enemiesInWave = 0;
        currentEnemies = 0;
    }

    private void Start()
    {
        enemiesToSpawn = GetEnemiesSpecificToRound();
        InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);
        UpdateWaveInfoUIText();

    }

    public ArrayList GetEnemiesSpecificToRound()
    {
        // Allows enemy types to appear based on index and round
        ArrayList currentWaveEnemies = new ArrayList();
        for (int i = 0; i < currentWave && i < enemyRound.Length; i++)
        {
            enemyRound[i].ResetWaveAmount(currentWave);
            currentWaveEnemies.Add(enemyRound[i]);
            enemiesInWave += enemyRound[i].GetAmount();
        }

        currentEnemies = enemiesInWave;

        return currentWaveEnemies;
    }

    private void SpawnEnemy()
    {
        Debug.Log("Spawning " + numOfEnemiesToSpawn + " at a time.");
        for (int i = 0; i < numOfEnemiesToSpawn; i++)
        {
            if (enemiesToSpawn.Count <= 0)
            {
                return; // Don't spawn if there's nothing to spawn.
            }

            int enemyToSpawn = Random.Range(0, enemiesToSpawn.Count);
            if (enemyToSpawn < enemiesToSpawn.Count)
            {
                EnemyInfo enemyRoundInfo = (EnemyInfo)enemiesToSpawn[enemyToSpawn];

                int spawnIndex = Random.Range(0, spawnPoints.Length);
                if (spawnIndex < spawnPoints.Length)
                {
                    GameObject spawnedEnemy = Instantiate(enemyRoundInfo.enemy,
                        spawnPoints[spawnIndex].position,
                        spawnPoints[spawnIndex].rotation
                        );
                    spawnedEnemy.GetComponent<IHealthWithWaveManager>().SetWaveManager(this);
                    DecreaseCount(enemyRoundInfo);
                }
            }
        }
    }

    private void AdjustNumOfEnemiesToSpawn()
    {
        if (currentWave == numOfWavesBeforeSpawnRateIncreased)
        {
            numOfWavesBeforeSpawnRateIncreased += numOfWavesBeforeSpawnRateIncreased;
            numOfEnemiesToSpawn++;
        }
    }

    private void DecreaseCount(EnemyInfo enemyRoundInfo)
    {
        enemyRoundInfo.DecrementAmount();
        if (enemyRoundInfo.GetAmount() <= 0)
        {
            enemiesToSpawn.Remove(enemyRoundInfo);
        }
    }

    private void StartNextWave()
    {
        currentWave++;
        enemiesInWave = 0;
        enemiesToSpawn = GetEnemiesSpecificToRound();
        AdjustNumOfEnemiesToSpawn();
        UpdateWaveInfoUIText();
    }

    private void UpdateWaveInfoUIText()
    {
        waveText.text = "Wave " + currentWave;
        UpdateEnemiesRemainingUI();
    }

    private void UpdateEnemiesRemainingUI()
    {
        enemiesRemainingText.text = "Enemies\n" + currentEnemies + "/" + enemiesInWave;
    }

    public void DecreaseEnemyCount()
    {
        currentEnemies--;
        UpdateEnemiesRemainingUI();
        if (currentEnemies <= 0)
        {
            StartNextWave();
        }
    }
}
