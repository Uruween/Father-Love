using UnityEngine;

public class DamangeManager : MonoBehaviour 
{
    [SerializeField] LifeController character;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            character.Damage(10f);
        }
    }
}
