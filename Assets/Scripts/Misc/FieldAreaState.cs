using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class FieldAreaState
{
    public string areaId;

    public int currentMonsterCount;
    public int maxMonsterCount;

    public string currentBossId;

    public List<string> droppedItemIds = new();

    public void Initialize(string areaId, int maxMonsterCount)
    {
        this.areaId = areaId;
        this.maxMonsterCount = maxMonsterCount;
        this.currentMonsterCount = maxMonsterCount;
        this.currentBossId = null;
        this.droppedItemIds.Clear();
    }

    public void IncreaseMonsterCount(int amount = 1)
    {
        currentMonsterCount = Mathf.Min(currentMonsterCount + amount, maxMonsterCount);
    }

    public void DecreaseMonsterCount(int amount = 1)
    {
        currentMonsterCount = Mathf.Max(currentMonsterCount - amount, 0);
    }

    public void AddDrop(string itemId)
    {
        droppedItemIds.Add(itemId);
    }

    public void ClearBoss()
    {
        currentBossId = null;
    }

    public string GetStatusString()
    {
        return $"エリア: {areaId}, モンスター: {currentMonsterCount}/{maxMonsterCount}, ボス: {(string.IsNullOrEmpty(currentBossId) ? "なし" : currentBossId)}, ドロップ: {droppedItemIds.Count}個";
    }

    public void OnMonsterDefeated(string monsterId)
    {
        DecreaseMonsterCount();

        if (currentBossId == monsterId)
        {
            ClearBoss();
            Debug.Log($"🧨 ボス {monsterId} を討伐！エリア {areaId} のボスは消えました。");
        }
        else
        {
            Debug.Log($"🗡️ モンスターを討伐（エリア: {areaId}）");
        }
    }
}