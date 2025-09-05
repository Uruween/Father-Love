using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private float life = 100f;
    [SerializeField] GameObject cm;
    private void Start()
    {
        cm.SetActive(false);
    }

    public void Health(float heal)
    {
        life += heal;
        if (life > 100)
        {
            life = 100;
        }
    }

    public void Damage(float damage)
    {
        life -= damage;
        if (life < 0)
        {
            life = 0;
            gameObject.SetActive(false);
            cm.SetActive(true);
        }
    }
    public float GetHealth()
    {
        return life;
    }
}
