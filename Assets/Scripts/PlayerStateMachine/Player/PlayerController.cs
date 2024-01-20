using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    PlayerInput input;
    Rigidbody rb;

    public Vector3 MoveSpeed => new Vector3(rb.velocity.x, 0, rb.velocity.z);
    public float verticalSpeed => rb.velocity.y;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        input.EnableGameplayInputs();
    }

    public void Move(Vector3 horizontalVelocity)
    {
        rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);
    }
}
