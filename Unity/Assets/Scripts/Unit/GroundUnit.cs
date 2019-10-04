using UnityEngine;
using System.Collections;

public class GroundUnit : Unit
{
    public override UnitType UnitType => UnitType.Ground;

    public override bool Damageable => false;
}
