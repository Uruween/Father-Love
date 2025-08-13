using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class movTo : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] Transform targetAgent;
    [SerializeField] Animator animator;
    [SerializeField] float timeBefore;
    [SerializeField] float time;
    [SerializeField] float actionDistance;
    [SerializeField] float speed;
    [SerializeField] float currDistance;

    public bool onAwake;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (onAwake)
        {
            agent.SetDestination(targetAgent.position);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!onAwake)
        {
            float _currDistance = Vector3.Distance(transform.position, targetAgent.position);
            currDistance = _currDistance;

            if (currDistance <= actionDistance)
            {
                agent.SetDestination(targetAgent.position);

            }




        }

    }
}
