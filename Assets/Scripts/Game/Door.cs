using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform doorHinge;
    public float openAngle = 90f;
    public float openSpeed = 2f;


    private bool openDoor = false;
    private bool range = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;


    void Start()
    {
        closedRotation = doorHinge.rotation;
        openRotation = Quaternion.Euler(doorHinge.eulerAngles + Vector3.up * openAngle);

        
    }

    

    void Update()
    {
        if (range && Input.GetKeyDown(KeyCode.F))
        {
            ToggleDoor();
        }

        doorHinge.rotation = Quaternion.Slerp(
            doorHinge.rotation,
            openDoor ? openRotation : closedRotation,
            Time.deltaTime * openSpeed
        );
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
