using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumericalSystem
{
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;

    private float amount;
    private float amountMax;

    public float Amount => amount;
    public float NormalizedAmount => amount / amountMax;

    public NumericalSystem(int amount)
    {
        amountMax = amount;
        this.amount = amount;
    }
    public void Damage(float amount)
    {
        this.amount -= amount;
        if (this.amount < 0)
        {
            this.amount = 0;
        }
        if (OnDamaged != null) OnDamaged(this, EventArgs.Empty);
    }
    public void Heal(float amount)
    {
        this.amount += amount;
        if (this.amount > amountMax)
        {
            this.amount = amountMax;
        }
        if (OnHealed != null) OnHealed(this, EventArgs.Empty);
    }
}
