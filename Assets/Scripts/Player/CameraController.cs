using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;         
    [SerializeField] private Vector3 offset = new Vector3(0, 2, -4);
    [SerializeField] private float rotationSpeed = 3f;

    [SerializeField] Vector3 normalOffset = new Vector3(0, 2, -4);
    [SerializeField] Vector3 crouchOffset = new Vector3(0, 1.2f, -2f);
    [SerializeField] private PlayerController playerController;

    private float yaw;
    private float pitch;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch = Mathf.Clamp(pitch, -30f, 60f);

        offset = Vector3.Lerp(offset, playerController.isCrouching ? crouchOffset : normalOffset, Time.deltaTime * 5f);


        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        transform.position = desiredPosition;
        transform.LookAt(target);
    }
}
