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

    public void InitializeInventoryUI()
    {
        _isOpened = _inventoryCanvas.activeInHierarchy;
        _inventoryWindow = _inventoryCanvas.transform.Find("InventoryWindow").gameObject;
        _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        _moneyText = _inventoryWindow.transform.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        _weightText = _inventoryWindow.transform.Find("WeightText").GetComponent<TextMeshProUGUI>();
        _stashObjRef = _inventoryWindow.transform.Find("Stash").GetComponent<GridLayoutGroup>();

        _inventorySlots = new(_inventorySlotsCount);

        for (int i = 0; i < _inventorySlotsCount; i++)
        {
            var slotObj = Instantiate(_slotPrefab, _stashObjRef.transform);
            var slot = slotObj.GetComponent<InventorySlot>();
            slot.EPointerEnter += ShowItemInfo;
            slot.EPointerExit += HideItemInfo;
            _inventorySlots.Add(slot);
        }

        _playerInventory.OnInventoryChanged += RefreshStashUI;
    }

    public void ShowInventory()
    {
        _isOpened = !_isOpened;
        _inventoryCanvas.SetActive(_isOpened);
        if (_isOpened) Cursor.lockState = CursorLockMode.Confined;
        else Cursor.lockState = CursorLockMode.Locked;
    }

    private void ShowItemInfo(AbstractItemInfo info, Transform transform)
    {
        Debug.Log(info.GetType() + transform.gameObject.ToString());
    }
    private void HideItemInfo()
    {
        Debug.Log("Exit");
    }
    public void ChangeMoneyUI(int value)
    {
        _moneyText.text = value.ToString();
    }

    public void ChangeWeightUI(float curValue, float maxValue)
    {
        _weightText.text = $"Weight: {curValue}/{maxValue}";
    }

    private void RefreshStashUI(List<(AbstractItemInfo, int)> items)
    {
        int i = 0;
        for (; i < items.Count;i++)
        {
            _inventorySlots[i].SetItem(items[i].Item1, items[i].Item2);
        }
        for (; i < _inventorySlots.Count; i++)
        {
            _inventorySlots[i].ClearSlot();
        }
    }
}
