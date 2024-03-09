using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerController : MonoBehaviour
{
    [Header("调试选项")]
    [SerializeField] bool enableHitStop;
    [SerializeField] bool enableCameraShake;
    [Range(0, 1)]
    [SerializeField] float mouseSensitivity = 0.5f;
    [Range(15, 30)]
    [SerializeField] float gamepadSensitivityMultiplier;
    [Range(0, 100)]
    [SerializeField] float extraGravity;
    [Range(0, 180)]
    [SerializeField] float lockMaxAngle;
    [Space]
    [Header("绑定物体")]
    
    [SerializeField] GameObject focusImage;
    [SerializeField] PlayerAttackTrigger[] triggers;
    [SerializeField] IKParameters ikParameters;
    [SerializeField] Rig rig;

    public static string GAMEPAD_CONTROL_SCHEME = "Gamepad";
    public static string MNK_CONTROL_SCHEME = "MNK";
    public static string CURRENT_CONTROL_SCHEME;

    Transform cameraFollowPos;
    float xCamRot;
    float yCamRot;
    float sensitivity
    {
        get
        {
            if (input.currentControlScheme == GAMEPAD_CONTROL_SCHEME)
            {
                return mouseSensitivity * gamepadSensitivityMultiplier;
            }
            else if (input.currentControlScheme == MNK_CONTROL_SCHEME)
            {
                return mouseSensitivity;
            }
            else return 0;
        }
    }
    ParticleSystem sweatParticle;
    ParticleSystem dustParticle;
    GameObject HealingParticle;

    CustomPlayerInput input;
    PlayerStateMachine stateMachine;
    Rigidbody rb;
    Animator animator;
    PlayerNumericalSystem numericalSystem;

    [HideInInspector] public Camera cam;
    [HideInInspector] public Transform mesh;
    [HideInInspector] public Transform enemy;
    public Vector3 MoveSpeed => new Vector3(rb.velocity.x, 0, rb.velocity.z);
    public Vector3 DamagePos { get; private set; }
    public float verticalSpeed => rb.velocity.y;

    public bool isDashing { get; set; }
    public bool isLocking { get; set; }
    public bool isCamAnimPlaying { get; set; }
    public bool isNextTargetPorfomed { get; set; }
    public bool lockedOnGround { get; set; }
    public bool UseGravity { get; set; }
    public bool isDead { get; set; }

    public bool HasStamina => numericalSystem.HasStamina;

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
            if (LegMoving && input.moveAxes != Vector3.zero)
            {
                float targetRot = Quaternion.LookRotation(input.moveAxes).eulerAngles.y + cam.transform.rotation.eulerAngles.y;
                Vector3 dir = Quaternion.Euler(0, targetRot, 0) * Vector3.forward;
                return dir;
            }
            else return transform.forward;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<CustomPlayerInput>();
        stateMachine = GetComponent<PlayerStateMachine>();
        numericalSystem = GetComponent<PlayerNumericalSystem>();
        animator = GetComponentInChildren<Animator>();
        mesh = transform.Find("CrabMesh");
        cam = Camera.main;
    }
    private void Start()
    {
        cameraFollowPos = transform.Find("CameraFollowPos");
        sweatParticle = transform.Find("SweatParticle").GetComponent<ParticleSystem>();
        dustParticle = transform.Find("DustParticle").GetComponent<ParticleSystem>();
        HealingParticle = transform.Find("HealParticle").gameObject;

        EventCenter.AddListener(FunctionType.RestartLevel, Revive);

        var triggers = GetComponentsInChildren<PlayerAttackTrigger>();
        foreach (var trigger in triggers)
        {
            trigger.hitEnemy += HitEnemy;
        }

        UseGravity = false;
        lockedOnGround = true;
    }
    private void Update()
    {
        if (input.Lock)
        {
            if (!isLocking)
            {
                LockTarget(GetLockTarget());
            }
            else
            {
                UnlockTarget();
            }
        }
        if (isLocking)
        {
            if (!isCamAnimPlaying && !isNextTargetPorfomed &&
                (input.mouseAxes.x * sensitivity / mouseSensitivity <= -14
                || input.mouseAxes.x * sensitivity / mouseSensitivity >= 14))
            {
                isNextTargetPorfomed = true;
                LockTarget(GetNextTarget(input.mouseAxes.x));
            }
            else if (isNextTargetPorfomed && input.mouseAxes.x <= 1 && input.mouseAxes.x >= -1)
            {
                isNextTargetPorfomed = false;
            }
        }
    }
    private void FixedUpdate()
    {
        if (lockedOnGround)
        {
            Ray ray = new Ray(transform.position + new Vector3(0, 5, 0), Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit, 10, 1 << 6))
            {
                transform.position = hit.point;
            }
        }
        if (UseGravity)
        {
            rb.AddForce(new Vector3(0, -extraGravity, 0));
        }
    }
    private void LateUpdate()
    {
        CameraRot();
    }

    void LockTarget(Transform target)
    {
        if (target == null) return;
        enemy = target;
        PlayFocusCamAnim();
        focusImage.SetActive(true);
        isLocking = true;
    }
    void UnlockTarget()
    {
        focusImage.SetActive(false);
        isLocking = false;
    }
    public void NewTarget()
    {
        Transform target = GetNextTarget(1);
        if (target != null)
        {
            LockTarget(target);
            return;
        }
        else
        {
            target = GetNextTarget(-1);
            if (target != null)
            {
                target = GetNextTarget(-1);
                LockTarget(target);
            }
            else
            {
                UnlockTarget();
            }
        }
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
                {
                    target = enemy.transform;
                    curMinDistance = distance;
                }
            }
        }
        return target;
    }
    Transform GetNextTarget(float dir)
    {
        if (enemy == null) return null;

        Vector3 right = Quaternion.AngleAxis(90, Vector3.up) * cameraFollowPos.forward;
        var enemys = GameObject.FindGameObjectsWithTag("Focus");
        float curMinDistance = 1;
        Transform target = null;
        foreach (var enemy in enemys)
        {
            if (enemy.transform == this.enemy) continue;

            Vector3 enemyDir = enemy.transform.position - cameraFollowPos.position;
            if (enemyDir.magnitude >= 15) continue;

            float angle = Vector3.Angle(cameraFollowPos.forward, enemyDir);
            if (angle < lockMaxAngle)
            {
                float distance = Vector3.Dot(right.normalized, enemyDir.normalized);
                if (distance * dir > 0)
                {
                    if (Mathf.Abs(distance) < curMinDistance)
                    {
                        target = enemy.transform;
                        curMinDistance = Mathf.Abs(distance);
                    }
                }
            }
        }
        return target;
    }

    void PlayFocusCamAnim()
    {
        //StopCoroutine(nameof(FocusCamAnim));
        StartCoroutine(nameof(FocusCamAnim));
    }
    IEnumerator FocusCamAnim()
    {
        isCamAnimPlaying = true;

        float lerpAmount = 0;
        while (lerpAmount < 1)
        {
            Quaternion to = Quaternion.LookRotation(enemy.position - cameraFollowPos.position);
            cameraFollowPos.rotation = Quaternion.Lerp(cameraFollowPos.rotation, to, lerpAmount);
            lerpAmount += Time.deltaTime * 5f;

            Vector3 pos = cam.WorldToScreenPoint(enemy.position);
            focusImage.transform.position = pos;
            yield return null;
        }
        isCamAnimPlaying = false;
    }


    public void HitEnemy()
    {
        if (enableHitStop)
        {
            StopCoroutine(nameof(HitStop));
            StartCoroutine(nameof(HitStop));
        }
        if (enableCameraShake)
        {
            cam.DOShakePosition(0.1f, 0.025f, 3, 90, true);
        }
    }
    IEnumerator HitStop()
    {
        animator.speed = 0f;
        //Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.1f);
        animator.speed = 1;
        //Time.timeScale = 1;
    }
    public void ClearHitCache()
    {
        foreach(var t in triggers)
        {
            t.ClearColliderCache();
        }
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

    public void HoleEnter(Vector3[] fromToPos)
    {
        fromToPosTemp = fromToPos;
        stateMachine.SwitchState(typeof(PlayerState_HoleEnter));
    }

    IEnumerator SweatAnim()
    {
        sweatParticle.Play();
        yield return new WaitForSeconds(1);
        sweatParticle.Stop();
    }
    public void Damage(Tuple<int, Vector3> Info)
    {
        if (isDashing || isDead) return;
        DamagePos = Info.Item2;
        numericalSystem.Damage(Info.Item1);
        if (numericalSystem.HasHealth)
        {
            stateMachine.SwitchState(typeof(PlayerState_Hit));
        }
        else
        {
            stateMachine.SwitchState(typeof(PlayerState_Dead));
        }

    }
    public void Heal(float Amount)
    {
        HealingParticle.SetActive(true);
        numericalSystem.Heal((int)Amount);
        Invoke(nameof(StopHealingParticle), 1);
    }
    void Revive()
    {
        Heal(100);
        stateMachine.SwitchState(typeof(PlayerState_Idle));
    }
    void StopHealingParticle()
    {
        HealingParticle.SetActive(false);
    }

    public void SetRigWeight(float weight)
    {
        rig.weight = weight;
    }

    public void Move(Vector3 horizontalVelocity)
    {
        rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);
    }
    public void SetVerticalVelocity(float speed)
    {
        rb.velocity = new Vector3(rb.velocity.x, speed, rb.velocity.z);
    }
    public void AddImpulse(Vector3 impulse)
    {
        rb.AddForce(impulse, ForceMode.Impulse);
    }
    void CameraRot()
    {
        if (isCamAnimPlaying) return;
        if (isLocking)
        {
            if (enemy == null)
            {
                NewTarget();
            }
            Vector3 pos = cam.WorldToScreenPoint(enemy.position);
            focusImage.transform.position = pos;

            Quaternion rotation = Quaternion.LookRotation(enemy.position - cameraFollowPos.position);
            cameraFollowPos.rotation = rotation;
            xCamRot = cameraFollowPos.rotation.eulerAngles.x;
            yCamRot = cameraFollowPos.rotation.eulerAngles.y;
        }
        else
        {
            xCamRot -= input.mouseAxes.y * sensitivity;
            yCamRot += input.mouseAxes.x * sensitivity;
            if (xCamRot >= 180) xCamRot -= 360;
            xCamRot = Mathf.Clamp(xCamRot, -30, 70);
            Quaternion rotation = Quaternion.Euler(xCamRot, yCamRot, 0);
            cameraFollowPos.rotation = rotation;
        }
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(FunctionType.RestartLevel, Revive);
    }
}
