using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PlayerInputActions playerInputActions;

    public Vector3 axes
    {
        get 
        {
            Vector2 input = playerInputActions.Gameplay.Move.ReadValue<Vector2>();
            Vector3 output = new Vector3(input.x, 0, input.y);
            return output; 
        }
    }

    public bool Move => axes.magnitude != 0f;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    public void EnableGameplayInputs()
    {
        playerInputActions.Gameplay.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }
}
