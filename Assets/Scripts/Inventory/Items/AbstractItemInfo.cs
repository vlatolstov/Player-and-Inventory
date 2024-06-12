using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractItemInfo : ScriptableObject
{
    [SerializeField] private int _id;
    public int ID => _id;
    [SerializeField] private ItemType _type;
    public ItemType Type { get => _type; }
    [SerializeField] private string _name;
    public string ItemName { get => _name; }
    [TextArea(3, 10)]
    [SerializeField] private string _description;
    public string Description { get => _description; }
    [Space]
    [SerializeField] private float _weight;
    public float Weight { get => _weight; }
    [Space]
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite { get => _sprite; }
}
