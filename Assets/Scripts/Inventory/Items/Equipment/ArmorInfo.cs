using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Armor", menuName = "Armor", order = 2)]
public class ArmorInfo : EquipmentInfo
{
    [SerializeField] private int _armorClass;
    public int ArmorClass { get => _armorClass; }
    [SerializeField] private ArmorType _armorType;
    public ArmorType ArmorType { get => _armorType; }

}
