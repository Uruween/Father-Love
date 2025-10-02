using UnityEngine;
using UnityEngine.AI;

public class ChaserController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] PlayerController targetAgent;

    [SerializeField] float live = 100f;
    [SerializeField] float attackDistance = 2f;
    [SerializeField] float detectionRange = 20f; 

    [SerializeField] float minDistanceToTarget = 1f; 
    private bool isPatrolling = true;
    private float counter = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        targetAgent = FindAnyObjectByType<PlayerController>();
        live = 100f;

        GoToRandomPoint();
    }

    void Update()
    {
        float currDistance = Vector3.Distance(transform.position, targetAgent.transform.position);

        if (live <= 0)
        {
            agent.isStopped = true;
            counter += Time.deltaTime;

            if (counter >= 5f)
            {
                live = 100f;
                counter = 0f;
                isPatrolling = true;
                GoToRandomPoint();
            }
            return;
        }

        if (currDistance < detectionRange)
        {
            isPatrolling = false;
            agent.SetDestination(targetAgent.transform.position);

            if (currDistance < attackDistance)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }
        }
        else
        {
            if (isPatrolling)
            {
                agent.isStopped = false;

                if (!agent.pathPending && agent.remainingDistance < minDistanceToTarget)
                {
                    GoToRandomPoint();
                }
            }
            else
            {
                isPatrolling = true;
                GoToRandomPoint();
            }
        }
    }

    private void GoToRandomPoint()
    {
        Vector3 randomPoint;
        if (RandomPointOnNavMesh(out randomPoint))
        {
            agent.SetDestination(randomPoint);
        }
    }

    private bool RandomPointOnNavMesh(out Vector3 result)
    {
        for (int i = 0; i < 30; i++) 
        {
            Vector3 randomPos = new Vector3(
                Random.Range(-200f, 200f), 
                0f,
                Random.Range(-200f, 200f)
            );

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPos, out hit, 50f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    public void LifeChaser(float damage)
    {
        live -= damage;
        Debug.Log("Recibió daño: " + damage + " | Vida restante: " + live);
    }
}
