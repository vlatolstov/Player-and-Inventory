using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHaveInventory 
{
    int Capacity { get; set; }

    void OpenInventory()
    {
    }
}
