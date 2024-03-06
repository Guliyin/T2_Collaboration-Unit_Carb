using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossParameters : MonoBehaviour
{
    [Header("Tread, Grab, GrabTwice, Summon")]
    public int[] AbilityWeight;
    public int AbilityTotal
    {
        get
        {
            int n = 0;
            foreach (int i in AbilityWeight)
            {
                n += i;
            }
            return n;
        }
    }
    [Space]
    public float MoveSpeed;
    public float AttackDistance;
    public float TurnRate;
    public float ChargeDistance;
    public float CharageSpeed;
    public float ChargeMaxTime;
}
