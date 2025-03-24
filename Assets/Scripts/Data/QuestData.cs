
using UnityEngine;
using System.Collections.Generic;
using GameData;


[System.Serializable]
public class QuestData
{
    public string questId;
    public string title;
    public QuestType type;

    public string targetId;        // モンスターIDやアイテムID
    public string targetAreaId;    // 目的地となるエリアID 
    public List<ItemData> rewardItems;

    public QuestStatus status;
    public float deadlineHours;
}
