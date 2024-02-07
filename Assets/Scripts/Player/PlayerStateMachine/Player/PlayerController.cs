using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("µ÷ÊÔÑ¡Ïî")]
    [SerializeField] bool enableHitStop;
    [Range(0, 1)]
    [SerializeField] float mouseSensitivity;
    [Space]


    Transform cameraFollowPos;
    float xCamRot;
    float yCamRot;
    ParticleSystem sweatParticle;
    ParticleSystem dustParticle;

    PlayerInput input;
    Rigidbody rb;
    Animator animator;
    PlayerNumericalSystem numericalSystem;

    [HideInInspector] public Camera cam;
    public Vector3 MoveSpeed => new Vector3(rb.velocity.x, 0, rb.velocity.z);
    public float verticalSpeed => rb.velocity.y;

    public bool isLocking { get; set; }
    public Transform enemy;

    public bool HasStamina => numericalSystem.HasStamina;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
        numericalSystem = GetComponent<PlayerNumericalSystem>();
        animator = GetComponentInChildren<Animator>();
        cam = Camera.main;
    }
    private void Start()
    {
        input.EnableGameplayInputs();

        cameraFollowPos = transform.Find("CameraFollowPos");
        sweatParticle = transform.Find("SweatParticle").GetComponent<ParticleSystem>();
        dustParticle = transform.Find("DustParticle").GetComponent<ParticleSystem>();

        var triggers = GetComponentsInChildren<PlayerAttackTrigger>();
        foreach (var trigger in triggers)
        {
            trigger.hitEnemy += HitEnemy;
        }
    }
    private void Update()
    {
        if (input.Lock)
        {
            isLocking = !isLocking;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            DeductStamina(30);
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
        cam.DOShakePosition(0.1f, 0.075f, 3, 90, true);
    }
    IEnumerator HitStop()
    {
        animator.speed = 0f;
        //Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.1f);
        animator.speed = 1;
        //Time.timeScale = 1;
    }

    public void DustAnim(bool play)
    {
        if (play) dustParticle.Play();
        else dustParticle.Stop();
    }

    public void DeductStamina(float amount)
    {
        numericalSystem.DeductStamina(amount);

        if (!HasStamina)
        {
            StopCoroutine(nameof(SweatAnim));
            StartCoroutine(nameof(SweatAnim));
        }
    }
    IEnumerator SweatAnim()
    {
        sweatParticle.Play();
        yield return new WaitForSeconds(3);
        sweatParticle.Stop();
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
