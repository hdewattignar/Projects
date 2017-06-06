using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public static int EnemysAlive = 0;
    public Wave[] waves;
    public Transform spawnPoint;

    public float timeBetweenWaves = 3f;
    private float countdown = 2f;
    private int waveIndex = 0;
    private int waveSize = 1; // number of enemies per wave

    public Text waveCountDownText;

    public GameManager gameManager;

    void Update()
    {
        if (EnemysAlive > 0)
        {
            return;
        }

        if (waveIndex == waves.Length)
        {
            Debug.Log("Level Won");
            gameManager.WinLevel();
            this.enabled = false;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountDownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Waves++;

        Wave wave = waves[waveIndex];

        EnemysAlive = wave.count;
        
        for (int i = 0; i < wave.count; i++)
        {
                       
            SpawnEnemy(wave.enemy);

            yield return new WaitForSeconds(1f / wave.rate);

        }        

        if (waveIndex % 5 == 0)
        {
            waveSize++;
        }

        waveIndex++;

        
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        
    }
}
