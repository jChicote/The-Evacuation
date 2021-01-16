using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Settings/Weapon Setting")]
public class WeaponSettings : ScriptableObject
{
    [Header("Weapons")]
    public List<WeaponInfo> turrentWeapons;
    public List<WeaponInfo> laserWeapon;
    public List<WeaponInfo> launcherWeapons;
}

/*
 *  Note:
 *  When storing unlcoked or purchased weapons / upgrades they must be stored in single lists filtered out by the weapon type attached to it.
 *  
 */