using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Equipment", order = 4)]
public abstract class EquipmentInfo : AbstractItemInfo
{
    [SerializeField] private EquipmentType _place;
    public EquipmentType Place { get => _place; }
}
