using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableItem : ScriptableObject
{
    [SerializeField] private int _id;
    public int ID => _id;
    [SerializeField] private ItemType _type;
    public ItemType Type { get => _type; }
    [SerializeField] private string _name;
    public string ItemName { get => _name; }
    [SerializeField] private string _description;
    public string Description { get => _description; }
    [Space]
    [SerializeField] private float _weight;
    public float Weight { get => _weight; }
    [SerializeField] private bool _stackable;
    public bool Stackable { get => _stackable; }
    [SerializeField] private int _maxStack;
    public int MaxStack { get => _maxStack; }
    [SerializeField] private int _count;
    public int Count { get => _count; }
}
