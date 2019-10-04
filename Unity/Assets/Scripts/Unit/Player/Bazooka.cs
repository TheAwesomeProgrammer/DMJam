using UnityEngine;
using System.Collections;

public class Bazooka : Weapon
{
    [SerializeField]
    private Player _player;

    [SerializeField]
    private BulletData _bulletDataTemplate;

    [SerializeField]
    private Bullet _bulletPrefab;

    [SerializeField]
    private Transform _spawnTransform;

    public override WeaponType WeaponType => WeaponType.Bazooka;

    protected override void FireWeapon()
    {
        BulletData bulletData = (BulletData)_bulletDataTemplate.Clone();
        Bullet spawnedBullet = Instantiate(_bulletPrefab, _spawnTransform.position, Quaternion.identity);
        bulletData.Direction = GetMouseDirection();
        bulletData.UnitDealingDamage = _player;
        spawnedBullet.Init(bulletData);
    }

    private Vector2 GetMouseDirection()
    {
        return (Camera.main.ScreenPointToRay(Input.mousePosition).origin - _spawnTransform.position).normalized;
    }

}
