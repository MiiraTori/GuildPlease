using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FieldAreaState
{
    public string areaId;
    public Dictionary<string, int> monsterCounts = new();
    public string currentBossId;

    public void Initialize(FieldAreaDataSO data)
    {
        areaId = data.areaId;
        monsterCounts.Clear();
        foreach (var monster in data.possibleMonsters)
        {
            if (!monsterCounts.ContainsKey(monster.monster.monsterId))
            {
                monsterCounts.Add(monster.monster.monsterId, 0);
            }
        }
        currentBossId = null;
    }

    public void IncreaseMonsters(FieldAreaDataSO data)
    {
        foreach (var monster in data.possibleMonsters)
        {
            string id = monster.monster.monsterId;
            if (monsterCounts.ContainsKey(id))
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

        // ボスだった場合、ボス出現を解除
        if (currentBossId == monsterId)
        {
            Debug.Log($"🗡️ ボス {monsterId} を討伐 → currentBossId を null にします");
            currentBossId = null;
        }
    }

    // 🔍 ログ出力用：エリア状態を文字列に変換
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