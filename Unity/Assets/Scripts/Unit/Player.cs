using UnityEngine;
using System.Collections;
using System;

public class Player : Unit
{
    private const string FIRE_INPUT = "PlayerFire";
    private const int BABY_KILL_QUOTA = 3;

    private int _babiesKilled;

    [SerializeField]
    private PlayerWeapons _playerWeapons;

    [SerializeField]
    private PlayerMovement _playerMovement;

    public override UnitType UnitType => UnitType.Player;

    public override bool Damageable => false;
    public PlayerMovement PlayerMovement => _playerMovement;
    public PlayerWeapons PlayerWeapons => _playerWeapons;

    public event Action PlayerFlipped;
    public event Action MetKilledBabyQuota;

    protected override void Update()
    {
        base.Update();
        if (Input.GetButtonDown(FIRE_INPUT))
        {
            Weapon currentWeapon = _playerWeapons.CurrentWeapon;
            currentWeapon.Fire();
        }
    }

    public override void OnKilledUnit(Unit unit)
    {
        base.OnKilledUnit(unit);
        if(unit.UnitType == UnitType.Baby)
        {
            _babiesKilled++;
        }
        if(_babiesKilled >= BABY_KILL_QUOTA)
        {
            MetKilledBabyQuota?.Invoke();
        }
    }

    public void Flip()
    {
        PlayerFlipped?.Invoke();
    }
}
