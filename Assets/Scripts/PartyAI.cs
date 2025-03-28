using UnityEngine;

public class PartyAI : MonoBehaviour
{
    public PartyData party;
    public QuestListComponent questManager;
    private float actionCooldown = 0;

    private void Start()
    {
        TimeManager.Instance.OnHourChanged += HandleHourChange;
    }

    private void Awake()
    {
        questManager = GameObject.FindObjectOfType<QuestListComponent>();
    }

    private void HandleHourChange(int hour)
    {
        actionCooldown -= 1;
        if (actionCooldown <= 0)
        {
            PerformAction();
            actionCooldown = Random.Range(1, 4); // 1~3時間ごとに行動
        }
    }

    private void PerformAction()
    {
        if (!string.IsNullOrEmpty(party.currentQuestId))
        {
            if (questManager.GetQuestById(party.currentQuestId) !=null)
            {
                MoveToQuestLocation();
            }
            else
            {
                Debug.Log($"{party.partyName} のクエストがキャンセルされました。");
                party.currentQuestId = null;
            }
        }
        else
        {
            RestOrExplore();
        }
    }

    private void MoveToQuestLocation()
    {
        Debug.Log($"{party.partyName} は {party.currentLocationId} から クエストエリアへ移動中！");
        party.currentLocationId = "QuestLocation"; // 目的地に移動
    }

    private void RestOrExplore()
    {
        Debug.Log($"{party.partyName} は休憩または探索中...");
    }
}