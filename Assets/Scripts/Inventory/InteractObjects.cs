using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int itemID; // ID del item según tu base de datos
    private bool isPlayerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (GameManager.Instance != null && GameManager.Instance.inventory != null)
            {
                GameManager.Instance.inventory.AddItem(itemID); // Inventario desde el Canvas
                Debug.Log("Objeto recogido con ID: " + itemID);
                Destroy(gameObject); // Destruye el objeto recogido
            }
        }
    }
}
