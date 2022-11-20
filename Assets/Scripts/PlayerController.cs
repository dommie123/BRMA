using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravity;
    [SerializeField] private SpringArm springArm;

    private bool jumped;
    private CharacterController controller;
    private PlayerActions playerActions;
    private GroundCheck groundCheckComponent;
    private Vector3 velocity;
    private Vector3 stickToGround;

    private void Awake() 
    {
        controller = GetComponent<CharacterController>();
        groundCheckComponent = transform.Find("Ground Check").GetComponent<GroundCheck>();

        playerActions = new PlayerActions();
        playerActions.PlayerController.Enable();
        // playerActions.PlayerController.Movement.performed += Movement;
        playerActions.PlayerController.LookAround.performed += LookAround;
        playerActions.PlayerController.JumpInteract.performed += JumpInteract;
        playerActions.PlayerController.Attack.performed += Attack;
        playerActions.PlayerController.DodgeRoll.performed += DodgeRoll;
        playerActions.PlayerController.SpecialAttackWeapon.performed += SpecialAttackWeapon;
        playerActions.PlayerController.Pause.performed += Pause;
        playerActions.PlayerController.AnimalFriendPowerMenu.performed += AnimalFriendPowerMenu;
        playerActions.PlayerController.TargetLockOn.performed += TargetLockOn;
        playerActions.PlayerController.CancelLockOn.performed += CancelLockOn;
        playerActions.PlayerController.SwitchPreviousTarget.performed += SwitchPreviousTarget;
        playerActions.PlayerController.SwitchNextTarget.performed += SwitchNextTarget;
        playerActions.PlayerController.Crouch.performed += Crouch;
        playerActions.PlayerController.BlockShield.performed += BlockShield;
        playerActions.PlayerController.WeaponsSelectionMenu.performed += WeaponsSelectionMenu;
        playerActions.PlayerController.GuardianEnhancementMenu.performed += GuardianEnhancementMenu;

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdatePhysics();
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        Vector2 inputVector = playerActions.PlayerController.Movement.ReadValue<Vector2>();

        // Player input coordinates
        float x = inputVector.x;
        float z = inputVector.y;

        // Camera forward and right vectors
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // Create new forward/right vectors relative to camera's rotation
        Vector3 forwardRelativeVerticalInput = z * forward;
        Vector3 rightRelativeVerticalInput = x * right;

        // Add the two vectors to get the new movement vector.
        Vector3 movement = forwardRelativeVerticalInput + rightRelativeVerticalInput;

        controller.Move(movement * speed * Time.deltaTime);
    }

    private void UpdatePhysics()
    {
        Vector3 gravityVector = Physics.gravity;
        bool isGrounded = groundCheckComponent.IsTouchingGround();

        if (!isGrounded && transform.parent != null)
            transform.parent = null;

        // Check to stick player to ground if they are grounded, unless they decide to jump. Otherwise, laws of physics apply.
        if (isGrounded && PlayerVelocityIsIncreasing())
        {
            velocity = stickToGround;
        }
        else
        {
            velocity += gravityVector * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    } 

    private bool PlayerVelocityIsIncreasing()
    {
        return velocity.y < 0;
    }

    // Control functions
    private void LookAround(InputAction.CallbackContext context)
    {
        springArm.Rotate(context);
    }

    private void JumpInteract(InputAction.CallbackContext context)
    {
        // TODO if near interactible object, trigger interact logic
        // Debug.Log(jumpHeight * -2f * gravity);
        if (groundCheckComponent.IsTouchingGround())
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            // controller.Move(new Vector3(controller.velocity.x, jumpHeight, controller.velocity.z));
    }

    private void Attack(InputAction.CallbackContext context)
    {
        Debug.Log("Attacking");
    }

    private void DodgeRoll(InputAction.CallbackContext context)
    {
        Debug.Log("DO A BARREL ROLL!");
    }

    private void SpecialAttackWeapon(InputAction.CallbackContext context)
    {
        Debug.Log("Special Attack engaging");
    }

    private void Pause(InputAction.CallbackContext context)
    {
        Debug.Log("Pausing game");
    }

    private void AnimalFriendPowerMenu(InputAction.CallbackContext context)
    {
        Debug.Log("Opening Animal Friend Power Menu");
    }

    private void TargetLockOn(InputAction.CallbackContext context)
    {
        Debug.Log("Locking on");
    }

    private void CancelLockOn(InputAction.CallbackContext context)
    {
        Debug.Log("Cancel lock on");
    }

    private void SwitchPreviousTarget(InputAction.CallbackContext context)
    {
        Debug.Log("Switching to previous target");
    }

    private void SwitchNextTarget(InputAction.CallbackContext context)
    {
        Debug.Log("Switching to next target");
    }

    private void Crouch(InputAction.CallbackContext context)
    {
        Debug.Log("Crouching");
    }

    private void BlockShield(InputAction.CallbackContext context)
    {
        Debug.Log("Blocking");
    }

    private void WeaponsSelectionMenu(InputAction.CallbackContext context)
    {
        Debug.Log("Opening Weapons Select Menu");
    }

    private void GuardianEnhancementMenu(InputAction.CallbackContext context)
    {
        Debug.Log("Opening Guardin Enhancement Menu");
    }
    // End control functions
}
