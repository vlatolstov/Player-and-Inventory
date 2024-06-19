using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private GameObject _inventoryCanvas;
    [SerializeField] private int _inventorySlotsCount = 56;

    private GameObject _inventoryWindow;
    private TextMeshProUGUI _moneyText;
    private TextMeshProUGUI _weightText;
    private GridLayoutGroup _stashObjRef;

    private bool _isOpened;

    private List<InventorySlot> _inventorySlots;
    private Inventory _playerInventory;

    private void Start()
    {
        _isOpened = _inventoryCanvas.activeInHierarchy;
        _inventoryWindow = _inventoryCanvas.transform.Find("InventoryWindow").gameObject;
        _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        _moneyText = _inventoryWindow.transform.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        _weightText = _inventoryWindow.transform.Find("WeightText").GetComponent<TextMeshProUGUI>();
        _stashObjRef = _inventoryWindow.transform.Find("Stash").GetComponent<GridLayoutGroup>();

        _inventorySlots = new(_inventorySlotsCount);
        InitializeInventoryUI();
    }
    private void InitializeInventoryUI()
    {
        for (int i = 0; i < _inventorySlotsCount; i++)
        {
            var slotObj = Instantiate(_slotPrefab, _stashObjRef.transform);
            var slot = slotObj.GetComponent<InventorySlot>();
            _inventorySlots.Add(slot);
        }

        _playerInventory.ItemAdded += OnItemAdded;
    }

    public void ShowInventory()
    {
        _isOpened = !_isOpened;
        _inventoryCanvas.SetActive(_isOpened);
        if (_isOpened) Cursor.lockState = CursorLockMode.Confined;
        else Cursor.lockState = CursorLockMode.Locked;
    }

    public void ChangeMoneyUI(int value)
    {
        _moneyText.text = value.ToString();
    }

    public void ChangeWeightUI(float curValue, float maxValue)
    {
        _weightText.text = $"Weight: {curValue}/{maxValue}";
    }

    private void OnItemAdded(AbstractItemInfo item, int count)
    {
        InventorySlot emptySlot = null;
        foreach (var slot in _inventorySlots)
        {
            if (slot.ItemInfo == null)
            {
                emptySlot = slot;
                break;
            }
        }
        emptySlot.SetItem(item, count);
    }
}
