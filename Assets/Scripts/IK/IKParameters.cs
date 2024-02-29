using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKParameters : MonoBehaviour
{
    public int stepState;

    public float CDMove = 0.1f;//��·����֮�����̼��
    public float CDSprint = 0.02f;//�ܲ�����֮�����̼��
    public float CD = 0.1f;//��ǰ���õ�CDֵ
    public float stepLength;//��ǰ̽�ľ���
    public float stepDistance;//һ�������Ŷ��ľ���
    public float stepSpeed = 1;//�����ٶ�
    public float stepHeight = 1;//̧�ȸ߶�

    float cd;

    public void Add()
    {
        stepState++;
        stepState = stepState % 3;
        cd = CD;
    }
    private void Update()
    {
        cd -= Time.deltaTime;
        if(cd<=0) Add();
    }
}
