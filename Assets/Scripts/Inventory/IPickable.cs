using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable
{
    int Count { get; }
    void PickUp(Inventory inv);
}
