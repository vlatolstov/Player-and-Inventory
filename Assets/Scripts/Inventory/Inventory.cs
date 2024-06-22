using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryUIManager UIManager { get => _uIManager; }
    private InventoryUIManager _uIManager;

    private float _maxWeight = 150;
    private float _curWeight = 0;
    private int _money = 0;

    public event Action<List<(AbstractItemInfo, int)>> OnInventoryChanged;

    private List<(AbstractItemInfo, int)> _items = new(56);


    private void Start()
    {
        _uIManager = FindFirstObjectByType<InventoryUIManager>();
        InitializeInventory();
    }
    private void InitializeInventory()
    {
        UIManager.InitializeInventoryUI();
        UIManager.ChangeMoneyUI(_money);
        UIManager.ChangeWeightUI(_curWeight, _maxWeight);
    }


    /// <summary>
    /// Changes money by count and updates UI representation.
    /// </summary>
    /// <param name="count">Negative value if decrease needed.</param>
    public void ChangeMoney(int count)
    {
        _money += count;
        UIManager.ChangeMoneyUI(_money);
    }
    public bool AddItem(AbstractItemInfo info, int count)
    {
        float weight = info.Weight * count;
        if (_curWeight + weight <= _maxWeight)
        {
            _curWeight += weight;
            UIManager.ChangeWeightUI(_curWeight, _maxWeight);
            switch (info.Type)
            {
                case ItemType.Money:
                    ChangeMoney(count);
                    Debug.Log($"Picked {count} {info.ItemName}{(count == 1 ? "" : "s")}.");
                    break;

                case ItemType.Armor:
                    ArmorInfo armor = info as ArmorInfo;
                    AddToThisInventory(armor, 1);
                    Debug.Log($"Picked {armor.ItemName} with {armor.ArmorClass} AC and {armor.ArmorType} type.");
                    break;

                case ItemType.Miscellaneous:
                    OtherItemInfo item = info as OtherItemInfo;
                    if (item.Stackable)
                    {
                        FillUpInventoryWithSameItem(count, item);
                    }
                    else
                    {
                        for (int i = 0; i < count; i++)
                        {
                            AddToThisInventory(item, 1);
                        }
                    }
                    Debug.Log($"Picked {count} {info.ItemName}{(count == 1 ? "" : "s")}.");
                    break;

                default:
                    Debug.LogError($"{info.Type} not implemented!");
                    return false;
            }
            OnInventoryChanged?.Invoke(_items);
            return true;
        }
        else
        {
            Debug.LogWarning("Cant pickup! Too heavy!");
            return false;
        }
    }
    private void AddToThisInventory(AbstractItemInfo info, int count)
    {
        _items.Add((info, count));
    }
    private void FillUpInventoryWithSameItem(int count, OtherItemInfo type)
    {
        var selected = HaveSameIDAndAppropriateCount(type);
        if (selected.Any())
        {
            foreach (var elem in selected)
            {
                int canFit = type.MaxStack - elem.Item1;
                if (canFit >= count)
                {
                    _items[elem.Item2] = (type, count + elem.Item1);
                    return;
                }
                else
                {
                    count -= canFit;
                    _items[elem.Item2] = (type, canFit + elem.Item1);
                }
            }
            if (count > 0)
            {
                int n = count / type.MaxStack;
                for (int i = 0; i < n; i++)
                {
                    AddToThisInventory(type, type.MaxStack);
                }
                int mod = count % type.MaxStack;
                if (mod > 0) AddToThisInventory(type, mod);
            }
        }
        else
        {
            int n = count / type.MaxStack;
            for (int i = 0; i < n; i++)
            {
                AddToThisInventory(type, type.MaxStack);
            }
            int mod = count % type.MaxStack;
            if (mod > 0) AddToThisInventory(type, mod);
        }
    }
    private IList<(int, int)> HaveSameIDAndAppropriateCount(OtherItemInfo item)
    {
        return _items
            .Select((kvp, index) => (kvp.Item1.ID, count: kvp.Item2, index))
            .Where(triple => triple.ID == item.ID && triple.count < item.MaxStack)
            .Select(triple => (triple.count, triple.index))
            .ToList();
    }
}
