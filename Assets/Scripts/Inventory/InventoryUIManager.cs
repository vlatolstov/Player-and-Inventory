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
    [SerializeField] private int _inventorySlotsCount = 72;
    [SerializeField] private Vector2 _infoPanelOffset = new();

    private GameObject _inventoryWindow;
    private TextMeshProUGUI _moneyText;
    private TextMeshProUGUI _weightText;
    private GridLayoutGroup _stashObjRef;

    private GameObject _infoPanel;
    private TextMeshProUGUI _info_Name;
    private TextMeshProUGUI _info_Type;
    private TextMeshProUGUI _info_Stats;
    private TextMeshProUGUI _info_Description;
    private TextMeshProUGUI _info_Count;
    private TextMeshProUGUI _info_Cost;
    private TextMeshProUGUI _info_Weight;

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
        
        _infoPanel = _inventoryWindow.transform.Find("InfoPanel").gameObject;
        _info_Name = _infoPanel.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        _info_Type = _infoPanel.transform.Find("Type").GetComponent<TextMeshProUGUI>();
        _info_Stats = _infoPanel.transform.Find("Stats").GetComponent<TextMeshProUGUI>();
        _info_Description = _infoPanel.transform.Find("Description").GetComponent<TextMeshProUGUI>();
        _info_Cost = _infoPanel.transform.Find("Cost").GetComponent<TextMeshProUGUI>();
        _info_Weight = _infoPanel.transform.Find("Weight").GetComponent<TextMeshProUGUI>();
        _info_Count = _infoPanel.transform.Find("Count").GetComponent<TextMeshProUGUI>();

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

    private void ShowItemInfo(AbstractItemInfo info, Transform slotTransform, int count)
    {
        if (info == null) return;

        _info_Name.text = info.ItemName;
        _info_Type.text = info.Type.ToString();
        _info_Description.text = info.Description;
        _info_Cost.text = $"({info.Cost}) {info.Cost * count}";
        _info_Count.text = $"Count: {count}";
        var totalWeight = info.Weight * count;
        _info_Weight.text = $"Weight:({info.Weight}) {totalWeight:F2}";
        //дописать stats

        _infoPanel.transform.position = new Vector3(
            slotTransform.position.x + _infoPanelOffset.x, 
            slotTransform.position.y + _infoPanelOffset.x,
            0);

        _infoPanel.SetActive(true);
    }
    private void HideItemInfo()
    {
        _infoPanel.gameObject.SetActive(false);
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
