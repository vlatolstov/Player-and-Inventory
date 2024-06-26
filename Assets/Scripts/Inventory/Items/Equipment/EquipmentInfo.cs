using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentInfo : AbstractItemInfo
{
    [SerializeField] private EquipmentType _place;
    public EquipmentType Place => _place;
}
