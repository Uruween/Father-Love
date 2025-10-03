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
    [SerializeField] private float accelerationSpeed = 3f;

    public bool isAttacking { get; set; } = false;
    public bool isPickingUp { get; set; } = false; // Para bloquear movimiento al recoger items

    private float horizontalAxis;
    private float verticalAxis;
    private Vector3 moveDirection;
    private float acceleration = 0f;

    public Camera cm;
    private Animator animator;
    public bool isCrouching { get; private set; } = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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
        // Si está atacando o recogiendo items, no procesar más input
        if (isAttacking || isPickingUp)
        {
            horizontalAxis = 0f;
            verticalAxis = 0f;
            acceleration = 0f;
            moveDirection = Vector3.zero;
            animator.SetFloat("Walk", 0f);
            return;
        }

        // AGACHARSE - Control Izquierdo
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
        }

        // Obtener input de movimiento
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        bool isMoving = Mathf.Abs(horizontalAxis) > 0.1f || Mathf.Abs(verticalAxis) > 0.1f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float targetAcceleration = 0f;

        if (isMoving)
        {
            if (isCrouching)
            {
                targetAcceleration = 0.25f;
            }
            else if (isRunning)
            {
                targetAcceleration = 1f;
            }
            else
            {
                targetAcceleration = 0.5f;
            }
        }
        else
        {
            targetAcceleration = 0f;
        }

        acceleration = Mathf.MoveTowards(acceleration, targetAcceleration, accelerationSpeed * Time.deltaTime);
        moveDirection = GetInputDirection();
        animator.SetFloat("Walk", acceleration);
    }

    private Vector3 GetInputDirection()
    {
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
        // Detener movimiento si está atacando, recogiendo items, o no hay dirección
        if (moveDirection == Vector3.zero || isAttacking || isPickingUp)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            return;
        }

        float actualSpeed = walkSpeed;

        if (isCrouching)
        {
            actualSpeed = walkSpeed * 0.5f;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            actualSpeed = runSpeed;
        }

        Vector3 targetVelocity = moveDirection * actualSpeed;
        Vector3 velocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
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