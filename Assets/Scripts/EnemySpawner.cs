using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] pathWaypoints;

    [Header("Timed Spawning")]
    public bool useTimedSpawn = false;
    public float startInterval = 3f;
    public float minInterval = 0.8f;
    public float difficultyRate = 0.95f;

    [Header("Manual Wave Spawning")]
    // useTimedSpawn kapalıysa bunu kullan
    public int number_of_enemy = 1;
    public bool autoSpawnOnStart = true;   // Oyunun başında otomatik dalga istiyorsan


    [Header("Wave UI")]
    public GameObject waveUIPanel;
    public TextMeshProUGUI waveCompletedText;
    public Button startWaveButton;

    private float currentInterval;
    private float timer;


    [Header("Wave Settings")]
    public float spawnDelayBetweenEnemies = 0.3f; // Aynı dalgadaki düşmanlar arası süre
    public int first_wave_number_of_enemy = 10;
    public int second_wave_number_of_enemy = 20;
    public int boss_wave_number_of_enemy = 30;

    private int currentWave = 0;
    private bool isSpawning = false;

    void Awake()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError($"[{name}] Awake aşamasında enemyPrefab NULL!", this);
        }
        else
        {
            Debug.Log($"[{name}] Awake -> enemyPrefab: {enemyPrefab.name}", this);
        }
    }
    void Start()
    {
        currentInterval = startInterval;

     /*
      //burası bi dursun 
        // Eğer zamanlı spawn kapalıysa ve otomatik dalga açıksa
        if (!useTimedSpawn && autoSpawnOnStart)
        {
            SpawnWave();   // number_of_enemy kadar zombi spawn eder
        }

    */
}

void Update()
    {

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartNextWave();
        }


        // Zamanlı spawn kapalıysa burası hiç çalışmaz, bu normal.
        if (!useTimedSpawn) return;

        timer += Time.deltaTime;

        if (timer >= currentInterval)
        {
            SpawnEnemy();
            timer = 0f;

            // Hızlanma
            currentInterval = Mathf.Max(minInterval, currentInterval * difficultyRate);
        }
    }



    public void StartNextWave()
    {
        waveUIPanel.SetActive(false); //yazı panel kapanıcak

        if (isSpawning)
        {
            Debug.Log("Zaten bir wave spawn ediliyor.");
            return;
        }

        currentWave++;

        switch (currentWave)
        {
            case 1:
               
                StartCoroutine(SpawnWaveRoutine(first_wave_number_of_enemy));
                break;

            case 2:
               
                StartCoroutine(SpawnWaveRoutine(second_wave_number_of_enemy));
                break;

            case 3:
               
                StartCoroutine(SpawnWaveRoutine(boss_wave_number_of_enemy));
                break;

            default:
                Debug.Log("Tüm wave'ler bitti!");
                break;
        }
    }









    // ============================================================
    // İstediğin anda belirli sayıda düşman göndermek için
    // (UI butonuna da bağlanabilir)
    // ============================================================
    
    /*
    public void SpawnWave()
    {
        StartCoroutine(SpawnWaveRoutine());
    }
    */
    private IEnumerator SpawnWaveRoutine(int count)
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab atanmadı!");
            yield break;
        }

        if (pathWaypoints == null || pathWaypoints.Length == 0)
        {
            Debug.LogError("Path waypoints atanmadı!");
            yield break;
        }

        isSpawning = true;

        for (int i = 0; i < count; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelayBetweenEnemies);
        }

        isSpawning = false;

        yield return new WaitForSeconds(5.0f);

        // Wave bitti → UI göster
        waveCompletedText.text = $"Wave {currentWave} Completed!";
        waveUIPanel.SetActive(true);

    }
    // ============================================================
    // Tek düşman spawn kodu
    // ============================================================
    void SpawnEnemy()
    {
        if (enemyPrefab == null || pathWaypoints.Length == 0) return;

        Vector3 spawnPos = pathWaypoints[0].position;
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        EnemyMover mover = enemy.GetComponent<EnemyMover>();
        if (mover != null)
        {
            mover.waypoints = pathWaypoints;
        }
    }
}