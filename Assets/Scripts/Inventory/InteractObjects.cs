using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int itemID;
    private bool isPlayerInRange = false;
    [SerializeField] Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator = other.GetComponent<Animator>();
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
        PickObject();
    }
    private void PickObject ()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (GameManager.Instance != null && GameManager.Instance.inventory != null)
            {
                animator.SetTrigger("PickItem");
                GameManager.Instance.inventory.AddItem(itemID);
                Debug.Log("Objeto recogido con ID: " + itemID);
            }
            gameObject.SetActive(false);
        }
    }
}
