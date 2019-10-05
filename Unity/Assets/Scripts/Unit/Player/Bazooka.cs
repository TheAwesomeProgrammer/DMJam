using UnityEngine;
using System.Collections;

public class Bazooka : Weapon
{
    private const string CHARGE_INPUT_NAME = "PlayerCharge";

    [SerializeField]
    private float _minExplosionForce = 40;

    [SerializeField]
    private float _maxExplosionForce = 70;

    [SerializeField]
    private float _chargeTime = 2;

    [SerializeField]
    private float _slowMotionScale = 0.5f;

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
        Time.timeScale = _slowMotionScale;
        _charger.Charge(_chargeTime, CHARGE_INPUT_NAME, FireBazookaShot);        
    }

    private void FireBazookaShot(float chargeProcentAmount)
    {
        Time.timeScale = 1;
        BulletData bulletData = (BulletData)_bulletDataTemplate.Clone();
        Bullet spawnedBullet = Instantiate(_bulletPrefab, _spawnTransform.position, Quaternion.identity);
        bulletData.Direction = GetMouseDirection();
        bulletData.UnitDealingDamage = _player;
        bulletData.ExplosionForce = _minExplosionForce + Mathf.Max(0, (_maxExplosionForce - _minExplosionForce) * chargeProcentAmount);
        spawnedBullet.Init(bulletData);
    }

    private Vector2 GetMouseDirection()
    {
        return (Camera.main.ScreenPointToRay(Input.mousePosition).origin - _spawnTransform.position).normalized;
    }

}
