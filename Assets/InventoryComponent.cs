using System.Collections.Generic;
using UnityEngine;
using GameData;

public class InventoryComponent : MonoBehaviour
{
    public List<ItemData> items = new List<ItemData>();

    /// <summary>
    /// アイテムを追加する
    /// </summary>
    public void AddItem(ItemData item)
    {
        if (item != null)
        {
            items.Add(item);
            Debug.Log($"[Inventory] {item.itemName} を追加しました");

            // 追加後に全アイテムを表示
            LogInventory();
        }
    }

    /// <summary>
    /// 現在の所持アイテムをログに出力
    /// </summary>
    public void LogInventory()
    {
        if (items.Count == 0)
        {
            Debug.Log("[Inventory] 所持アイテムなし");
            return;
        }

        string log = "[Inventory] 所持アイテム一覧：\n";
        foreach (var item in items)
        {
            log += $"- {item.itemName} (ID: {item.itemId})\n";
        }
        Debug.Log(log);
    }

    /// <summary>
    /// 全てのアイテムを削除
    /// </summary>
    public void ClearInventory()
    {
        items.Clear();
        Debug.Log("[Inventory] アイテムをすべて削除しました");
    }

    /// <summary>
    /// 特定のアイテムを所持しているか確認
    /// </summary>
    public bool ContainsItem(string itemId)
    {
        return items.Exists(i => i.itemId == itemId);
    }
}