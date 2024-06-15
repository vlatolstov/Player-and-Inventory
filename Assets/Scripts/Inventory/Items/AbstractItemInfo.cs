using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public abstract class AbstractItemInfo : ScriptableObject
{
    [SerializeField] private int _id;
    public int ID => _id;
    [SerializeField] private ItemType _type;
    public ItemType Type => _type;
    [SerializeField] private string _name;
    public string ItemName => _name;
    [TextArea(3, 10)]
    [SerializeField] private string _description;
    public string Description => _description;
    [Space]
    [SerializeField] private float _weight;
    public float Weight => _weight;
    [Space]
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;
    [SerializeField] private GameObject _placeableGO;
    public GameObject PlaceableGameObject => _placeableGO;
}
