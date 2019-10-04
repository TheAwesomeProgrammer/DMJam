using UnityEngine;
using System.Collections;

public class Bullet : Unit
{
    private BulletData _bulletData;

    [SerializeField]
    private Transform _bodyTransform;

    [SerializeField]
    private GameObject colliderGo;

    [SerializeField]
    private Vector2 _defaultLookDirection;

    [SerializeField]
    private Explosion _explosionPrefab;

    public override UnitType UnitType => UnitType.Bullet;
    public override bool Damageable => false;

    public void Init(BulletData bulletData)
    {
        _bulletData = bulletData;
        _bodyTransform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(_defaultLookDirection, bulletData.Direction));
        TriggerNotifier triggerNotifier = colliderGo.AddComponent<TriggerNotifier>();
        triggerNotifier.Init(bulletData.TargetUnitTypes);
        triggerNotifier.UnitEntered += OnUnitEntered;
    }

    private void OnUnitEntered(UnitType unitType, Unit unit)
    {
        Explosion explosion = Instantiate(_explosionPrefab, _bodyTransform.position, Quaternion.identity);
        explosion.Init(_bulletData.UnitDealingDamage);
        Destroy(_rootGo);
    }

    private void Update()
    {
        _bodyTransform.Translate(_defaultLookDirection * _bulletData.Speed * Time.deltaTime);
    }
}

