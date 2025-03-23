using UnityEngine;
using GameData;

[RequireComponent(typeof(TaskTimerComponent))]
public class AdventurerAIComponent : MonoBehaviour
{
    public AdventurerState state;
    private TaskTimerComponent timer;

    private void Start()
    {
        timer = GetComponent<TaskTimerComponent>();

        if (state == null || state.currentQuest == null)
        {
            Debug.LogWarning("冒険者状態またはクエストが設定されていません");
            return;
        }

        StartNextAction();
    }

    public void AssignQuest(QuestData quest)
    {
        if (state.isBusy) return;

        state.currentQuest = quest;
        state.currentAction = AdventurerActionType.Move;
        state.isBusy = true;

        Debug.Log($"🧾 クエスト受注：{quest.title}");
        StartNextAction();
    }

    private void StartNextAction()
    {
        if (state.isBusy) return;

        AdventurerActionType next = ChooseNextAction();
        state.currentAction = next;

        switch (next)
        {
            case AdventurerActionType.Move:
                MoveToQuestArea();
                break;
            case AdventurerActionType.Combat:
                EngageInCombat();
                break;
            case AdventurerActionType.Return:
                ReturnToGuild();
                break;
            case AdventurerActionType.None:
            default:
                Debug.Log($"冒険者 {state.data.displayName} は行動待機中");
                break;
        }
    }

    private AdventurerActionType ChooseNextAction()
    {
        switch (state.currentAction)
        {
            case AdventurerActionType.None:
                return AdventurerActionType.Move;
            case AdventurerActionType.Move:
                return AdventurerActionType.Combat;
            case AdventurerActionType.Combat:
                return AdventurerActionType.Return;
            case AdventurerActionType.Return:
            default:
                return AdventurerActionType.None;
        }
    }

    // ✅ クエストの目的地に移動
    private void MoveToQuestArea()
    {
        if (state.currentQuest == null)
        {
            Debug.LogWarning("クエストがありません");
            return;
        }

        string areaId = state.currentQuest.targetAreaId;

        state.isBusy = true;
        Debug.Log($"【移動中】{state.data.displayName} → クエストエリア {areaId}");

        timer.StartTask(5f, () =>
        {
            state.currentAreaId = areaId;
            state.isBusy = false;
            StartNextAction();
        });
    }

    // ✅ 戦闘処理とクエスト判定
    private void EngageInCombat()
    {
        state.isBusy = true;
        Debug.Log($"【戦闘開始】{state.data.displayName} at {state.currentAreaId}");

        timer.StartTask(4f, () =>
        {
            Debug.Log($"【戦闘終了】{state.data.displayName} → 討伐完了");

            CheckQuestCompletion();

            state.isBusy = false;
            StartNextAction();
        });
    }

    // ✅ クエスト達成判定処理
    private void CheckQuestCompletion()
    {
        if (state.currentQuest == null)
            return;

        string target = state.currentQuest.targetId;

        if (!string.IsNullOrEmpty(target) && Random.value > 0.5f)
        {
            state.currentQuest.status = QuestStatus.Completed;
            Debug.Log($"【クエスト達成】{state.data.displayName} が {target} を討伐しました！");
        }
        else
        {
            Debug.Log($"【クエスト未達成】{state.data.displayName} は対象に遭遇しませんでした。");
        }
    }

    // ✅ 帰還＆報酬受け取り処理
    private void ReturnToGuild()
    {
        state.isBusy = true;
        Debug.Log($"【帰還中】{state.data.displayName} ギルドへ戻る");

        timer.StartTask(3f, () =>
        {
            state.currentAreaId = "guild";

            // ✅ クエスト報酬処理
            if (state.currentQuest != null && state.currentQuest.status == QuestStatus.Completed)
            {
                var inventory = GetComponent<InventoryComponent>();
                if (inventory != null)
                {
                    foreach (var reward in state.currentQuest.rewardItems)
                    {
                        inventory.AddItem(reward);
                        Debug.Log($"【報酬獲得】{state.data.displayName} は {reward.itemName} を受け取った！");
                    }
                }
            }

            state.ResetState();
            StartNextAction();
        });
    }
}