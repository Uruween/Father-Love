using UnityEngine;

public class KnifeDamage : MonoBehaviour
{

   
    [SerializeField] Animator animator;

    [SerializeField] float damage = 10f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("Buttom", true);
            
        }
        else
        {
            animator.SetBool("Buttom", false);
            
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyBasic enemy = other.GetComponent<enemyBasic>();
            enemy.LifeEnemy(damage);
        }
    }
}
