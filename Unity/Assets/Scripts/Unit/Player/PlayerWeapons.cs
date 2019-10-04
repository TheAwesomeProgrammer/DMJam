using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField]
    private List<Weapon> _weapons;

    public Weapon CurrentWeapon { get; private set; }

    private void Start()
    {
        ActivateWeaponType(WeaponType.Bazooka);
    }

    public Weapon GetWeaponWithType(WeaponType weaponType)
    {
        foreach (var weapon in _weapons)
        {
            if (weapon.WeaponType == weaponType)
            {
                return weapon;
            }
        }

        Debug.LogError("Couldn't find weapon with type " + weaponType);
        return null;
    }

    private void ActivateWeaponType(WeaponType weaponType)
    {
        foreach (var weapon in _weapons)
        {
            if (weapon.WeaponType == weaponType)
            {
                CurrentWeapon = weapon;
                weapon.Activate();
            }
            else
            {
                weapon.Deactivate();
            }
        }
    }

    public void AddAmmo(int ammoAmount)
    {
        CurrentWeapon.AddAmmo(ammoAmount);
    }
}

