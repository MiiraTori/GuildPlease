using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クエスト一覧を管理するコンポーネント（IDで取得可能）
/// </ summary >
public class QuestListComponent : MonoBehaviour
{
    private Dictionary<string, QuestData> questMap = new Dictionary<string, QuestData>();

    [SerializeField]
    private List<QuestDataSO> questSOList = new List<QuestDataSO>();

    private void Awake()
    {
        questMap.Clear();

        foreach (var questSO in questSOList)
        {
            if (!string.IsNullOrEmpty(questSO.questId))
            {
                questMap[questSO.questId] = questSO.CreateQuestInstance();
            }
            else
            {
                Debug.LogWarning($"⚠️ クエストIDが空です: {questSO.title}");
            }
        }
    }

    /// <summary>
    /// IDからクエストを取得
    /// </summary>
    public QuestData GetQuestById(string questId)
    {
        if (questMap.TryGetValue(questId, out var quest))
        {
            return quest;
        }

        Debug.LogWarning($"⛔ クエストID {questId} が見つかりません！");
        return null;
    }

    /// <summary>
    /// 全クエストのリストを取得
    /// </summary>
    public List<QuestData> GetAllQuests()
    {
        return new List<QuestData>(questMap.Values);
    }

    /// <summary>
    /// クエストを追加（動的に追加する場合）
    /// </summary>
    public void AddQuest(QuestData newQuest)
    {
        if (!questMap.ContainsKey(newQuest.questId))
        {
            questMap[newQuest.questId] = newQuest;
        }
        else
        {
            Debug.LogWarning($"⚠️ クエスト {newQuest.questId} はすでに存在しています。");
        }
    }

    /// <summary>
    /// クエストを削除
    /// </summary>
    public void RemoveQuest(string questId)
    {
        if (questMap.Remove(questId))
        {
            Debug.Log($"✅ クエスト {questId} を削除しました。");
        }
        else
        {
            Debug.LogWarning($"⛔ クエスト {questId} は見つかりません。");
        }
    }

    /// <summary>
    /// クエストの進行状況を更新（モンスター討伐など）
    /// </summary>
    public void UpdateQuestProgress(string questId, int count)
    {
        var quest = GetQuestById(questId);
        if (quest != null && quest.status == QuestStatus.InProgress)
        {
            quest.defeatedCount += count;
            Debug.Log($"📌 クエスト {quest.title} の進行状況: {quest.defeatedCount} / {quest.targetCount}");

            if (quest.defeatedCount >= quest.targetCount)
            {
                CompleteQuest(questId);
            }
        }
    }

    /// <summary>
    /// クエスト完了処理
    /// </summary>
    public void CompleteQuest(string questId)
    {
        var quest = GetQuestById(questId);
        if (quest != null && quest.status == QuestStatus.InProgress)
        {
            quest.status = QuestStatus.Completed;
            Debug.Log($"🎉 クエスト {quest.title} が完了しました！");
            GrantRewards(quest);
        }
    }

    /// <summary>
    /// クエスト報酬を付与
    /// </summary>
    private void GrantRewards(QuestData quest)
    {
        foreach (var item in quest.rewardItems)
        {
            Debug.Log($"🎁 {item.itemName} x{item.itemName} を獲得！");

            // 修正必須
           // InventoryComponent.AddItem(item);
        }
    }

    /// <summary>
    /// クエスト期限をチェックし、期限切れのクエストを失敗にする
    /// </summary>
    public void CheckQuestDeadlines(float elapsedHours)
    {
        foreach (var quest in questMap.Values)
        {
            if (quest.status == QuestStatus.InProgress && elapsedHours >= quest.deadlineHours)
            {
                quest.status = QuestStatus.Failed;
                Debug.Log($"⏳ クエスト {quest.title} は期限切れで失敗しました！");
            }
        }
    }
}