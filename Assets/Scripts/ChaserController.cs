using UnityEngine;
using UnityEngine.AI;

public class Chaser : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] Transform targetAgent;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(targetAgent.position);
    }
}
