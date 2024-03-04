using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    [SerializeField] int health;

    void Damage(int damage)
    {
        health -= damage;
        if (health <= 0) Broke();
    }
    void Broke()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        Destroy(gameObject, 15);
    }
}
