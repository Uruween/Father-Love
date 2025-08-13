using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] float life = 100f;
    public void Health(float heal)
    {
        life += heal;
        if (life >100)
        {
            life = 100;
        }
    }
    public void Damage (float damage)
    {
        life -= damage;
        if (life < 0)
        {
            life = 0;
        } 
    }
}
