using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomPlayerInput : PlayerInput
{
    [SerializeField] float attackInputBufferTime = 0.5f;
    WaitForSeconds waitInputBufferTime;
    public int HasAttackInputBuffer;

    PlayerInputActions playerInputActions;

    public Vector3 moveAxes
    {
        get 
        {
            Vector2 input = playerInputActions.Gameplay.Move.ReadValue<Vector2>();
            Vector3 output = new Vector3(input.x, 0, input.y);
            return output; 
        }
    }
    public Vector2 mouseAxes => playerInputActions.Gameplay.Look.ReadValue<Vector2>();
    public bool Sprint => playerInputActions.Gameplay.Sprint.IsPressed();
    public bool Dash => playerInputActions.Gameplay.Dash.WasPerformedThisFrame();
    public bool RightAttack => playerInputActions.Gameplay.RightAttack.WasPressedThisFrame();
    public bool LeftAttack => playerInputActions.Gameplay.LeftAttack.WasPressedThisFrame();
    public bool Attack => RightAttack||LeftAttack;
    public bool Lock => playerInputActions.Gameplay.Lock.WasPerformedThisFrame();
    public bool Move => moveAxes.magnitude != 0f;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            playerInputActions = GameManager.Instance.playerInputActions;
        }
        else
        {
            playerInputActions = new PlayerInputActions();
        }
        waitInputBufferTime = new WaitForSeconds(attackInputBufferTime);
    }
    public void SetAttackInputBufferTimer(int n)
    {
        StopCoroutine(nameof(AttackInputBufferCoroutine));
        StartCoroutine(nameof(AttackInputBufferCoroutine), n);
    }
    IEnumerator AttackInputBufferCoroutine(int n)
    {
        HasAttackInputBuffer = n;
        yield return waitInputBufferTime;
        HasAttackInputBuffer = 0;
    }
}
