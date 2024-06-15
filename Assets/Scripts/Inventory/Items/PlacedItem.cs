using UnityEngine;

public class PlacedItem : MonoBehaviour, IPickable
{
    [SerializeField] private AbstractItemInfo _info;
    public AbstractItemInfo Info => _info;
    [SerializeField] private int _count = 1;
    public int Count => _count;

    public void Initialize(int count) => _count = count;
    public void PickUp(Inventory inv)
    {
        if (inv.AddItem(_info, _count)) gameObject.SetActive(false);
    }
}
