using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : PlayerController
{
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>().normalized;
        CallMoveEvent(input);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            CallJumpEvent();
        }
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            CallDashEvent();
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            CallAttackEvent();
        }
    }
}
