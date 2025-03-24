using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] public List<ItemData> items = new List<ItemData>();

    public void AddItem(ItemData item)
    {
        if (item == null) return;

        items.Add(item);
        Debug.Log($"🎒 アイテム追加: {item.itemName}");
    }

    public bool HasItem(string itemId)
    {
        return items.Exists(i => i.itemId == itemId);
    }

    public void RemoveItem(string itemId)
    {
        var found = items.Find(i => i.itemId == itemId);
        if (found != null)
        {
            items.Remove(found);
            Debug.Log($"🗑️ アイテム削除: {found.itemName}");
        }
    }

    public void LogInventory()
    {
        Debug.Log("📦 現在のインベントリ:");
        foreach (var item in items)
        {
            Debug.Log($"- {item.itemName} (ID: {item.itemId})");
        }
    }
}