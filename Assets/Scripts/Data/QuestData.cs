
using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class QuestData
{
    public string questId;
    public string title;
    public QuestType type;
    public int targetCount; // 討伐目標数
    public string targetId;        // モンスターIDやアイテムID
    public string targetAreaId;    // 目的地となるエリアID 
    public List<ItemData> rewardItems;
    public QuestStatus status;
    public float deadlineHours;
    public int defeatedCount; // 現在の討伐数

  
}
