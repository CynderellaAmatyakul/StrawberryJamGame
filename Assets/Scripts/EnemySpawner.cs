using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 5;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.20f;
    [SerializeField] private float enemiesPerSecCap = 10f;
    [SerializeField] private List<GameObject> wave3;
    [SerializeField] private List<GameObject> wave4;
    [SerializeField] private List<GameObject> wave5;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    [SerializeField] private int currentWave = 1;
    [SerializeField] private float timeSinceLastSpawn;
    [SerializeField] private int enemiesAlive;
    [SerializeField] private int enemiesLeftToSpawn;
    [SerializeField] private float eps;
    [SerializeField] private bool isSpawning= false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        //Debug.Log(currentWave);

        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy(currentWave);
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private void SpawnEnemy(int wavesCount)
    {
        if (wavesCount <= 2)
        {
            GameObject prefabToSpawn = enemyPrefabs[0];
            Instantiate(prefabToSpawn, LevelManager.instance.startPoint.position, Quaternion.identity);
        }
        else if (wavesCount == 3 && wave3.Count > 0)
        {
            Instantiate(wave3[0], LevelManager.instance.startPoint.position, Quaternion.identity);
            wave3.RemoveAt(0);
        }
        else if (wavesCount == 4 && wave4.Count > 0)
        {
            Instantiate(wave4[0], LevelManager.instance.startPoint.position, Quaternion.identity);
            wave4.RemoveAt(0);
        }
        else if (wavesCount == 5 && wave5.Count > 0)
        {
            Instantiate(wave5[0], LevelManager.instance.startPoint.position, Quaternion.identity);
            wave5.RemoveAt(0);
        }
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecCap);
    }
}
