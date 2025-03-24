using GameData;
using UnityEngine;

[RequireComponent(typeof(TaskTimerComponent))]
public class AdventurerAIComponent : MonoBehaviour
{
    public AdventurerState state;

    [Header("🔹 ドロップアイテム")]
    [SerializeField] private ItemDataSO goblinHornSO;

    private TaskTimerComponent timer;

    private void Awake()
    {
        timer = GetComponent<TaskTimerComponent>();
    }

    public void AssignQuest(QuestData quest)
    {
        if (state.isBusy) return;

        state.ResetState();
        state.currentQuest = quest;

        Debug.Log($"📝 冒険者にクエスト「{quest.title}」を渡しました！");
        StartQuest();
    }

    private void StartQuest()
    {
        if (state.currentQuest == null) return;
        if (state.isBusy) return;

        state.isBusy = true;
        state.currentAction = AdventurerActionType.MoveToTarget;
        timer.StartTimer(2f, ProceedQuestStep);
    }

    private void ProceedQuestStep()
    {
        LogAdventurerStatus("処理中");

        switch (state.currentAction)
        {
            case AdventurerActionType.MoveToTarget:
                state.currentAction = AdventurerActionType.EngageCombat;
                Debug.Log("⚔️ 戦闘開始！");
                timer.StartTimer(2f, ProceedQuestStep);
                break;

            case AdventurerActionType.EngageCombat:
                string targetId = state.currentQuest.targetId;
                Debug.Log($"🗡️ モンスター「{targetId}」を討伐！");

                var inventory = GetComponent<InventoryComponent>();
                if (goblinHornSO != null && inventory != null)
                {
                    var item = goblinHornSO.CreateItemInstance();
                    inventory.AddItem(item);
                    Debug.Log($"🎁 ドロップ獲得: {item.displayName}");

                    if (inventory.HasItem(targetId))
                    {
                        // 🎯 クエスト達成
                        state.currentQuest.status = QuestStatus.Completed;
                        Debug.Log($"✅ クエスト達成！");

                        // 🎖️ ポイント加算
                        state.data.guildPoints += 10;
                        state.data.experience += 30;
                        Debug.Log($"🏅 ギルドポイント +10（現在: {state.data.guildPoints}）");
                        Debug.Log($"✨ 経験値 +30（現在: {state.data.experience}）");
                    }
                }

                state.currentAction = AdventurerActionType.ReturnToGuild;
                timer.StartTimer(2f, ProceedQuestStep);
                break;

            case AdventurerActionType.ReturnToGuild:
                Debug.Log("🏠 ギルドに帰還");

                if (state.currentQuest != null && state.currentQuest.status == QuestStatus.Completed)
                {
                    state.currentAction = AdventurerActionType.SubmitReport;
                    timer.StartTimer(1f, ProceedQuestStep);
                }
                else
                {
                    Debug.Log("⚠️ クエスト失敗！ポイント減少");
                    state.data.guildPoints = Mathf.Max(0, state.data.guildPoints - 5);
                    state.ResetState();
                }
                break;

            case AdventurerActionType.SubmitReport:
                Debug.Log($"📜 クエスト「{state.currentQuest.title}」を報告しました！");

                var inv = GetComponent<InventoryComponent>();
                if (inv != null && state.currentQuest.rewardItems != null)
                {
                    foreach (var reward in state.currentQuest.rewardItems)
                    {
                        inv.AddItem(reward);
                        Debug.Log($"🎉 報酬受取: {reward.displayName}");
                    }
                }

                state.ResetState();
                break;
        }

        LogAdventurerStatus("完了");
    }

    private void LogAdventurerStatus(string stepName)
    {
        Debug.Log($"🧠 [{stepName}] 状態: {state.currentAction} / 現在地: {state.currentAreaId} / クエスト: {state.currentQuest?.title}");
    }
}