using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Explosion : MonoBehaviour
{
    private const int GARANTEED_DAMAGE_AMOUNT = int.MaxValue;

    private Unit _unitDealingDamage;
    private float _explosionForce;
    private float _explosionRadius;

    [SerializeField]
    private float _aliveTime;

    [SerializeField]
    private float _forceApplyTime;

    [SerializeField]
    private CircleCollider2D _circleCollider2D;

    [SerializeField]
    private Transform _bodyTransform;

    [SerializeField]
    private Transform _rootTransform;

    [SerializeField]
    private GameObject _bodyGo;

    [SerializeField]
    private GameObject _rootGo;

    [SerializeField]
    private GameObject _backgroundIndicatorGo;

    private List<UnitType> _typesCanApplyExplosionForceTo = new List<UnitType>()
    {
        UnitType.Player
    };

    private List<UnitType> _typesExplosionCanDamage = new List<UnitType>()
    {
        UnitType.Block,
        UnitType.Baby
    };

    public void Init(float explosionForce, float explosionRadius, Unit unitDealingDamage)
    {
        _explosionForce = explosionForce;
        _explosionRadius = explosionRadius;
        _unitDealingDamage = unitDealingDamage;
        _rootTransform.localScale = new Vector3(explosionRadius, explosionRadius);
        _circleCollider2D.enabled = true; 
        TriggerNotifier triggerNotifier = _bodyGo.AddComponent<TriggerNotifier>();

        List<UnitType> unitTypesToBeNotifiedOff = new List<UnitType>();
        unitTypesToBeNotifiedOff.AddRange(_typesCanApplyExplosionForceTo);
        unitTypesToBeNotifiedOff.AddRange(_typesExplosionCanDamage);
        triggerNotifier.Init(unitTypesToBeNotifiedOff);

        triggerNotifier.UnitEntered += UnitEntered;
        LeanTween.alpha(_backgroundIndicatorGo, 0, _aliveTime);
        LeanTween.delayedCall(_forceApplyTime, () => _circleCollider2D.enabled = false);
        LeanTween.delayedCall(_aliveTime, () => Destroy(_rootGo));
    }

    private void UnitEntered(UnitType unitType, Unit unit)
    {
        if (_typesCanApplyExplosionForceTo.Contains(unitType))
        {
            UnitToApplyForceToEntered(unitType, unit);
        }
        else if (_typesExplosionCanDamage.Contains(unitType))
        {
            UnitToApplyDamageToEntered(unitType, unit);
        }          
    }

    private void UnitToApplyForceToEntered(UnitType unitType, Unit unit)
    {
        Player player = unit as Player;
        player.PlayerMovement.AddExplosionForce(_explosionForce, _bodyTransform.position, _explosionRadius);
    }

    private void UnitToApplyDamageToEntered(UnitType unitType, Unit unit)
    {
        unit.Life.TakeDamage(GARANTEED_DAMAGE_AMOUNT, _unitDealingDamage);
    }
}
