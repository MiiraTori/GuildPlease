using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 冒険者の一覧を管理するコンポーネント（IDで取得可能）
/// </summary>
public class AdventurerListComponent : MonoBehaviour
{
    private Dictionary<string, AdventurerData> adventurerMap = new Dictionary<string, AdventurerData>();

    [SerializeField]
    public List<AdventurerDataSO> adventurerSOList = new List<AdventurerDataSO>();


    private void Awake()
    {
        adventurerMap = new Dictionary<string, AdventurerData>();

        foreach (var adventurer in adventurerSOList)
        {
            if (!string.IsNullOrEmpty(adventurer.adventurerId))
            {
                adventurerMap.Add(adventurer.adventurerId, adventurer.CreateAdventurerInstance());
            }
            else
            {
                Debug.LogWarning($"⛔️ 冒険者IDが空です: {adventurer.name}");
            }
        }
    }



    public List<KeyValuePair<string, AdventurerData>> GetAdventurerMapList()
    {
        return adventurerMap.ToList();
    }


    /// <summary>
    /// 現在登録されている全冒険者を取得
    /// </summary>
    public List<AdventurerData> GetAllAdventurers()
    {
        return adventurerMap.Values.ToList();
    }
    
}