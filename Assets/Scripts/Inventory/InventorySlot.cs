using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Image _image;
    private TextMeshProUGUI _countText;

    private AbstractItemInfo _itemInfo;
    public AbstractItemInfo ItemInfo => _itemInfo;

    private void Awake()
    {
        _image = transform.Find("ContentImage").GetComponent<Image>();
        _countText = transform.Find("CountText").GetComponent<TextMeshProUGUI>();
    }
    public void SetItem(AbstractItemInfo item, int count)
    {
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

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
