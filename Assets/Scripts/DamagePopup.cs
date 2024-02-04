using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] float posRandRange;
    [SerializeField] float StartSpeed;
    [SerializeField] float SpeedDirRange;
    void OnEnable()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        transform.position += new Vector3(Random.Range(-posRandRange,posRandRange), Random.Range(-posRandRange, posRandRange), Random.Range(-posRandRange, posRandRange));
        //Invoke(nameof(Anim), 1f);
        Anim();
    }

    private void Anim()
    {
        Sequence sequence = DOTween.Sequence();
        print("here");
        sequence.Append(transform.DOScale(1.2f, 0.05f));
        sequence.Append(transform.DOScale(1, 0.05f));
        sequence.AppendInterval(0.2f);
        sequence.Append(transform.DOScale(0, 0.3f));

        sequence.Play();
    }
}
