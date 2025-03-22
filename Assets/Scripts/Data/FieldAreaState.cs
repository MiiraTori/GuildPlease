using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FieldAreaState
{
    public string areaId;

    /// <summary>通常モンスターの現在数（monsterId → count）</summary>
    public Dictionary<string, int> monsterCounts = new Dictionary<string, int>();

    /// <summary>現在出現中のボスID（いなければ null）</summary>
    public string currentBossId = null;

    /// <summary>エリアごとの初期化（外部から一括登録）</summary>
    public void Initialize(FieldAreaDataSO areaData, int initialCount = 1)
    {
        monsterCounts.Clear();

        foreach (var spawn in areaData.encounterMonsters)
        {
            string id = spawn.monster.monsterId;
            monsterCounts[id] = Mathf.Max(0, initialCount);
        }

        currentBossId = null;
    }

    /// <summary>最大数までモンスターを増加させる（ボスがいないときのみ）</summary>
    public void IncreaseMonsters(FieldAreaDataSO areaData, int maxCountPerMonster = 5)
    {
        if (!string.IsNullOrEmpty(currentBossId)) return;

        foreach (var spawn in areaData.encounterMonsters)
        {
            string id = spawn.monster.monsterId;
            if (!monsterCounts.ContainsKey(id))
                monsterCounts[id] = 0;

            if (monsterCounts[id] < maxCountPerMonster)
                monsterCounts[id]++;
        }
    }

    /// <summary>モンスターが討伐されたときに呼ばれる</summary>
    public void OnMonsterDefeated(string monsterId)
    {
        if (monsterCounts.ContainsKey(monsterId) && monsterCounts[monsterId] > 0)
        {
            monsterCounts[monsterId]--;
        }

        if (currentBossId == monsterId)
        {
            currentBossId = null;
        }
    }

    /// <summary>現在のモンスター数をデバッグ出力</summary>
    public void LogStatus()
    {
        Debug.Log($"[Area: {areaId}] Boss: {currentBossId ?? "None"}");
        foreach (var kv in monsterCounts)
        {
            Debug.Log($" - {kv.Key}: {kv.Value}体");
        }
    }
}