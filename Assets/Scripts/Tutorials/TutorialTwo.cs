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
    void MinionDie(MinionController minion)
    {
        dieCount++;
        minion.Die -= MinionDie;
        if (dieCount >= 3)
        {
            GameManager.Instance.LoadLevelThree();
            Destroy(gameObject, 0.5f);
        }
    }
}
