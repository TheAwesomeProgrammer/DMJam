using UnityEngine;
using System.Collections;

public class BlockUnit : Unit
{
    public override UnitType UnitType => UnitType.Block;

    public override bool Damageable => true;
}
