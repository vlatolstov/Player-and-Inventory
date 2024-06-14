using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject m_inventoryWindow;
    private float _maxWeight = 100;
    private float _curWeight = 0;
    private long _money = 0;
    private List<(AbstractItemInfo, int)> _items = new();
    private bool m_isOpened = false;


    //temporary method
    public void ShowInventory()
    {
        Debug.Log($"You have {_items.Count} items. You have {_money} gold. Weight {_curWeight}/{_maxWeight}." +
            $"Item List: {String.Join(", ", _items)}");
        m_isOpened = !m_isOpened;
        m_inventoryWindow.SetActive(m_isOpened);
    }

    public bool AddItem(AbstractItemInfo info, int count)
    {
        float weight = info.Weight * count;
        if (_curWeight + weight <= _maxWeight)
        {
            _curWeight += weight;
            switch (info.Type)
            {
                case ItemType.Money:
                    _money += count;
                    Debug.Log($"Picked {count} {info.ItemName}{(count == 1 ? "" : "s")}.");
                    break;
                case ItemType.Armor:
                    ArmorInfo armor = info as ArmorInfo;
                    _items.Add((armor, count));
                    Debug.Log($"Picked {armor.ItemName} with {armor.ArmorClass} AC and {armor.ArmorType} type.");
                    break;
                case ItemType.Miscellaneous:
                    OtherItemInfo item = info as OtherItemInfo;

                    if (item.Stackable)
                    {
                        
                    }
                    else
                    {
                        for (int i = 0; i < count; i++)
                        {
                            _items.Add((item, 1));
                        }
                    }
                    Debug.Log($"Picked {count} {info.ItemName}{(count == 1 ? "" : "s")}.");
                    break;
                default:
                    Debug.LogError($"{info.Type} not implemented!");
                    return false;
            }
            return true;
        }
        else
        {
            Debug.LogWarning("Cant pickup! Too heavy!");
            return false;
        }
    }
}
