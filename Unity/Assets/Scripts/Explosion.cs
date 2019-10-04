using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Explosion : MonoBehaviour
{
    private const int GARANTEED_DAMAGE_AMOUNT = int.MaxValue;

    private Unit _unitDealingDamage;

    [SerializeField]
    private float _explosionForce;

    [SerializeField]
    private Transform _bodyTransform;

    [SerializeField]
    private GameObject _bodyGo;

    [SerializeField]
    private GrowCircleCollider _growCircleCollider;

    private List<UnitType> _typesCanApplyExplosionForceTo = new List<UnitType>()
    {
        UnitType.Player
    };

    private List<UnitType> _typesExplosionCanDamage = new List<UnitType>()
    {
        UnitType.Block
    };

    public void Init(Unit unitDealingDamage)
    {
        _unitDealingDamage = unitDealingDamage;
        TriggerNotifier triggerNotifier = _bodyGo.AddComponent<TriggerNotifier>();

        List<UnitType> unitTypesToBeNotifiedOff = new List<UnitType>();
        unitTypesToBeNotifiedOff.AddRange(_typesCanApplyExplosionForceTo);
        unitTypesToBeNotifiedOff.AddRange(_typesExplosionCanDamage);
        triggerNotifier.Init(unitTypesToBeNotifiedOff);

        triggerNotifier.UnitEntered += UnitEntered;
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
        player.PlayerMovement.AddExplosionForce(_explosionForce, _bodyTransform.position, _growCircleCollider.GrownRadius);
    }

    private void UnitToApplyDamageToEntered(UnitType unitType, Unit unit)
    {
        unit.Life.TakeDamage(GARANTEED_DAMAGE_AMOUNT, _unitDealingDamage);
    }
}
