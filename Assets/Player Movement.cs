using System;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Player player;
    private PlayerControls playerControls;
    private CharacterController characterController;
    private Animator animator;

    public Vector3 MoveDirection;

    public Vector2 MoveInput;
    public Vector2 AimInput;
    public float speed = 1.5f;
    private float verticalVelocity;
    private bool isRunning;
    [SerializeField] private LayerMask aimlayermask;
    [SerializeField] private Transform aim; // Reference to the aim transform
    public float walkSpead;
    public float runSpeed;


    private void AssignInputEvents()
    {
        playerControls = player.controls;


        playerControls.Character.Movement.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        playerControls.Character.Movement.canceled += ctx => MoveInput = Vector2.zero;

        playerControls.Character.Aim.performed += ctx => AimInput = ctx.ReadValue<Vector2>();
        playerControls.Character.Aim.canceled += ctx => AimInput = Vector2.zero;

        playerControls.Character.Run.performed += ctx =>
        {
            speed = runSpeed;
            isRunning = true;
        };// Increase speed when running
        playerControls.Character.Run.canceled += ctx =>
        {
            speed = walkSpead;
            isRunning = false; // Reset speed when not running
        };
    }

    private void Start()
    {
        player = GetComponent<Player>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        AssignInputEvents();
    }

    private void Update()
    {
        ApplyMovement();
        AimTowardsMouse();
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        float xVelocity = Vector3.Dot(MoveDirection.normalized, transform.right);
        float zVelocity = Vector3.Dot(MoveDirection.normalized, transform.forward);
        animator.SetFloat("xVelocity", xVelocity, .1f, Time.deltaTime);
        animator.SetFloat("zVelocity", zVelocity, .1f, Time.deltaTime);
        if(isRunning && MoveDirection.magnitude > 0.5f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
     
        //if (MoveDirection.magnitude > 0)
        //{
        //    animator.SetBool("IsMoving", true);
        //}
        //else
        //{
        //    animator.SetBool("IsMoving", false);
        //  }

    }


    private void ApplyMovement()
    {
        if (characterController != null)
        {
            MoveDirection = new Vector3(MoveInput.x, 0, MoveInput.y);
            ApplyGravity();
            if (MoveDirection.magnitude > 0)
            {
                characterController.Move(MoveDirection * Time.deltaTime * speed);

            }
        }
    }


    private void ApplyGravity()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = -0.5f; // Small downward force to keep grounded
        }
        else
        {
            verticalVelocity -= 9.81f * Time.deltaTime; // Apply gravity
        }
        MoveDirection.y = verticalVelocity;
    }

    private void AimTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(AimInput);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimlayermask))
        {
            Vector3 aimDirection = (hit.point - transform.position).normalized;
            aimDirection.y = 0; // Keep the aim direction horizontal
            transform.forward = aimDirection; // Rotate the player to face the aim direction
            aim.position = new Vector3(hit.point.x, aim.position.y, hit.point.z); // Update aim position
        }
    }
}
