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
        Debug.Log($"[AI] TaskTimerComponent: {(timer != null ? "取得成功" : "取得失敗")}");
    }

    public void AssignQuest(QuestData quest)
    {
        if (state.isBusy)
        {
            Debug.LogWarning("⚠️ 冒険者は現在別の行動中です！");
            return;
        }

        state.ResetState();
        state.currentQuest = quest;

        Debug.Log($"📝 冒険者にクエスト「{quest.title}」を渡しました！");
        StartQuest();
    }

    private void StartQuest()
    {
        if (state.currentQuest == null)
        {
            Debug.LogWarning("❌ クエストが設定されていません！");
            return;
        }

        if (state.isBusy)
        {
            Debug.LogWarning("⚠️ すでに行動中のため開始できません");
            return;
        }

        state.isBusy = true;
        state.currentAction = AdventurerActionType.MoveToTarget;

        Debug.Log($"🚶 移動開始 → {state.currentQuest.targetAreaId}");
        timer.StartTimer(2f, ProceedQuestStep);
    }

    private void ProceedQuestStep()
    {
        LogAdventurerStatus("開始");

        switch (state.currentAction)
        {
            case AdventurerActionType.MoveToTarget:
                Debug.Log($"⚔️ 戦闘開始！");
                state.currentAction = AdventurerActionType.EngageCombat;
                timer.StartTimer(2f, ProceedQuestStep);
                break;

            case AdventurerActionType.EngageCombat:
                string targetId = state.currentQuest.targetId;
                Debug.Log($"⚔️ 討伐中… モンスター「{targetId}」を倒した！");

                var inventory = GetComponent<InventoryComponent>();
                if (goblinHornSO != null && inventory != null)
                {
                    var item = goblinHornSO.CreateItemInstance();
                    inventory.AddItem(item);
                    Debug.Log($"🎁 ドロップ獲得: {item.itemName}");

                    if (inventory.HasItem(targetId))
                    {
                        Debug.Log($"✅ クエスト達成条件アイテム（{targetId}）を所持 → 達成判定OK");

                        // 🎯 クエスト完了状態に更新
                        state.currentQuest.status = QuestStatus.Completed;
                        Debug.Log($"🎉 クエスト「{state.currentQuest.title}」は達成されました！");
                    }
                    else
                    {
                        Debug.Log($"❌ クエスト達成条件アイテム（{targetId}）未所持 → 未達成");
                    }
                }

                state.currentAction = AdventurerActionType.ReturnToGuild;
                timer.StartTimer(2f, ProceedQuestStep);
                break;

            case AdventurerActionType.ReturnToGuild:
                Debug.Log($"🏠 ギルドに帰還しました");

                if (state.currentQuest != null && state.currentQuest.status == QuestStatus.Completed)
                {
                    Debug.Log("📣 クエスト完了報告へ進みます！");
                    state.currentAction = AdventurerActionType.SubmitReport;
                    timer.StartTimer(1f, ProceedQuestStep);
                }
                else
                {
                    Debug.Log("🛑 クエスト未達成のため報告しません");
                    state.ResetState();
                }
                break;

            case AdventurerActionType.SubmitReport:
                Debug.Log($"📜 クエスト「{state.currentQuest.title}」を報告しました！");
                Debug.Log($"🎉 報酬を受け取りました！（※報酬処理は今後追加）");
                state.ResetState();
                break;
        }

        LogAdventurerStatus("終了");
    }

    private void LogAdventurerStatus(string stepName)
    {
        Debug.Log($"🧠 [{stepName}] 状態: {state.currentAction} / 現在地: {state.currentAreaId} / クエスト: {state.currentQuest?.title}");
    }
}