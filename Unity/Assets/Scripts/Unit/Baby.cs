using UnityEngine;
using System.Collections;

public class Baby : Unit
{
    public override UnitType UnitType => UnitType.Baby;
    public override bool Damageable => true;
}
