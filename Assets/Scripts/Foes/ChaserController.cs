using UnityEngine;
using UnityEngine.AI;

public class ChaserController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] PlayerController targetAgent;
    [SerializeField] float live = 100f;
    [SerializeField] float attackDistance;
    public float counter = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        live = 100f;
        targetAgent = FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        float currDistance = Vector3.Distance(transform.position, targetAgent.transform.position);
        agent.SetDestination(targetAgent.transform.position);
        if (currDistance < attackDistance)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
       
        
        if (live <= 0)
        {
            agent.isStopped = true;
            live = 0;
            counter += Time.deltaTime;
        }
        if (counter >= 5f)
        {
            agent.isStopped = false;
            agent.SetDestination(targetAgent.transform.position);
            live = 100f;
            counter = 0f;
        }

    }
    public void LifeChaser(float damage)
    {
        live -= damage;
        Debug.Log(damage + " " + live);
    }
}
