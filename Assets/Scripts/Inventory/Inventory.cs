using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject m_inventoryWindow;
    private float _maxWeight = 100;
    private float _curWeight = 0;
    private long _money = 0;
    private List<AbstractItemInfo> _items = new();
    private bool m_isOpened = false;
    

    //temporary method
    public void ShowInventory()
    {
        Debug.Log($"You have {_items.Count} items. You have {_money} gold. Weight {_curWeight}/{_maxWeight}." +
            $"Item List: {String.Join(", ", _items)}");
        m_isOpened = !m_isOpened;
        m_inventoryWindow.SetActive(m_isOpened);
    }

    public void AddItem(AbstractItemInfo item)
    {
        switch (item.Type)
        {
            case ItemType.Money:
                _money += item.Count;
                ShowLog();
                break;
            case ItemType.Armor:
                if (CheckWeight())
                {
                    _items.Add(item);
                    _curWeight += item.Weight;
                    ShowLog();
                }
                break;
            default:
                Debug.LogError($"{item.Type} not implemented!");
                break;
        }

        void ShowLog() => Debug.Log($"Picked {item.Count} {item.ItemName}{(item.Count == 1 ? "" : "'s")}.");

        bool CheckWeight() => _curWeight + item.Weight <= _maxWeight;
    }
}
