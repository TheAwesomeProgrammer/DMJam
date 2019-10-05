using UnityEngine;
using System.Collections;

public class LoseOnPlayerContact : MonoBehaviour
{
    [SerializeField]
    private GameObject _triggerGo;

    private void Awake()
    {
        TriggerNotifier triggerNotifier = _triggerGo.AddComponent<TriggerNotifier>();
        triggerNotifier.Init(new System.Collections.Generic.List<UnitType>() { UnitType.Player });
        triggerNotifier.UnitEntered += UnitEntered;
    }

    private void UnitEntered(UnitType unitType, Unit unit)
    {
        Game.Instance.
    }
}
