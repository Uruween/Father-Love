using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyBasic : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] Transform targetAgent;
    [SerializeField] Animator animator;
    [SerializeField] float timeBefore;
    [SerializeField] float time;
    [SerializeField] float actionDistance;
    [SerializeField] float currDistance;
    [SerializeField] float attackDistance;
    [SerializeField] float live; 
    private Rigidbody rb;



    public bool onAwake;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator.SetBool("Hit", false);
        if (onAwake)
        {
            agent.SetDestination(targetAgent.position);
        }
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!onAwake)
        {
            float _currDistance = Vector3.Distance(transform.position, targetAgent.position);
            currDistance = _currDistance;

            if (currDistance <= actionDistance)
            {
                agent.SetDestination(targetAgent.position);
                agent.isStopped = false;
                animator.SetBool("Hit", false);
                if (currDistance <= attackDistance)
                {
                    agent.isStopped = true;
                    animator.SetBool("Hit",true);
                }
            }
        }
    }

    public void LifeEnemyBasic(float damage)
    {
        live -= damage;
        Debug.Log(damage + " " + live);
        if (live < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
