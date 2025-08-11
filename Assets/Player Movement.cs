using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls playerControls;
    private CharacterController characterController;

    public Vector3 MoveDirection;

    public Vector2 MoveInput;
    public Vector2 AimInput;
    public float speed = 1.5f;

    private void Awake()
    {
        playerControls = new PlayerControls();
      
        playerControls.Character.Fire.performed += ctx => Shoot();
        playerControls.Character.Movement.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        playerControls.Character.Movement.canceled += ctx => MoveInput = Vector2.zero;

        playerControls.Character.Aim.performed += ctx => AimInput = ctx.ReadValue<Vector2>();
        playerControls.Character.Aim.canceled += ctx => AimInput = Vector2.zero;
    }
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Shoot()
    {
        // Implement shooting logic here
        Debug.Log("Shoot action performed");
    }
    private void Update()
    {
        ApplyMovement();
        //AimTowardsMouse();
        //UpdateAnimation();
    }

    private void ApplyMovement()
    {
        if (characterController != null)
        {
            MoveDirection = new Vector3(MoveInput.x, 0, MoveInput.y);
            //ApplyGravity();
            if (MoveDirection.magnitude > 0)
            {
                characterController.Move(MoveDirection * Time.deltaTime * speed);

            }
        }
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
}
