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
    [SerializeField] private Light directionalLight;

    private bool jumped;
    private bool cameraFocusedOnPlayer;
    private float speedModifier;
    private CharacterController controller;
    private PlayerActions playerActions;
    private GroundCheck groundCheckComponent;
    private Vector3 velocity;
    private Vector3 stickToGround;
    private HealthManager health;
    private StatusManager status;
    private Light viewLight;
    public GameObject cameraTarget;

    //dummy camera target DELETE LATER
    public GameObject enemy;

    private void Awake() 
    {
        jumped = false;
        cameraFocusedOnPlayer = true;
        springArm.target = cameraTarget.transform; // sets the camera target here rather than through multiple places
        speedModifier = 1f;
        controller = GetComponent<CharacterController>();
        groundCheckComponent = transform.Find("Ground Check").GetComponent<GroundCheck>();
        health = GetComponent<HealthManager>();
        status = GetComponent<StatusManager>();
        viewLight = transform.Find("View Light").gameObject.GetComponent<Light>();

        playerActions = new PlayerActions();
        playerActions.PlayerController.Enable();
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

        if (health.EntityIsDead() || status.GetCurrentStatusEffects().Contains(StatusEffect.Stunned)) 
        {
            Debug.Log("I am dead/stunned!");
            if (playerActions.PlayerController.enabled)
                playerActions.PlayerController.Disable();
            
            return;
        }
        else if (!status.GetCurrentStatusEffects().Contains(StatusEffect.Frozen))
        {
            UpdateMovement();
        }

        UpdateAdditionalStatusEffects();

        UpdateCamera();
    }

    private void UpdateMovement()
    {
        Vector2 inputVector = playerActions.PlayerController.Movement.ReadValue<Vector2>();

        // Player input coordinates, put into vector2 to normalize the inputs
        Vector2 moveInput = new Vector2(inputVector.x, inputVector.y);

        float x = moveInput.x;
        float z = moveInput.y;

        // Camera forward and right vectors
        Vector3 forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
        Vector3 right = Camera.main.transform.right;

        // Create new forward/right vectors relative to camera's rotation, also only uses the x and z values so that the player doesnt skip in the air
        Vector3 forwardRelativeVerticalInput = z * new Vector3(forward.x, 0, forward.z);
        Vector3 rightRelativeVerticalInput = x * new Vector3(right.x, 0, right.z);

        // Add the two vectors to get the new movement vector.
        Vector3 movement = forwardRelativeVerticalInput + rightRelativeVerticalInput;

        controller.Move(movement * (speed * speedModifier) * Time.deltaTime);
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

    private void UpdateAdditionalStatusEffects()
    {
        List<StatusEffect> playerStatusEffects = status.GetCurrentStatusEffects();

        if (playerStatusEffects.Contains(StatusEffect.SlowDown))
        {
            speedModifier = 0.5f;
        }

        else if (!playerStatusEffects.Contains(StatusEffect.SlowDown))
        {
            speedModifier = 1f;
        }

        if (playerStatusEffects.Contains(StatusEffect.Blindness) && !viewLight.gameObject.activeInHierarchy)
        {
            directionalLight.transform.Rotate(new Vector3(180, 0, 0));
            viewLight.gameObject.SetActive(true);
            Debug.Log("Help! I'm blind!");
        }

        else if (!playerStatusEffects.Contains(StatusEffect.Blindness) && viewLight.gameObject.activeInHierarchy)
        {
            directionalLight.transform.Rotate(new Vector3(180, 0, 0));
            viewLight.gameObject.SetActive(false);
            Debug.Log("I'm not blind anymore!");
        }
    }

    private void UpdateCamera()
    {
        if (cameraFocusedOnPlayer == true) //THE PLAYERS TARGET SHOULDNT BE A FUCKING BOOL
        {
            cameraTarget.transform.position = transform.position;
            springArm.targetArmLength = 7;
        }
        else if (cameraFocusedOnPlayer == false)
        {
            cameraTarget.transform.position = (transform.position + enemy.transform.position)/2;
            springArm.targetArmLength = Vector3.Distance(transform.position, enemy.transform.position);
        }
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
        GameManager.instance.PauseGame();
    }

    private void AnimalFriendPowerMenu(InputAction.CallbackContext context)
    {
        GameManager.instance.OpenAnimalFriendPowerMenu();
    }

    private void TargetLockOn(InputAction.CallbackContext context)
    {
        Debug.Log("Locking on");
        cameraFocusedOnPlayer = false; //MAKING THE PLAYERS TARGET A BOOL IS FUCKING STUPID
    }

    private void CancelLockOn(InputAction.CallbackContext context)
    {
        Debug.Log("Cancel lock on");
        cameraFocusedOnPlayer = true;
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
        GameManager.instance.OpenWeaponSelectionMenu();
    }

    private void GuardianEnhancementMenu(InputAction.CallbackContext context)
    {
        GameManager.instance.OpenGuardianEnhancementMenu();
    }
    // End control functions
}
