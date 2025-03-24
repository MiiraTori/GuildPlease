using UnityEngine;

[RequireComponent(typeof(TaskTimerComponent))]
public class AdventurerAIComponent : MonoBehaviour
{
    public AdventurerState state;

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
        switch (state.currentAction)
        {
            case AdventurerActionType.MoveToTarget:
                Debug.Log($"⚔️ 戦闘開始！");
                state.currentAction = AdventurerActionType.EngageCombat;
                timer.StartTimer(2f, ProceedQuestStep);
                break;

            case AdventurerActionType.EngageCombat:
                Debug.Log($"🏠 帰還中...");
                state.currentAction = AdventurerActionType.ReturnToGuild;
                timer.StartTimer(2f, ProceedQuestStep);
                break;

            case AdventurerActionType.ReturnToGuild:
                Debug.Log($"✅ クエスト完了！");
                state.ResetState();
                break;
        }
    }
}