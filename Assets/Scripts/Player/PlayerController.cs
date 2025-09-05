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

    private LifeController lifeController;

    [SerializeField] private PlayerData playerData;

    public bool isCrouching { get; private set; } = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Rigidbody settings: no rotation por f�sica, solo manual
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        if (playerData != null)
        {
            transform.position = playerData.position;
            if (lifeController != null)
            {
                lifeController.Health(playerData.health - lifeController.GetHealth());
            }
        }
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
        // Correr
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveDirection = GetInputDirection() * runSpeed;
        }
        else
        {
            moveDirection = GetInputDirection() * walkSpeed;
        }

        // Agacharse
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
            // Detener movimiento
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            return;
        }

        // Mantener la velocidad vertical (gravedad)
        Vector3 velocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z);

        rb.linearVelocity = velocity;

        // Rotaci�n suave hacia la direcci�n de movimiento
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

        // Guardar posici�n y salud
        if (playerData != null)
        {
            playerData.position = transform.position;
            if (lifeController != null)
            {
                playerData.health = lifeController.GetHealth();
            }
        }
    }

    private void ChangeWorld()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (Input.GetKeyDown(KeyCode.P))
        {
            SavePlayerData();

            if (currentSceneName == "EscenaPrueba")
            {
                SceneManager.LoadScene("PruebaCambioMundo");
            }
            else
            {
                SceneManager.LoadScene("EscenaPrueba");
            }
        }
    }

    private void SavePlayerData()
    {
        if (playerData != null)
        {
            playerData.position = transform.position;
            if (lifeController != null)
            {
                playerData.health = lifeController.GetHealth();
            }
        }
    }
}
