using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float accelerationSpeed = 3f;

    public bool isAttacking { get; set; } = false;
    public bool isPickingUp { get; set; } = false;

    private float horizontalAxis;
    private float verticalAxis;
    private Vector3 moveDirection;
    private float acceleration = 0f;
    PlayerInput playerInput;

    public GameObject lantern;
    [SerializeField]bool isLanternOn = false;

    public Camera cm;
    private Animator animator;
    public bool isCrouching { get; private set; } = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        // Configuración del Rigidbody para evitar giros
        rb.constraints = RigidbodyConstraints.FreezeRotation; // Congelar TODA la rotación
        rb.interpolation = RigidbodyInterpolation.Interpolate; // Suavizar movimiento
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; // Mejor detección de colisión
        lantern.SetActive(false);
        isLanternOn = false;
    }

    private void Update()
    {
        HandleInput();
        ChangeWorld();
        if(playerInput.actions["Activate"].WasPressedThisFrame())
        {
            isLanternOn = !isLanternOn;
            lantern.SetActive(isLanternOn);
        }
    }

    private void FixedUpdate()
    {
        Move();

        // CRÍTICO: Forzar rotación solo en Y cada frame de física
        Vector3 currentRotation = transform.eulerAngles;
        transform.eulerAngles = new Vector3(0f, currentRotation.y, 0f);

        // Eliminar cualquier velocidad angular residual
        rb.angularVelocity = Vector3.zero;
    }

    private void HandleInput()
    {
        if (isAttacking || isPickingUp)
        {
            horizontalAxis = 0f;
            verticalAxis = 0f;
            acceleration = 0f;
            moveDirection = Vector3.zero;
            animator.SetFloat("Walk", 0f);
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
        }

        horizontalAxis = playerInput.actions["Move"].ReadValue<Vector2>().x;
        verticalAxis = playerInput.actions["Move"].ReadValue<Vector2>().y;

        bool isMoving = Mathf.Abs(horizontalAxis) > 0.1f || Mathf.Abs(verticalAxis) > 0.1f;
        bool isRunning = playerInput.actions["Run"].IsPressed();

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
        else if (playerInput.actions["Run"].IsPressed())
        {
            actualSpeed = runSpeed;
        }

        Vector3 targetVelocity = moveDirection * actualSpeed;
        Vector3 velocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
        rb.linearVelocity = velocity;

        // Rotar hacia la dirección de movimiento
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
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