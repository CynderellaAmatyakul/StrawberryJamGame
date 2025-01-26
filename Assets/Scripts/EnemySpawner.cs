using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.20f;
    [SerializeField] private float enemiesPerSecCap = 10f;
    [SerializeField] private List<GameObject> wave1;
    [SerializeField] private List<GameObject> wave2;
    [SerializeField] private List<GameObject> wave3;
    [SerializeField] private List<GameObject> wave4;
    [SerializeField] private List<GameObject> wave5;
    [SerializeField] private List<GameObject> wave6;
    [SerializeField] private List<GameObject> wave7;
    [SerializeField] private List<GameObject> wave8;
    [SerializeField] private List<GameObject> wave9;
    [SerializeField] private List<GameObject> wave10;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    [SerializeField] private TextMeshProUGUI waveCountText;
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

    private void OnGUI()
    {
        waveCountText.text = "Wave " + currentWave.ToString();
    }

    private void SpawnEnemy(int wavesCount)
    {
        if (wavesCount == 1 && wave1.Count > 0)
        {
            Instantiate(wave1[0], LevelManager.instance.startPoint.position, Quaternion.identity);
            wave1.RemoveAt(0);
        }
        else if (wavesCount == 2 && wave2.Count > 0)
        {
            Instantiate(wave2[0], LevelManager.instance.startPoint.position, Quaternion.identity);
            wave2.RemoveAt(0);
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
        else if (wavesCount == 6 && wave6.Count > 0)
        {
            Instantiate(wave6[0], LevelManager.instance.startPoint.position, Quaternion.identity);
            wave6.RemoveAt(0);
        }
        else if (wavesCount == 7 && wave7.Count > 0)
        {
            Instantiate(wave7[0], LevelManager.instance.startPoint.position, Quaternion.identity);
            wave7.RemoveAt(0);
        }
        else if (wavesCount == 8 && wave8.Count > 0)
        {
            Instantiate(wave8[0], LevelManager.instance.startPoint.position, Quaternion.identity);
            wave8.RemoveAt(0);
        }
        else if (wavesCount == 9 && wave9.Count > 0)
        {
            Instantiate(wave9[0], LevelManager.instance.startPoint.position, Quaternion.identity);
            wave9.RemoveAt(0);
        }
        else if (wavesCount == 10 && wave10.Count > 0)
        {
            Instantiate(wave10[0], LevelManager.instance.startPoint.position, Quaternion.identity);
            wave10.RemoveAt(0);
        }
    }

    private int EnemiesPerWave()
    {
        switch (currentWave)
        {
            case 1: return wave1.Count;
            case 2: return wave2.Count;
            case 3: return wave3.Count;
            case 4: return wave4.Count;
            case 5: return wave5.Count;
            case 6: return wave6.Count;
            case 7: return wave7.Count;
            case 8: return wave8.Count;
            case 9: return wave9.Count;
            case 10: return wave10.Count;
            default: return 0;
        }
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecCap);
    }
}
