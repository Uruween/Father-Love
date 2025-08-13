using UnityEngine;
using UnityEngine.TextCore.Text;

public class CureManager : MonoBehaviour
{
    private LifeController character;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            character.Health(10f);
        }

    }
}
