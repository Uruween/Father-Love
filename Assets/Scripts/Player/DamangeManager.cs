using UnityEngine;

public class DamangeManager : MonoBehaviour 
{
    
    private void OnTriggerEnter(Collider other)
    {
        LifeController character = other.GetComponent<LifeController>();
        if (other.CompareTag("Player"))
        {
            character.Damage(10f);
        }
    }
}
