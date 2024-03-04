using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTwo : MonoBehaviour
{
    [SerializeField] MinionController[] minions;

    int dieCount;

    private void Start()
    {
        foreach (var minion in minions)
        {
            minion.Die += MinionDie;
        }
    }
    void MinionDie()
    {
        dieCount++;
        if (dieCount >= 3)
        {
            GameManager.Instance.LoadLevelThree();
        }
    }
}
