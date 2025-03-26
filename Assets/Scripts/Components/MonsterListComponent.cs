using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterListComponent : MonoBehaviour
{
    [SerializeField]
    private Dictionary<string, MonsterData> monsterMap = new Dictionary<string, MonsterData>();

    [SerializeField]
    public List<MonsterDataSO> monsterSOList = new List<MonsterDataSO>();
 
    private void Awake()
    {
        monsterMap = new Dictionary<string, MonsterData>();

        foreach (var monster in monsterSOList)
        {
            if (!string.IsNullOrEmpty(monster.monsterId))
            {
                monsterMap[monster.monsterId] = monster.CreateMonsterInstance();
            }
            else
            {
                Debug.LogWarning($"👹 モンスターIDが空です: {monster.name}");
            }
        }
    }

    // IDで取得
    public MonsterData GetMonsterById(string id)
    {
        if (monsterMap.TryGetValue(id, out var data))
        {
            return data;
        }
        Debug.LogWarning($"🐉 モンスターが見つかりません: {id}");
        return null;
    }

    // 全モンスター一覧（値だけ）
    public List<MonsterData> GetAllMonsters()
    {
        return monsterMap.Values.ToList();
    }

    // IDとセットの一覧
    public List<KeyValuePair<string, MonsterData>> GetMonsterMapList()
    {
        return monsterMap.ToList();
    }
}
