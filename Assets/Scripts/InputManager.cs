using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputManger")]
public class InputManager : ScriptableObject, PlayerInputActions.IPlayerActions, PlayerInputActions.IUIActions
{
    PlayerInputActions playerInputActions;

    public event Action<Vector2> MoveEvent;
    public event Action PauseEvent;
    public event Action ResumeEvent;
    public event Action JumpEvent;
    public event Action JumpCanceledEvent;
    public event Action DashEvent;
    public event Action AttackEvent;
    public event Action ThrowEvent;
    private void OnEnable()
    {
        if (playerInputActions == null) 
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.SetCallbacks(this);
            playerInputActions.UI.SetCallbacks(this);
        }

        SetPlayer();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            JumpEvent?.Invoke();
        }
        if(context.canceled)
        {
            JumpCanceledEvent?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            DashEvent?.Invoke();
        }   
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            AttackEvent?.Invoke();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            PauseEvent?.Invoke();
            SetUI();
        }      
    }

    public void OnResume(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            ResumeEvent?.Invoke();
            SetPlayer();
        }
    }

    public void SetPlayer()
    {
        playerInputActions.Player.Enable();
        playerInputActions.UI.Disable();
    }

    public void SetUI()
    {
        playerInputActions.Player.Disable();
        playerInputActions.UI.Enable();
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ThrowEvent?.Invoke();
        }
    }
}
