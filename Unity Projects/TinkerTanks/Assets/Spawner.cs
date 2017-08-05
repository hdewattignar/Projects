using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Transform playerSpawn;
    public Transform[] EnemySpawn;
    
    public GameObject[] enemys;
    public GameObject player;

    public int maxEnemys = 4;
    public float timeBetweenWaves = 10;
    public float timeBewwenSpawns = 2;

    float spawnTimer;
    float waveTimer; 
    int currentEnemyCount = 0;

    void Start()
    {
        spawnTimer = timeBewwenSpawns;
        waveTimer = timeBetweenWaves;

        SpawnPlayer();        
    }
	// Update is called once per frame
	void Update () {

        if (spawnTimer < timeBewwenSpawns)
            spawnTimer += Time.deltaTime;
        if (waveTimer < timeBetweenWaves)
            waveTimer += timeBetweenWaves;

        if (spawnTimer >= timeBewwenSpawns && currentEnemyCount < maxEnemys)
        {
            SpawnEnemy();
            spawnTimer = 0;
        }

	}

    void SpawnPlayer()
    {
        GameObject newPlayer = (GameObject)Instantiate(player, playerSpawn.position, playerSpawn.rotation);
        GetComponent<Manager>().SetPlayer(newPlayer);
    }

    void SpawnEnemy()
    {
        int rngPatrolNode = Random.Range(0, EnemySpawn.Length);

        GameObject newEnemy = (GameObject)Instantiate(enemys[0], EnemySpawn[rngPatrolNode].position, EnemySpawn[rngPatrolNode].rotation);

        currentEnemyCount++;
    }

    public void EnemyDied()
    {
        currentEnemyCount--;
    }
}
