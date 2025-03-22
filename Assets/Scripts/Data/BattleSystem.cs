using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [Header("バトル対象")]
    public MonsterDataSO enemyMonster;

    [Header("現在のフィールドエリアID")]
    public string currentAreaId;

    private MonsterPopulationSystem monsterPopulationSystem;

    private void Start()
    {
        // MonsterPopulationSystem を取得（必要に応じて自動設定）
        monsterPopulationSystem = FindObjectOfType<MonsterPopulationSystem>();
    }

    /// <summary>
    /// バトル終了後、敵を討伐した処理
    /// </summary>
    public void OnMonsterDefeated()
    {
        if (enemyMonster == null || string.IsNullOrEmpty(currentAreaId))
        {
            Debug.LogWarning("討伐処理に必要な情報が不足しています。");
            return;
        }

        // 討伐通知を送る（通常 or ボス）
        var areaState = monsterPopulationSystem.GetAreaState(currentAreaId);
        if (areaState != null)
        {
            // ボスの場合は currentBossId をリセット
            if (areaState.currentBossId == enemyMonster.monsterId)
            {
                Debug.Log($"【Boss討伐】{enemyMonster.displayName} を倒しました！");
            }
            else
            {
                Debug.Log($"【モンスター討伐】{enemyMonster.displayName} を倒しました。");
            }

            // 通常/ボス問わずカウントを減らす
            monsterPopulationSystem.NotifyMonsterDefeated(currentAreaId, enemyMonster.monsterId);
        }
    }
}