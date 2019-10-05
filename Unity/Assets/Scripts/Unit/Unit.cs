using UnityEngine;
using System.Collections;
using NaughtyAttributes;
using JetBrains.Annotations;

public delegate void KilledUnit(Unit unit);

public abstract class Unit : MonoBehaviour
{
    [SerializeField, ShowIf("IsDamageable")]
    private Life _life;

    [SerializeField]
    protected GameObject _rootGo;

    [SerializeField]
    protected Transform _collisionTransform;

    public abstract UnitType UnitType { get; }

    public abstract bool Damageable { get; }

    public GameObject RootGo => _rootGo;

    public Transform CollisionTransform => _collisionTransform;

    public Life Life => _life;

    public event Died Died;
    public event KilledUnit KilledUnit;

    [UsedImplicitly]
    public bool IsDamageable()
    {
        return Damageable;
    }

    private void Awake()
    {
        _life.Init(Damageable, this);
        _life.Died += Die;
    }

    protected virtual void Update()
    {
        _life.Update();
    }

    public void Die(Unit killedBy)
    {
        Died?.Invoke(killedBy);
        OnDied(killedBy);
        Destroy(_rootGo);
    }

    public virtual void OnKilledUnit(Unit unit)
    {
        KilledUnit?.Invoke(unit);
    }

    protected virtual void OnDied(Unit killedBy)
    {

    }
}

