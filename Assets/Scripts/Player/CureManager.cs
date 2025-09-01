using UnityEngine;
using UnityEngine.TextCore.Text;

public class CureManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        LifeController character = other.GetComponent<LifeController>();
        if (other.CompareTag("Player"))
        {
            character.Health(10f);
            Destroy(gameObject);
        }
    }
}
