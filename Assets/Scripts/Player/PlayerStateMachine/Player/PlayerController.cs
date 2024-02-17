using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("调试选项")]
    [SerializeField] bool enableHitStop;
    [Range(0, 1)]
    [SerializeField] float mouseSensitivity;
    [Range(0, 100)]
    [SerializeField] float extraGravity;
    [Space]
    [Range(0, 180)]
    [SerializeField] float lockMaxAngle;
    [Space]


    Transform cameraFollowPos;
    float xCamRot;
    float yCamRot;
    ParticleSystem sweatParticle;
    ParticleSystem dustParticle;

    PlayerInput input;
    PlayerStateMachine stateMachine;
    Rigidbody rb;
    Animator animator;
    PlayerNumericalSystem numericalSystem;

    [HideInInspector] public Camera cam;
    public Vector3 MoveSpeed => new Vector3(rb.velocity.x, 0, rb.velocity.z);
    public float verticalSpeed => rb.velocity.y;

    public bool isLocking { get; set; }
    public bool isCamAnimPlaying { get; set; }
    public bool CamAnimPlaying { get; set; }
    [Header("绑定物体")]
    public Transform enemy;
    public Transform mesh;

    public bool HasStamina => numericalSystem.HasStamina;

    [SerializeField] IKParameters ikParameters;
    [SerializeField] Rig rig;
    public Action resetLegs;
    private bool legMoving;
    public bool LegMoving
    {
        get { return legMoving; }
        set
        {
            legMoving = value;
            ikParameters.CD = LegMoving ? ikParameters.CDMove : ikParameters.CDSprint;
        }
    }

    public Vector3[] fromToPosTemp { get; set; }
    public Vector3 legDir
    {
        get
        {
            if (LegMoving)
            {
                float targetRot = Quaternion.LookRotation(input.moveAxes).eulerAngles.y + cam.transform.rotation.eulerAngles.y;
                Vector3 dir = Quaternion.Euler(0, targetRot, 0) * Vector3.forward;
                return dir;
            }
            else return transform.forward;
        }
    }
    public bool UseGravity { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
        stateMachine = GetComponent<PlayerStateMachine>();
        numericalSystem = GetComponent<PlayerNumericalSystem>();
        animator = GetComponentInChildren<Animator>();
        mesh = transform.Find("CrabMesh");
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

        UseGravity = true;
    }
    private void Update()
    {
        if (input.Lock)
        {
            if (!isLocking)
            {
                LockTarget();
            }
            else
            {
                UnlockTarget();
            }
        }
    }
    private void FixedUpdate()
    {
        if (UseGravity)
        {
            rb.AddForce(new Vector3(0, -extraGravity, 0));
        }
    }
    private void LateUpdate()
    {
        CameraRot();
    }

    void LockTarget()
    {
        enemy = GetLockTarget();
        if (enemy == null) return;
        PlayFocusCamAnim();
        isLocking = true;
    }
    void UnlockTarget()
    {
        isLocking= false;
    }

    void PlayFocusCamAnim()
    {
        StopCoroutine(nameof(FocusCamAnim));
        StartCoroutine(nameof(FocusCamAnim));
    }
    IEnumerator FocusCamAnim()
    {
        isCamAnimPlaying = true;
        Quaternion to = Quaternion.LookRotation(enemy.position - cameraFollowPos.position);

        float lerpAmount = 0;
        while (lerpAmount < 1)
        {
            cameraFollowPos.rotation = Quaternion.Lerp(cameraFollowPos.rotation, to, lerpAmount);
            lerpAmount += Time.deltaTime * 10f;
            yield return null;
        }
        isCamAnimPlaying = false;
    }

    Transform GetLockTarget()
    {
        //Physics.OverlapSphere(cameraFollowPos.position,30);
        var enemys = GameObject.FindGameObjectsWithTag("Focus");
        float curMinDistance = 9999;
        Transform target = null;
        foreach (var enemy in enemys)
        {
            Vector3 enemyDir = enemy.transform.position - cameraFollowPos.position;
            float distance = enemyDir.magnitude;
            if (distance < curMinDistance)
            {
                float angle = Vector3.Angle(cameraFollowPos.forward, enemyDir);
                if (angle < lockMaxAngle)
                    target = enemy.transform;
                    curMinDistance = distance;
            }
        }
        return target;
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

    void HoleEnter(Vector3[] fromToPos)
    {
        print("?");
        fromToPosTemp = fromToPos;
        stateMachine.SwitchState(typeof(PlayerState_HoleEnter));
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
    public void SetVerticalVelocity(float speed)
    {
        rb.velocity = new Vector3(rb.velocity.x, speed, rb.velocity.z);
    }
    void CameraRot()
    {
        if (isCamAnimPlaying) return;
        if (isLocking)
        {
            Quaternion rotation = Quaternion.LookRotation(enemy.position - cameraFollowPos.position);
            cameraFollowPos.rotation = rotation;
            xCamRot = cameraFollowPos.rotation.eulerAngles.x;
            yCamRot = cameraFollowPos.rotation.eulerAngles.y;
        }
        else
        {
            xCamRot -= input.mouseAxes.y * mouseSensitivity;
            yCamRot += input.mouseAxes.x * mouseSensitivity;
            if (xCamRot >= 180) xCamRot -= 360;
            xCamRot = Mathf.Clamp(xCamRot, -30, 70);
            Quaternion rotation = Quaternion.Euler(xCamRot, yCamRot, 0);
            cameraFollowPos.rotation = rotation;
        }
    }
}
