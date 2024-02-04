using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform cameraFollowPos;
    float xCamRot;
    float yCamRot;

    PlayerInput input;
    Rigidbody rb;
    Animator animator;

    [HideInInspector] public Camera cam;
    public Vector3 MoveSpeed => new Vector3(rb.velocity.x, 0, rb.velocity.z);
    public float verticalSpeed => rb.velocity.y;

    public bool isLocking { get; set; }
    public Transform enemy;

    [Space]
    [Header("µ÷ÊÔÑ¡Ïî")]
    [SerializeField] bool enableHitStop;
    [Range(0, 1)]
    [SerializeField] float mouseSensitivity;

    [SerializeField] float a;
    [SerializeField] float b;
    [SerializeField] int c;
    [SerializeField] float d;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
        animator = GetComponentInChildren<Animator>();
        cam = Camera.main;
    }
    private void Start()
    {
        input.EnableGameplayInputs();
        var triggers = GetComponentsInChildren<PlayerAttackTrigger>();
        foreach (var trigger in triggers)
        {
            trigger.hit += HitEnemy;
        }
    }
    private void Update()
    {
        if (input.Lock)
        {
            isLocking = !isLocking;
        }
    }
    private void LateUpdate()
    {
        CameraRot();
    }

    public void HitEnemy()
    {
        if (enableHitStop)
        {
            StopCoroutine(nameof(HitStop));
            StartCoroutine(nameof(HitStop));
        }
        cam.DOShakePosition(a, b, c, d, true);
    }
    IEnumerator HitStop()
    {
        animator.speed = 0f;
        //Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.1f);
        //Time.timeScale = 1;
        animator.speed = 1;
    }

    public void Move(Vector3 horizontalVelocity)
    {
        rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);
    }
    void CameraRot()
    {
        if (isLocking)
        {
            Quaternion rotation = Quaternion.LookRotation(enemy.position - cameraFollowPos.position);
            //cameraFollowPos.rotation = rotation;
            cameraFollowPos.rotation = rotation;
            xCamRot = cameraFollowPos.rotation.eulerAngles.x;
            yCamRot = cameraFollowPos.rotation.eulerAngles.y;
        }
        else
        {
            xCamRot -= input.mouseAxes.y * mouseSensitivity;
            yCamRot += input.mouseAxes.x * mouseSensitivity;
            xCamRot = Mathf.Clamp(xCamRot, -30, 70);
            Quaternion rotation = Quaternion.Euler(xCamRot, yCamRot, 0);
            cameraFollowPos.rotation = rotation;
        }
    }
}
