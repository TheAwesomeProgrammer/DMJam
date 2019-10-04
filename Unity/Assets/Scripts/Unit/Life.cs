using UnityEngine;
using System.Collections;
using System;

public delegate void Died(Unit killedBy);

[Serializable]
public class Life
{
    private bool _wasDeadLastFrame;
    private bool _isDamageable;
    private Unit _lastDealtDamage;
    private Unit _ownerUnit;

    [SerializeField]
    private int _health;

    public int Health
    {
        get { return _health; }
        private set { _health = value; }
    }

    public bool IsDead => Health <= 0;

    public event Died Died;

    public void Init(bool isDamageable, Unit ownerUnit)
    {
        _isDamageable = isDamageable;
    }

    public void TakeDamage(int damage, Unit unitDealingDamage)
    {
        if (damage < 0)
        {
            return;
        }

        _lastDealtDamage = unitDealingDamage;
        Health -= damage;

        if (Health < 0)
        {
            Health = 0;
        }
    }

    public void Heal(int healAmount)
    {
        if (healAmount < 0)
        {
            return;
        }

        Health += healAmount;
    }

    public void Update()
    {
        if (_isDamageable && !_wasDeadLastFrame && IsDead)
        {
            Die();
        }

        _wasDeadLastFrame = IsDead;
    }

    public void Die()
    {
        if (_isDamageable)
        {
            _lastDealtDamage.OnKilledUnit(_ownerUnit);
            _health = 0;
            _wasDeadLastFrame = true;
            Died?.Invoke(_lastDealtDamage);
        }
    }
}

