using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 3f;
    public float turnSpeed = 5f;
    public float reachThreshold = 0.1f;

    private Animator anim;
    private int currentIndex = 0;

    void Start()
    {
        anim = GetComponent<Animator>();

        if (anim != null)
        {
            anim.SetFloat("SPEED", 0.5f);   // 0.5 = yürüyüþ, koþu eþiðinin altýnda
        }


        if (waypoints != null && waypoints.Length > 0)
        {
            transform.position = waypoints[0].position;
            currentIndex = 1;
        }
    }

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;
        if (currentIndex >= waypoints.Length) return;

        Vector3 targetPos = waypoints[currentIndex].position;
        Vector3 dir = (targetPos - transform.position);
        Vector3 dirNorm = dir.normalized;

        // ?? SPEED parametresi animatöre gider
        anim.SetFloat("SPEED", dir.magnitude);

        // ?? ROTASYON — zombi hedefe doðru döner
        if (dirNorm != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(dirNorm);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
        }

        // ?? ÝLERLEME
        transform.position += dirNorm * speed * Time.deltaTime;

        // ?? Noktaya ulaþtý mý?
        if (Vector3.Distance(transform.position, targetPos) < reachThreshold)
        {
            currentIndex++;

            if (currentIndex >= waypoints.Length)
            {
                Destroy(gameObject);
            }
        }
    }
}
