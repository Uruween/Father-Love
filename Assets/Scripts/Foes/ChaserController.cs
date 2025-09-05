using UnityEngine;
using UnityEngine.AI;

public class ChaserController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] Transform targetAgent;
    [SerializeField] float live;
    [SerializeField] float attackDistance;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float currDistance = Vector3.Distance(transform.position, targetAgent.position);
        agent.SetDestination(targetAgent.position);
        if (currDistance < attackDistance)
        {
            agent.isStopped = true;
        }
    }
    public void LifeChaser(float damage)
    {
        live -= damage;
        Debug.Log(damage + " " + live);
        if (live < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
