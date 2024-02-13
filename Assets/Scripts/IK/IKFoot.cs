using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFoot : MonoBehaviour
{
    [SerializeField] int Index;
    IKParameters parameters;

    PlayerController player;
    Transform root;
    Vector3 posOffset;
    Vector3 lastWorldPos, curWorldPos;

    float lerpAmount;
    Vector3 lastPlayerRot;
    private void Awake()
    {
        root = transform.parent.parent;
        player = root.parent.parent.GetComponent<PlayerController>();
        parameters = root.GetComponent<IKParameters>();

        player.resetLegs += ResetLegs;
    }
    private void Start()
    {
        lastPlayerRot = player.transform.forward;
        posOffset = transform.localPosition * 3;
        lastWorldPos = transform.position;
        curWorldPos = transform.position;
    }
    private void Update()
    {
        LerpFootPosition();

        Ray ray = new Ray(root.position + Quaternion.AngleAxis(player.transform.rotation.eulerAngles.y, Vector3.up) * posOffset + new Vector3(0, 10, 0), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 20, 1 << 6))
        {
            var deltaDistance = Vector3.Distance(lastWorldPos, hit.point);
            if (deltaDistance >= parameters.stepDistance)
            {
                if(parameters.stepState == Index)
                    NewStep(hit.point);
            }
            else if (Vector3.Dot(lastPlayerRot, player.transform.forward) <= 0.8f)
            {
                lastPlayerRot = player.transform.forward;
                NewStep(hit.point);
            }
        }
    }

    void LerpFootPosition()
    {
        lerpAmount += Time.deltaTime * parameters.stepSpeed;
        lerpAmount = Mathf.Clamp01(lerpAmount);

        var LerpTarget = curWorldPos + Vector3.up * Mathf.Sin(lerpAmount * Mathf.PI) * parameters.stepHeight;
        transform.position = Vector3.Lerp(transform.position, LerpTarget, lerpAmount);
    }
    void NewStep(Vector3 pos)
    {
        lerpAmount = 0;
        lastWorldPos = curWorldPos;
        curWorldPos = pos + player.legDir * parameters.stepLength + new Vector3(0, posOffset.y, 0);
    }
    public void ResetLegs()
    {
        Ray ray = new Ray(root.position + Quaternion.AngleAxis(player.transform.rotation.eulerAngles.y, Vector3.up) * posOffset + new Vector3(0, 10, 0), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 20, 1 << 6))
        {
            NewStep(hit.point);
        }
    }
}