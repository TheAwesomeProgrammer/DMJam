using UnityEngine;
using System.Collections;

public class Bazooka : Weapon
{
    private const string CHARGE_INPUT_NAME = "PlayerCharge";

    private PlayerMovement _playerMovement;

    [SerializeField]
    private float _upwardsModifier;

    [SerializeField]
    private float _recoilForce;

    [SerializeField]
    private float _minExplosionRadius = 2;

    [SerializeField]
    private float _maxExplosionRadius = 4;

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

    private void Awake()
    {
        _playerMovement = _player.PlayerMovement;
    }

    protected override void FireWeapon()
    {
        Time.timeScale = _slowMotionScale;
        _charger.Charge(_chargeTime, CHARGE_INPUT_NAME, FireBazookaShot);        
    }

    private void FireBazookaShot(float chargeProcentAmount)
    {
        _playerMovement.AddRecoil(_recoilForce, GetMouseDirection());
        Time.timeScale = 1;
        BulletData bulletData = (BulletData)_bulletDataTemplate.Clone();
        Bullet spawnedBullet = Instantiate(_bulletPrefab, _spawnTransform.position, Quaternion.identity);
        bulletData.Direction = GetMouseDirection();
        bulletData.UnitDealingDamage = _player;
        bulletData.ExplosionForce = _minExplosionForce + Mathf.Max(0, (_maxExplosionForce - _minExplosionForce) * chargeProcentAmount);
        bulletData.ExplosionRadius = _minExplosionRadius + Mathf.Max(0, (_maxExplosionRadius - _minExplosionRadius) * chargeProcentAmount);
        bulletData.ExplosionUpwardsModifier = _upwardsModifier;
        spawnedBullet.Init(bulletData);
    }

    private Vector2 GetMouseDirection()
    {
        return (Camera.main.ScreenPointToRay(Input.mousePosition).origin - _spawnTransform.position).normalized;
    }

}
