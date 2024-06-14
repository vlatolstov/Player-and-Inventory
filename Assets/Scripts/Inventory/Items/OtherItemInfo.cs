using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Other", menuName = "Other Item", order = 5)]
public class OtherItemInfo : AbstractItemInfo
{
    [SerializeField] private bool _stackable;
    public bool Stackable { get => _stackable; }
    [Range(0, 1000)]
    [SerializeField] private int _maxStack;
    public int MaxStack { get => _maxStack; }
}
