using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public Transform[] waypoints;   // Path içindeki noktalar
    public float speed = 3f;
    public float reachThreshold = 0.1f;  // Noktaya yaklaþýldý saymak için

    private int currentIndex = 0;

    void Start()
    {
        // Ýhtiyaten baþlangýçta ilk waypoint'e koy
        if (waypoints != null && waypoints.Length > 0)
        {
            transform.position = waypoints[0].position;
            currentIndex = 1; // sonraki noktaya git
        }
    }

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;
        if (currentIndex >= waypoints.Length) return; // sona geldi

        Vector3 targetPos = waypoints[currentIndex].position;
        Vector3 dir = (targetPos - transform.position).normalized;

        // hareket
        transform.position += dir * speed * Time.deltaTime;

        // hedefe yeterince yaklaþtý mý?
        if (Vector3.Distance(transform.position, targetPos) < reachThreshold)
        {
            currentIndex++;

            // Bitiþe ulaþtýysa burada base'e damage verip yok edebilirsin
            if (currentIndex >= waypoints.Length)
            {
                // TODO: Base health düþür
                Destroy(gameObject);
            }
        }
    }

    // Editörde yolu görmek için (opsiyonel)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (waypoints == null) return;
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            if (waypoints[i] != null && waypoints[i + 1] != null)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }
    }
}
