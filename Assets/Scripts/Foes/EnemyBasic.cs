using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class enemyBasic : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform targetAgent;
    [SerializeField] private Animator animator;

    [SerializeField] private float actionDistance = 10f;
    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private float currDistance;

    [SerializeField] private float attackAnimationDuration = 1.5f; 
    [SerializeField] private float attackCooldown = 2f; 
    private bool isAttacking = false;
    private float lastAttackTime = 0f;

    [SerializeField] private float live = 100f;

    public bool onAwake = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        animator.SetBool("IsAttack", false);
        animator.SetBool("IsWalk", false);

        if (onAwake)
        {
            agent.SetDestination(targetAgent.position);
            animator.SetBool("IsWalk", true);
        }
    }

    private void Update()
    {
        if (isAttacking) return;

        currDistance = Vector3.Distance(transform.position, targetAgent.position);

        if (onAwake)
        {
            HandleOnAwakeMode();
        }
        else
        {
            HandleNormalMode();
        }
    }

    private void HandleNormalMode()
    {
        if (currDistance <= actionDistance)
        {
            if (currDistance <= attackDistance)
            {
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    StartCoroutine(AttackSequence());
                }
                
                    agent.isStopped = true;
                    animator.SetBool("IsWalk", false);
                agent.velocity = Vector3.zero;

            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(targetAgent.position);
                animator.SetBool("IsWalk", true);
            }
        }
        else
        {
            agent.isStopped = false;
            animator.SetBool("IsWalk", false);

        }
    }

    private void HandleOnAwakeMode()
    {
        if (currDistance <= attackDistance)
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                StartCoroutine(AttackSequence());
            }
            else
            {
                agent.isStopped = true;
                animator.SetBool("IsWalk", false);
                agent.velocity = Vector3.zero;
            }
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(targetAgent.position);
            animator.SetBool("IsWalk", true);
        }
    }

    private IEnumerator AttackSequence()
    {
        isAttacking = true;

        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        animator.SetBool("IsWalk", false);

        Vector3 directionToTarget = (targetAgent.position - transform.position).normalized;
        directionToTarget.y = 0;  
        if (directionToTarget != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }

        animator.SetBool("IsAttack", true);

        yield return new WaitForSeconds(attackAnimationDuration);

        animator.SetBool("IsAttack", false);

        lastAttackTime = Time.time;

        isAttacking = false;

        Debug.Log("Ataque completado");
    }

    public void LifeEnemyBasic(float damage)
    {
        live -= damage;
        Debug.Log($"Enemigo recibió {damage} de daño. Vida restante: {live}");

        if (live <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        StopAllCoroutines();

        agent.isStopped = true;
        agent.enabled = false;

        animator.SetBool("IsWalk", false);
        animator.SetBool("IsAttack", false);

        animator.SetTrigger("Dead");

        Debug.Log("Enemigo eliminado");

        StartCoroutine(DisableAfterDelay(2.7f));
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}