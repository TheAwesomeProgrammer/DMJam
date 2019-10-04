using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void UnitEntered(UnitType unitType, Unit unit);
public delegate void UnitStayed(UnitType unitType, Unit unit);
public delegate void UnitExited(UnitType unitType, Unit unit);

public class TriggerNotifier : MonoBehaviour
{
    private List<UnitType> _unitsToNotifyOn;

    public event UnitEntered UnitEntered;
    public event UnitStayed UnitStayed;
    public event UnitExited UnitExited;

    public void Init(List<UnitType> unitsToNotifyOn)
    {
        _unitsToNotifyOn = unitsToNotifyOn;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Unit otherUnit = other.GetComponentInParent<Unit>();
        if (otherUnit != null && _unitsToNotifyOn.Contains(otherUnit.UnitType))
        {
            UnitEntered?.Invoke(otherUnit.UnitType, otherUnit);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Unit otherUnit = other.GetComponentInParent<Unit>();
        if (otherUnit != null && _unitsToNotifyOn.Contains(otherUnit.UnitType))
        {
            UnitStayed?.Invoke(otherUnit.UnitType, otherUnit);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Unit otherUnit = other.GetComponentInParent<Unit>();
        if (otherUnit != null && _unitsToNotifyOn.Contains(otherUnit.UnitType))
        {
            UnitExited?.Invoke(otherUnit.UnitType, otherUnit);
        }
    }
}

