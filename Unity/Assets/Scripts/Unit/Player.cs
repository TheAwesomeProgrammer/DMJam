using UnityEngine;
using System.Collections;

public class Player : Unit
{
    private const string FIRE_INPUT = "PlayerFire";

    [SerializeField]
    private PlayerWeapons _playerWeapons;

    [SerializeField]
    private PlayerMovement _playerMovement;

    public override UnitType UnitType => UnitType.Player;

    public override bool Damageable => false;
    public PlayerMovement PlayerMovement => _playerMovement;

    protected override void Update()
    {
        base.Update();
        if (Input.GetButtonDown(FIRE_INPUT))
        {
            Weapon currentWeapon = _playerWeapons.CurrentWeapon;
            currentWeapon.Fire();
        }
    }
}
