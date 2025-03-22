using UnityEngine;
using System.Collections.Generic;
using GameData;

[CreateAssetMenu(menuName = "Data/QuestData", fileName = "NewQuestData")]
public class QuestDataSO : ScriptableObject
{
    public string questId;
    public string title;
    public QuestType type;
    public string targetId;
    public List<ItemDataSO> rewardItems;
    public QuestStatus status;
    public float deadlineHours;

    /// <summary>
    /// ScriptableObject から QuestData のインスタンスを生成
    /// </summary>
    public QuestData CreateQuestInstance()
    {
        return new QuestData
        {
            questId = this.questId,
            title = this.title,
            type = this.type,
            targetId = this.targetId,
            status = this.status,
            deadlineHours = this.deadlineHours,
            rewardItems = this.rewardItems.ConvertAll(itemSO => itemSO.CreateItemInstance())
        };
    }
}

public class QuestData
{
    public string questId;
    public string title;
    public QuestType type;
    public string targetId;
    public List<ItemData> rewardItems;
    public QuestStatus status;
    public float deadlineHours;
}