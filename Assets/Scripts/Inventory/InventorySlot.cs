using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image _image;
    public Text _countText;

    private AbstractItemInfo _itemInfo;

    private void Awake()
    {
        _image = transform.Find("ContentImage").GetComponent<Image>();
    }
    public void SetItem(AbstractItemInfo item, int count)
    {
        _itemInfo = item;
        _image.sprite = item.Sprite;
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
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
