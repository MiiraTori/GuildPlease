using UnityEngine;
using GameData;
using System.Collections.Generic;

public class AdventurerAIComponent : MonoBehaviour
{
    private AdventurerState state;
    private TaskTimerComponent timer;
    private InventoryComponent inventory;

    [SerializeField] private AdventurerDataSO adventurerDataSO;
    [SerializeField] private FieldAreaListComponent areaList;
    [SerializeField] private MonsterSpawnerSystem monsterSpawner;
    [SerializeField] private ItemListComponent itemList;

    [SerializeField] private int combatDuration = 2;
    [SerializeField] private int staminaCostPerCombat = 10;

    private void Awake()
    {
        state = new AdventurerState(adventurerDataSO.CreateAdventurerInstance());
        timer = GetComponent<TaskTimerComponent>();
        inventory = GetComponent<InventoryComponent>();
    }

    private void Update()
    {
        if (state == null || timer == null || timer.IsRunning || state.isBusy) return;
        PerformAction();
    }

    public void AssignQuest(QuestData quest)
    {
        if (state == null || state.isBusy) return;

        state.currentQuest = quest;
        Debug.Log($"🧭 冒険者にクエスト「{quest.title}」を渡しました！");
        StartMoveToTarget();
    }

    private void PerformAction()
    {
        switch (state.currentAction)
        {
            case AdventurerActionType.MoveToTarget:
                StartMoveToTarget();
                break;
            case AdventurerActionType.EngageCombat:
                EngageCombat();
                break;
            case AdventurerActionType.ReturnToGuild:
                StartReturnToGuild();
                break;
            case AdventurerActionType.SubmitReport:
                StartSubmitReport();
                break;
            case AdventurerActionType.Rest:
                StartRest();
                break;
            default:
                DecideNextAction();
                break;
        }
    }

    private void DecideNextAction()
    {
        if (state.currentQuest != null && state.currentQuest.status != QuestStatus.Completed)
        {
            if (state.data.currentStamina < staminaCostPerCombat)
            {
                state.currentAction = AdventurerActionType.Rest;
            }
            else if (state.currentAreaId != state.currentQuest.targetAreaId)
            {
                state.currentAction = AdventurerActionType.MoveToTarget;
            }
            else
            {
                state.currentAction = AdventurerActionType.EngageCombat;
            }
        }
        else if (state.currentQuest != null && state.currentQuest.status == QuestStatus.Completed)
        {
            state.currentAction = AdventurerActionType.ReturnToGuild;
        }
        else
        {
            state.currentAction = AdventurerActionType.None;
        }
    }

    private void StartMoveToTarget()
    {
        state.currentAction = AdventurerActionType.MoveToTarget;
        state.isBusy = true;

        Debug.Log($"🚶‍♂️ 冒険者が {state.currentQuest.targetAreaId} へ向かっています…");
        timer.StartTimer(2f, () =>
        {
            state.currentAreaId = state.currentQuest.targetAreaId;
            state.isBusy = false;
            state.currentAction = AdventurerActionType.EngageCombat;
        });
    }

    private void EngageCombat()
    {
        state.isBusy = true;
        Debug.Log($"⚔️ 冒険者が戦闘を開始します！");

        timer.StartTimer(combatDuration, () =>
        {
            var monster = monsterSpawner.GetRandomMonsterInArea(state.currentAreaId);

            if (monster == null)
            {
                Debug.Log("👀 モンスターがいませんでした。");
                state.isBusy = false;
                DecideNextAction();
                return;
            }

            Debug.Log($"🛡️ 「{monster.displayName}」と戦闘中…");
            state.data.currentStamina -= staminaCostPerCombat;
            /*
            if (monsterSpawner..DefeatMonster(area.areaId, monster.displayName))
            {
                string dropId = monster.dropItems[0].itemId;
                if (dropId != null)
                {
                    var item = itemList.GetItemById(dropId);
                    inventory.AddItem(item);
                    Debug.Log($"🎁 ドロップアイテム「{item.itemId}」を入手！");
                }

                state.data.experience += monster.experienceReward;
                state.data.guildPoints += 10    ;
               }
            */
            if (state.data.currentStamina <= 0)
            {
                Debug.Log($"💤 スタミナが尽きたため帰還します。");
                state.currentAction = AdventurerActionType.ReturnToGuild;
            }
            else if (state.currentQuest.status == QuestStatus.Completed)
            {
                state.currentAction = AdventurerActionType.ReturnToGuild;
            }

            state.isBusy = false;
        });
    }

    private void StartReturnToGuild()
    {
        state.currentAction = AdventurerActionType.ReturnToGuild;
        state.isBusy = true;

        Debug.Log("🏠 ギルドに帰還中…");
        timer.StartTimer(2f, () =>
        {
            state.currentAreaId = "guild";
            state.isBusy = false;
            state.currentAction = AdventurerActionType.SubmitReport;
        });
    }

    private void StartSubmitReport()
    {
        state.isBusy = true;

        Debug.Log($"📝 クエスト「{state.currentQuest.title}」を報告しました！");
        state.currentQuest.status = QuestStatus.Completed;

        state.currentQuest = null;
        state.currentAction = AdventurerActionType.None;
        state.isBusy = false;
    }

    private void StartRest()
    {
        state.isBusy = true;
        Debug.Log("💤 冒険者が休憩しています…");

        timer.StartTimer(3f, () =>
        {
            state.data.currentStamina = state.data.MaxStamina;
            Debug.Log("🔋 スタミナが回復しました！");
            state.isBusy = false;
            DecideNextAction();
        });
    }
}