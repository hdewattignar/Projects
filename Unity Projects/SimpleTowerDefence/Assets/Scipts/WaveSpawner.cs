using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public Transform enemyPreFab;
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    private int waveNum = 1;
    private int waveSize = 1; // number of enemies per wave

    public Text waveCountDownText;

    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountDownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveSize; i++)
        {
                       
            SpawnEnemy();

            yield return new WaitForSeconds(0.5f);

        }

        waveNum++;
        PlayerStats.Waves++;

        if (waveNum % 5 == 0)
        {
            waveSize++;
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPreFab, spawnPoint.position, spawnPoint.rotation);
    }
}
