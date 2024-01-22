using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
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

    public bool Move => moveAxes.magnitude != 0f;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }
    private void Update()
    {
        
    }
    public void EnableGameplayInputs()
    {
        playerInputActions.Gameplay.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }
}
