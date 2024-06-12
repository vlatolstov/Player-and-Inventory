using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IPickable
{
    public string ItemName { get => _name; }
    [SerializeField] private string _name;
    public ItemType Type { get => _type; }
    [SerializeField] private ItemType _type;
    public float Weight { get => _weight; }
    [SerializeField] private float _weight;
    public bool Stackable { get => _stackable; }
    [SerializeField] private bool _stackable;
    public int MaxStack { get => _maxStack; }
    [SerializeField] private int _maxStack;
    public int Count { get => _count; }
    [SerializeField] private int _count;

    public void PickUp(Inventory inv)
    {
        gameObject.SetActive(false);
        inv.AddItem(this);
    }

    public override string ToString()
    {
        return $"{Count} {ItemName}";
    }
}
