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



    public bool onAwake;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator.SetBool("IsAttack", false);
        animator.SetBool("IsWalk", false);
        if (onAwake)
        {
            agent.SetDestination(targetAgent.position);
        }
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
                animator.SetBool("IsWalk", true);
                if (currDistance <= attackDistance)
                {
                    agent.isStopped = true;
                    animator.SetBool("IsWalk", false);
                    animator.SetBool("IsAttack",true);
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
