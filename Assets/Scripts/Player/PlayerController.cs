using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController character;
    [SerializeField] private Transform cameraTransform; 

    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float gravity = -9.81f;
    private float horizontalAxis;
    private float verticalAxis;
    private Vector3 velocity;

    private LifeController lifeController;


   
    public bool isCrouching { get; private set; } = false;

    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed += 0.05f;
            if (speed > 7f) speed = 7f;
        }
        else
        {
            speed -= 0.05f;
            if (speed < 3f) speed = 3f;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            
        }

        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0f;
            camRight.y = 0f;

            Vector3 moveDirection = camForward * verticalAxis + camRight * horizontalAxis;

           
            character.Move(moveDirection.normalized * speed * Time.deltaTime);

           
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;
        character.Move(velocity * Time.deltaTime);
    }
}

