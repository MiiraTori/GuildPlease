using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FieldAreaState
{
    public string areaId;
    public Dictionary<string, int> monsterCounts = new();
    public string currentBossId;

    public void Initialize(FieldAreaDataSO data, int initialCount = 0)
    {
        areaId = data.areaId;
        monsterCounts.Clear();
        foreach (var monster in data.possibleBosses)
        {
            if (!monsterCounts.ContainsKey(monster.monsterData.monsterId))
            {
                monsterCounts.Add(monster.monsterData.monsterId, initialCount);
            }
        }

        currentBossId = null;
    }

    public void IncreaseMonsters(FieldAreaDataSO data, int maxCountPerMonster = 5)
    {
        foreach (var monster in data.possibleBosses)
        {
            string id = monster.monsterData.monsterId;
            if (monsterCounts.ContainsKey(id) && monsterCounts[id] < maxCountPerMonster)
            {
                monsterCounts[id]++;
            }
        }
    }

    public void OnMonsterDefeated(string monsterId)
    {
        if (monsterCounts.ContainsKey(monsterId) && monsterCounts[monsterId] > 0)
        {
            monsterCounts[monsterId]--;
        }

        if (currentBossId == monsterId)
        {
            Debug.Log($"🗡️ ボス {monsterId} を討伐 → currentBossId を null にします");
            currentBossId = null;
        }
    }

    public string GetStatusString()
    {
        string monsterInfo = "";
        foreach (var pair in monsterCounts)
        {
            monsterInfo += $"{pair.Key}:{pair.Value} ";
        }

        string bossInfo = string.IsNullOrEmpty(currentBossId) ? "なし" : currentBossId;
        return $"[エリア:{areaId}] モンスター:{monsterInfo} / ボス:{bossInfo}";
    }
}