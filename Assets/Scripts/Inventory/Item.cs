using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IPickable
{
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
}
