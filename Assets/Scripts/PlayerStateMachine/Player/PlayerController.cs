using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform cameraFollowPos;
    float xCamRot;
    float yCamRot;

    [Range(0,1)]
    [SerializeField] float mouseSensitivity;

    PlayerInput input;
    Rigidbody rb;

    [HideInInspector] public Camera cam;
    public Vector3 MoveSpeed => new Vector3(rb.velocity.x, 0, rb.velocity.z);
    public float verticalSpeed => rb.velocity.y;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
        cam = Camera.main;
    }
    private void Start()
    {
        input.EnableGameplayInputs();
    }
    private void LateUpdate()
    {
        CameraRot();
    }

    public void Move(Vector3 horizontalVelocity)
    {
        rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);
    }
    void CameraRot()
    {
        xCamRot -= input.mouseAxes.y * mouseSensitivity;
        yCamRot += input.mouseAxes.x * mouseSensitivity;
        xCamRot = Mathf.Clamp(xCamRot, -30, 70);
        Quaternion rotation = Quaternion.Euler(xCamRot, yCamRot, 0);
        cameraFollowPos.rotation = rotation;
    }
}
