using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKParameters : MonoBehaviour
{
    public int stepState;

    public float CDMove = 0.1f;//走路步伐之间的最短间隔
    public float CDSprint = 0.02f;//跑步步伐之间的最短间隔
    public float CD = 0.1f;//当前采用的CD值
    public float stepLength;//向前探的距离
    public float stepDistance;//一步动画脚动的距离
    public float stepSpeed = 1;//动画速度
    public float stepHeight = 1;//抬腿高度

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
