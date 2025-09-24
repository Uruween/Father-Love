using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private Transform cameraTransform;

    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;

    private float horizontalAxis;
    private float verticalAxis;

    private Vector3 moveDirection;


    public Camera cm;

    public bool isCrouching { get; private set; } = false;

    private void Start()
    {

        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        cm.backgroundColor = Color.black;

    }

    private void Update()
    {
        HandleInput();
        ChangeWorld();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveDirection = GetInputDirection() * runSpeed;
        }
        else
        {
            moveDirection = GetInputDirection() * walkSpeed;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
        }
    }

    private Vector3 GetInputDirection()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        Vector3 inputDir = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;

        if (inputDir.magnitude < 0.1f)
            return Vector3.zero;

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        Vector3 direction = camForward.normalized * verticalAxis + camRight.normalized * horizontalAxis;
        return direction.normalized;
    }

    private void Move()
    {
        if (moveDirection == Vector3.zero)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            return;
        }
        Vector3 velocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z);

        rb.linearVelocity = velocity;
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
       
    }

    private void ChangeWorld()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (cm.backgroundColor == Color.black)
            {
                cm.backgroundColor = Color.white;
            }
            else if (cm.backgroundColor == Color.white)
            {
                cm.backgroundColor = Color.black;
            }
        }
    }
   
}
