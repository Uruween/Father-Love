using System.Collections;
using UnityEngine;

public class KnifeDamage : MonoBehaviour
{
    [SerializeField] Animator animator;
    private PlayerController playerController;
    [SerializeField] float damage = 10f;

    private void Start()
    {
        animator.GetComponent<Animator>();
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack());
        }
    }
    private IEnumerator Attack()
    {
        playerController.isAttacking = true;
        animator.SetBool("Punch", true);

        yield return new WaitForSeconds(0.8f); 

        animator.SetBool("Punch", false);
        playerController.isAttacking = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyBasic enemy = other.GetComponent<enemyBasic>();
            enemy.LifeEnemyBasic(damage);
        }
        if (other.CompareTag("Chaser"))
        {
            ChaserController chaser = other.GetComponent<ChaserController>();
            chaser.LifeChaser(damage);
        }
    }
}
