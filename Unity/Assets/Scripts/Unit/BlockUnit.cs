using UnityEngine;
using System.Collections;

public class BlockUnit : Unit
{
    private BlockUnitPart[] _blockUnitParts;

    public override UnitType UnitType => UnitType.Block;

    public override bool Damageable => true;

    protected override void Awake()
    {
        base.Awake();
        _blockUnitParts = GetComponentsInChildren<BlockUnitPart>();
    }

    public void AddExplosionForce(float explosionForce, Vector2 explosionPosition, float explosionRadius, float upwardsModifier)
    {
        foreach(var blockUnitPart in _blockUnitParts)
        {
            blockUnitPart.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier);
        }
    }
}
