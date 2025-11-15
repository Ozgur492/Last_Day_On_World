using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] pathWaypoints;  // Path içindeki tüm WP'ler
    public float spawnInterval = 2f;   // kaç saniyede bir zombi gelecek

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null || pathWaypoints.Length == 0) return;

        // Baþlangýç noktasý: ilk waypoint
        Vector3 spawnPos = pathWaypoints[0].position;
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        // EnemyMover'a waypointleri ver
        EnemyMover mover = enemy.GetComponent<EnemyMover>();
        if (mover != null)
        {
            mover.waypoints = pathWaypoints;
        }
    }
}