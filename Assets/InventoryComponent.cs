using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField]
    public List<ItemData> items = new List<ItemData>();

    /// <summary>
    /// アイテムをインベントリに追加
    /// </summary>
    public void AddItem(ItemData item)
    {
        items.Add(item);
        Debug.Log($"📦 アイテム追加: {item.itemName}");
    }

    /// <summary>
    /// 指定されたIDのアイテムを所持しているか確認
    /// </summary>
    public bool HasItem(string itemId)
    {
        return items.Exists(item => item.itemId == itemId);
    }

    /// <summary>
    /// インベントリの内容をログに表示（デバッグ用）
    /// </summary>
    public void LogInventory()
    {
        Debug.Log("📦 [インベントリ一覧]");
        foreach (var item in items)
        {
            Debug.Log($"- {item.itemName} ({item.itemId})");
        }
    }
}