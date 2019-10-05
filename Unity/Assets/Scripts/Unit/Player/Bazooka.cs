using UnityEngine;
using System.Collections;

public class Bazooka : Weapon
{
    private const float MIN_EXPLOSION_FORCE = 40;
    private const float MAX_EXPLOSION_FORCE = 70;
    private const float CHARGE_TIME = 2;
    private const float SLOW_MOTION_SCALE = 0.5f;
    private const string CHARGE_INPUT_NAME = "PlayerCharge";

    [SerializeField]
    private Player _player;

    [SerializeField]
    private Charger _charger;

    [SerializeField]
    private BulletData _bulletDataTemplate;

    [SerializeField]
    private Bullet _bulletPrefab;

    [SerializeField]
    private Transform _spawnTransform;

    public override WeaponType WeaponType => WeaponType.Bazooka;

    protected override void FireWeapon()
    {
        Time.timeScale = SLOW_MOTION_SCALE;
        _charger.Charge(CHARGE_TIME, CHARGE_INPUT_NAME, FireBazookaShot);        
    }

    private void FireBazookaShot(float chargeProcentAmount)
    {
        Time.timeScale = 1;
        BulletData bulletData = (BulletData)_bulletDataTemplate.Clone();
        Bullet spawnedBullet = Instantiate(_bulletPrefab, _spawnTransform.position, Quaternion.identity);
        bulletData.Direction = GetMouseDirection();
        bulletData.UnitDealingDamage = _player;
        bulletData.ExplosionForce = MIN_EXPLOSION_FORCE + Mathf.Max(0, (MAX_EXPLOSION_FORCE - MIN_EXPLOSION_FORCE) * chargeProcentAmount);
        spawnedBullet.Init(bulletData);
    }

    private Vector2 GetMouseDirection()
    {
        return (Camera.main.ScreenPointToRay(Input.mousePosition).origin - _spawnTransform.position).normalized;
    }

}
