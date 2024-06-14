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

    public static void ShowDatabase() => Debug.Log($"Database contains {m_itemDatabase.Count} items:\n" + String.Join("\n", m_itemDatabase));

}
