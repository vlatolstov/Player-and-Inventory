using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _countText;
    private int _count;

    private AbstractItemInfo _itemInfo;
    public AbstractItemInfo ItemInfo => _itemInfo;

    public event Action<AbstractItemInfo, int> EPointerEnter;
    public event Action EPointerExit;

    public void SetItem(AbstractItemInfo item, int count)
    {
        _count = count;
        _itemInfo = item;
        _image.sprite = item.Sprite;
        _image.gameObject.SetActive(true);
        _countText.text = item.Type switch
        {
            ItemType.Miscellaneous => count.ToString(),
            _ => string.Empty,
        };
    }

    public void ClearSlot()
    {
        _itemInfo = null;
        _image.sprite = null;
        _countText.text = string.Empty;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_itemInfo != null)
        {
            EPointerEnter?.Invoke(_itemInfo, _count);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_itemInfo != null)
        {
            EPointerExit?.Invoke();
        }
    }
}
