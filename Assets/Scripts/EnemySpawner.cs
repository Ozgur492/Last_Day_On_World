using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] pathWaypoints;

    public float startInterval = 3f;
    public float minInterval = 0.8f;
    public float difficultyRate = 0.95f;

    private float currentInterval;
    private float timer;

    void Start()
    {
        currentInterval = startInterval;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= currentInterval)
        {
            SpawnEnemy();
            timer = 0f;

            // Zamanla hýzlanýr
            currentInterval = Mathf.Max(minInterval, currentInterval * difficultyRate);
        }
    }

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
