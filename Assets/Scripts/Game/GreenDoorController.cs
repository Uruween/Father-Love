using UnityEngine;

public class GreenDoorController : MonoBehaviour
{
    public Transform doorHinge;
    public float openAngle = 90f;
    public float openSpeed = 2f;

    [SerializeField] private int requiredId = 2; 

    private bool openDoor = false;
    private bool range = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    private Inventory inventory;

    void Start()
    {
        closedRotation = doorHinge.rotation;
        openRotation = Quaternion.Euler(doorHinge.eulerAngles + Vector3.up * openAngle);

        if (GameManager.Instance != null)
        {
            inventory = GameManager.Instance.inventory;

            if (inventory != null)
            {
                inventory.ItemUsed += OnItemUsed;
            }
        }
    }

    void OnDestroy()
    {
        if (inventory != null)
        {
            inventory.ItemUsed -= OnItemUsed;
        }
    }

    void Update()
    {
        if (range && Input.GetKeyDown(KeyCode.F) && inventory.Items.ContainsKey(requiredId))
        {
            ToggleDoor();
        }

        doorHinge.rotation = Quaternion.Slerp(
            doorHinge.rotation,
            openDoor ? openRotation : closedRotation,
            Time.deltaTime * openSpeed
        );
    }

    private void OnItemUsed(int itemId)
    {
        if (!openDoor && range && itemId == requiredId)
        {
            ToggleDoor();
        }
    }

    private void ToggleDoor()
    {
        openDoor = !openDoor;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            range = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            range = false;
        }
    }
}
