using System.Diagnostics;
using UnityEngine;

public class AdventurerSpawner : MonoBehaviour
{
    [Header("テスト設定")]
    public string testAreaId = "forest01";
    public string testMonsterId = "goblin001";
   
    private MonsterPopulationSystem monsterSystem;

    void Start()
    {
        monsterSystem = FindObjectOfType<MonsterPopulationSystem>();

        if (monsterSystem == null)
        {
            UnityEngine.Debug.LogError("MonsterPopulationSystem がシーン内に見つかりません！");
            return;
        }

        // ✅ テスト実行：討伐通知を送信
        monsterSystem.NotifyMonsterDefeated(testAreaId, testMonsterId);
    }
}