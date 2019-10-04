using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class BulletData : ICloneable
{
    public float Speed;
    public int Damage;
    public List<UnitType> TargetUnitTypes;

    public Unit UnitDealingDamage { get; set; }
    public Vector2 Direction { get; set; }

    public object Clone()
    {
        return MemberwiseClone();
    }
}

