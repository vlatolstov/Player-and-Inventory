using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    private static readonly Dictionary<int, AbstractItemInfo> m_itemDatabase = new();

    private void Start()
    {
        LoadItems();
    }

    private void LoadItems()
    {
        var all = Resources.LoadAll("Items");
        foreach (var obj in all)
        {
            var item = (AbstractItemInfo)obj;
            if (item != null)
            {
                Debug.Log($"Added {item.name}. ID {item.ID}");
                if (!m_itemDatabase.TryAdd(item.ID, item)) Debug.LogError($"{item.ID} already in database. Remove duplicates!");
            }
        }
    }

    public static void ShowDatabase()
    {
        Debug.Log("Database contains:");
        int i = 1;
        foreach (var item in m_itemDatabase.Values)
        {
            Debug.Log($"¹{i++} {item.name}. ID {item.ID}");
        }
    }
}
