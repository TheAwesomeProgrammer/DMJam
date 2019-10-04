using UnityEngine;
using System.Collections;
using System;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    private int _maxAmmo;

    [SerializeField]
    private int _startAmmo;

    [SerializeField]
    private GameObject _weaponRootGo;

    public abstract WeaponType WeaponType { get; }
    public int Ammo { get; private set; }

    public event Action NoAmmo;

    protected virtual void Start()
    {
        Ammo = _startAmmo;
    }

    public virtual void Fire()
    {
        if (Ammo > 0)
        {
            Ammo--;
            FireWeapon();
        }
        else if (Ammo <= 0)
        {
            NoAmmo?.Invoke();
        }
    }

    protected abstract void FireWeapon();

    public void AddAmmo(int ammoAmount)
    {
        Ammo += ammoAmount;
        if (Ammo > _maxAmmo)
        {
            Ammo = _maxAmmo;
        }
    }

    public void Activate()
    {
        _weaponRootGo.SetActive(true);
    }

    public void Deactivate()
    {
        _weaponRootGo.SetActive(false);
    }
}

