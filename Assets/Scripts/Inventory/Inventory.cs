using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryUIManager InventoryUIManager { get => _inventoryUIManager; }
    private InventoryUIManager _inventoryUIManager;

    private float _maxWeight = 150;
    private float _curWeight = 0;
    private int _money = 0;

    public event Action<List<(AbstractItemInfo, int)>> OnInventoryChanged;
    public event Action<string, Sprite> OnPickupItem;

    private List<(AbstractItemInfo, int)> _items = new(56);


    private void Start()
    {
        _inventoryUIManager = FindFirstObjectByType<InventoryUIManager>();
        InitializeInventory();
    }
    private void InitializeInventory()
    {
        InventoryUIManager.InitializeInventoryUI();
        InventoryUIManager.ChangeMoneyUI(_money);
        InventoryUIManager.ChangeWeightUI(_curWeight, _maxWeight);
    }

    public void ChangeMoney(int count)
    {
        _money += count;
        InventoryUIManager.ChangeMoneyUI(_money);
    }
    public bool AddItem(AbstractItemInfo info, int count)
    {
        float weight = info.Weight * count;
        string message;
        if (_curWeight + weight <= _maxWeight)
        {
            _curWeight += weight;
            InventoryUIManager.ChangeWeightUI(_curWeight, _maxWeight);
            switch (info.Type)
            {
                case ItemType.Money:
                    ChangeMoney(count);
                    message = $"Picked {count} {info.ItemName}{(count == 1 ? "" : "s")}.";
                    break;

                case ItemType.Armor:
                    ArmorInfo armor = info as ArmorInfo;
                    AddToThisInventory(armor, 1);
                    message = $"Picked {armor.ItemName}";
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
                    message = $"Picked {count} {info.ItemName}{(count == 1 ? "" : "s")}.";
                    break;
                default:
                    message = $"{info.Type} not implemented!";
                    return false;
            }
            OnInventoryChanged?.Invoke(_items);
            OnPickupItem?.Invoke(message, info.Sprite);
            Debug.Log(message);
            return true;
        }
        else
        {
            message = "Cant pickup! Too heavy!";
            OnPickupItem?.Invoke(message, null);
            Debug.Log(message);
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
