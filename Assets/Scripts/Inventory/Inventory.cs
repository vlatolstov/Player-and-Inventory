using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private float _maxWeight = 100;
    private float _curWeight = 0;
    private long _money = 0;
    private List<Item> _items = new();

    //temporary method
    public void ShowInventory()
    {
        Debug.Log($"You have {_items.Count} items. You have {_money} gold. Weight {_curWeight}/{_maxWeight}." +
            $"Item List: {String.Join(", ", _items)}");
    }

    public void AddItem(Item item)
    {
        switch (item.type)
        {
            case Item.ItemType.Money:
                _money += item.count;
                ShowLog();
                break;
            case Item.ItemType.Armor:
                if (CheckWeight())
                {
                    _items.Add(item);
                    _curWeight += item.weight;
                    ShowLog();
                }
                break;
            default:
                Debug.LogError($"{item.type} not implemented!");
                break;
        }

        //pick information
        void ShowLog() => Debug.Log($"Picked {item.count} {item.itemName}{(item.count == 1 ? "" : "'s")}.");

        bool CheckWeight() => _curWeight + item.weight <= _maxWeight;
    }
}
