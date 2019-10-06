using UnityEngine;
using System.Collections;

public class StructureUnit : Unit
{
    [SerializeField]
    private UnitType _unitType;

    [SerializeField]
    private bool _destructable;

    public override UnitType UnitType => _unitType;

    public override bool Damageable => _destructable;
}
