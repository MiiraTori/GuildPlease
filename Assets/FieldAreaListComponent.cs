using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// フィールドエリアの一覧を管理するコンポーネント（IDで取得可能）
/// </summary>
public class FieldAreaListComponent : MonoBehaviour
{
    private Dictionary<string, FieldAreaDataSO> fieldAreaMap = new Dictionary<string, FieldAreaDataSO>();

    [SerializeField]
    public List<FieldAreaDataSO> fieldAreaSOList = new List<FieldAreaDataSO>();

    private void Awake()
    {
        fieldAreaMap = new Dictionary<string, FieldAreaDataSO>();

        foreach (var area in fieldAreaSOList)
        {
            if (!string.IsNullOrEmpty(area.areaId))
            {
                fieldAreaMap[area.areaId] = area;
            }
            else
            {
                Debug.LogWarning($"⛔️ エリアIDが空です: {area.name}");
            }
        }
    }

    /// <summary>
    /// IDからフィールドエリアを取得
    /// </summary>
    public FieldAreaDataSO GetAreaById(string id)
    {
        if (fieldAreaMap.TryGetValue(id, out var data))
        {
            return data;
        }
        Debug.LogWarning($"📍 フィールドエリアが見つかりません: {id}");
        return null;
    }

    /// <summary>
    /// 全フィールドエリアのリストを取得
    /// </summary>
    public List<FieldAreaDataSO> GetAllAreas()
    {
        return fieldAreaMap.Values.ToList();
    }

    /// <summary>
    /// IDとセットで取得（必要な場合）
    /// </summary>
    public List<KeyValuePair<string, FieldAreaDataSO>> GetFieldAreaMapList()
    {
        return fieldAreaMap.ToList();
    }
}