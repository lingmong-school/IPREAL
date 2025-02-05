
using UnityEngine;
using UnityEngine.AI;

public class KoiFishAI : MonoBehaviour
{
    public float wanderRadius = 5f;
    public float wanderTimer = 5f;
    public float detectionRadius = 3f;

    private NavMeshAgent agent;
    private float timer;
    private Transform targetFood;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    void Update()
    {
        if (targetFood == null)
        {
            DetectFood();
        }

        if (targetFood != null)
        {
            agent.SetDestination(targetFood.position);
        }
        else
        {
            Wander();
        }
    }

    void Wander()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = wanderTimer;
        }
    }

    void DetectFood()
    {
        GameObject[] foodObjects = GameObject.FindGameObjectsWithTag("Food");
        if (foodObjects.Length > 0)
        {
            Transform closestFood = null;
            float closestDistance = Mathf.Infinity;

            foreach (GameObject food in foodObjects)
            {
                float distance = Vector3.Distance(transform.position, food.transform.position);
                if (distance < detectionRadius && distance < closestDistance)
                {
                    closestDistance = distance;
                    closestFood = food.transform;
                }
            }

            if (closestFood != null)
            {
                targetFood = closestFood;
                Debug.Log("Detected food at: " + targetFood.position);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            targetFood = null;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector2 randomPoint = Random.insideUnitCircle * dist;
        Vector3 finalPosition = new Vector3(origin.x + randomPoint.x, origin.y, origin.z + randomPoint.y);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(finalPosition, out hit, dist, layermask))
        {
            return hit.position;
        }

        return origin;
    }
}
