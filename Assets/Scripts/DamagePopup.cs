using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] float posRandRange;
    [SerializeField] Vector2 startSpeedRange;
    float startSpeedX;
    float startSpeedY;
    public void Init(int damage)
    {
        GetComponentInChildren<TMP_Text>().text = damage.ToString();

        transform.localPosition = Vector3.zero;

        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        transform.localPosition += new Vector3(Random.Range(-posRandRange, posRandRange), Random.Range(-posRandRange, posRandRange), Random.Range(-posRandRange, posRandRange));
        //Invoke(nameof(Anim), 1f);

        startSpeedX = Random.Range(-startSpeedRange.x, startSpeedRange.x);
        startSpeedY = Random.Range(startSpeedRange.y - 1, startSpeedRange.y);

        Anim();
    }

    private void Anim()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(1.2f, 0.05f));
        sequence.Append(transform.DOScale(1, 0.05f));
        sequence.AppendInterval(0.2f);
        sequence.Append(transform.DOScale(0, 0.3f));

        sequence.Play();
    }
    private void Update()
    {
        startSpeedY += -9.8f * Time.deltaTime;
        transform.localPosition += new Vector3(startSpeedX * Time.deltaTime, startSpeedY * Time.deltaTime, 0);
    }
}
