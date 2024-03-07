using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class TreadCrackFade : MonoBehaviour
{
    DecalProjector decal;

    void Awake()
    {
        decal = GetComponent<DecalProjector>();
    }
    private void OnEnable()
    {
        decal.fadeFactor = 0;
        DOTween.To(() => decal.fadeFactor, x => decal.fadeFactor = x, 1, 0.25f);
        StartCoroutine(Fade());
    }
    IEnumerator Fade()
    {
        yield return new WaitForSeconds(1);
        DOTween.To(() => decal.fadeFactor, x => decal.fadeFactor = x, 0, 1);
    }
}
