using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IPickable
{
    public string itemName;
    public ItemType type;
    public float weight;
    public bool stackable;
    public  int maxStack;
    public int count;
    public void PickUp(Inventory inv)
    {
        gameObject.SetActive(false);
        inv.AddItem(this);
    }

    public override string ToString()
    {
        return $"{count} {itemName}";
    }
    public enum ItemType
    {
        Money,
        Armor,
        NaO
    }
}
