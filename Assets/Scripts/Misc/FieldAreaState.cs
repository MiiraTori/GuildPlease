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

    public void Initialize(int maxCount)
    {
        maxMonsterCount = maxCount;
        currentMonsterCount = maxCount;
        currentBossId = null;
        droppedItemIds.Clear();
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
}