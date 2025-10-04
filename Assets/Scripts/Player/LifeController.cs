using System.Collections;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private float maxLife = 100f;
    [SerializeField] private float life = 100f;
    [SerializeField] private GameObject cm;
    [SerializeField] private Animator animator;
    [SerializeField] private float deathAnimationDuration = 2.8f;

    private bool isDead = false;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (cm != null)
        {
            cm.SetActive(false);
        }

        life = maxLife;
    }

    public void Health(float heal)
    {
        if (isDead) return;

        life += heal;
        life = Mathf.Clamp(life, 0, maxLife);

        Debug.Log($"Curación: +{heal}. Vida actual: {life}/{maxLife}");
    }

    public void Damage(float damage)
    {
        if (isDead) return;

        life -= damage;
        Debug.Log($"Daño recibido: -{damage}. Vida actual: {life}/{maxLife}");

        if (life <= 0)
        {
            life = 0;
            StartCoroutine(Dead());
        }
    }

    public float GetHealth()
    {
        return life;
    }

    public float GetHealthPercentage()
    {
        return life / maxLife;
    }

    public bool IsDead()
    {
        return isDead;
    }

    private IEnumerator Dead()
    {
        isDead = true;


        if (animator != null)
        {
            animator.SetTrigger("Die");

            animator.SetBool("IsWalk", false);
            animator.SetBool("IsAttack", false);
        }

        DisableMovement();

        yield return new WaitForSeconds(deathAnimationDuration);

        Debug.Log("Secuencia de muerte completada");

        gameObject.SetActive(false);

        if (cm != null)
        {
            cm.SetActive(true);
        }
    }

    private void DisableMovement()
    {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            agent.enabled = false;
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        PlayerController playerController = GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        enemyBasic enemy = GetComponent<enemyBasic>();
        if (enemy != null)
        {
            enemy.enabled = false;
        }
    }
}