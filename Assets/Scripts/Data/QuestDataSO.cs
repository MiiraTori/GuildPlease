using System.Collections;
using System.Collections.Generic;
using GameData;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/QuestData", fileName = "NewQuestData")]
public class QuestDataSO : ScriptableObject
{
    public string questId;
    public string title;
    public QuestType type;
    public int targetCount; // 討伐目標数
    public string targetId;
    public string targetAreaId;
    public List<ItemDataSO> rewardItems;
    public QuestStatus status;
    public float deadlineHours;

    public QuestData CreateQuestInstance()
    {
        return new QuestData
        {
            questId = this.questId,
            title = this.title,
            type = this.type,
            targetCount = this.targetCount,
            targetId = this.targetId,
            targetAreaId = this.targetAreaId,
            status = this.status,
            deadlineHours = this.deadlineHours,
            rewardItems = this.rewardItems.ConvertAll(itemSO => itemSO.CreateItemInstance()),
            defeatedCount = 0
        };
    }
}