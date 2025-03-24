using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemListComponent : MonoBehaviour
{
    [SerializeField]
    private Dictionary<string, ItemData> itemMap = new Dictionary<string, ItemData>();

    [SerializeField]
    public List<ItemDataSO> itemSOList = new List<ItemDataSO>();

    private void Awake()
    {
        itemMap = new Dictionary<string, ItemData>();

        foreach (var item in itemSOList)
        {
            if (!string.IsNullOrEmpty(item.itemId))
            {
                itemMap[item.itemId] = item.CreateItemInstance();
            }
            else
            {
                Debug.LogWarning($"⛔️ アイテムIDが空です: {item.name}");
            }
        }
    }

    // IDで取得
    public ItemData GetItemById(string id)
    {
        if (itemMap.TryGetValue(id, out var data))
        {
            return data;
        }
        Debug.LogWarning($"🧳 アイテムが見つかりません: {id}");
        return null;
    }


    // 全アイテム一覧（値だけ）
    public List<ItemData> GetAllItems()
    {
        return itemMap.Values.ToList();
    }

    // IDとセットの一覧
    public List<KeyValuePair<string, ItemData>> GetItemMapList()
    {
        return itemMap.ToList();
    }
}