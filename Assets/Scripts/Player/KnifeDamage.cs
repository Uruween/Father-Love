using System.Collections;
using UnityEngine;

public class KnifeDamage : MonoBehaviour
{
    [SerializeField] Animator animator;
    private PlayerController playerController;
    [SerializeField] float damage = 10f;
    private new BoxCollider collider;
    private void Start()
    {
        collider = GetComponent<BoxCollider>();
        animator.GetComponent<Animator>();
        playerController = FindAnyObjectByType<PlayerController>();
        collider.enabled = false;
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
        collider.enabled = true;
        animator.SetBool("Punch", true);

        yield return new WaitForSeconds(0.8f); 

        animator.SetBool("Punch", false);
        playerController.isAttacking = false;
        collider.enabled = false;
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
