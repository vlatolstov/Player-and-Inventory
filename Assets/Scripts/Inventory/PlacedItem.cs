using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedItem : MonoBehaviour, IPickable
{
    [SerializeField] private AbstractItemInfo _info;
    public AbstractItemInfo Info => _info;

    public PlacedItem(AbstractItemInfo info) => _info = info;
    public void PickUp(Inventory inv)
    {
        gameObject.SetActive(false);
        inv.AddItem(_info);
    }
}
