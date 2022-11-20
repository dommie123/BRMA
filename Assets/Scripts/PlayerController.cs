using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private void Awake() 
    {
        PlayerActions playerActions = new PlayerActions();
        playerActions.PlayerController.Enable();
        playerActions.PlayerController.Movement.performed += Movement;
        playerActions.PlayerController.LookAround.performed += LookAround;
        playerActions.PlayerController.Jump.performed += Jump;
        playerActions.PlayerController.Interact.performed += Interact;
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
    private void Update()
    {
        
    }

    private void Movement(InputAction.CallbackContext context)
    {
        Debug.Log("Moving");
    }

    private void LookAround(InputAction.CallbackContext context)
    {
        Debug.Log("Looking around");
    }

    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jumping");
    }

    private void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Interacting with object");
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
}
