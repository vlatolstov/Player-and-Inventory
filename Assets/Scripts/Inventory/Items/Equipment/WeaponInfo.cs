using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 3)]
public class WeaponInfo : EquipmentInfo
{
    [SerializeField] private int _minDamage;
    [SerializeField] private int _maxDamage;
    [SerializeField] private int _attackSpeed;
    [SerializeField] private bool _twoHanded;
}
