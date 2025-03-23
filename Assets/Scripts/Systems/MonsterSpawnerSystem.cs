using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 各フィールドエリアにおけるモンスター・ボスの出現を管理するシステム
/// </summary>
public class MonsterSpawnerSystem : MonoBehaviour
{
    [Header("管理するすべてのエリア")]
    [SerializeField] private List<FieldAreaDataSO> fieldAreas;

    private bool bossSpawnedToday = false;

    private void Start()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnHourChanged += HandleHourChange;
        }
    }

    private void OnDestroy()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnHourChanged -= HandleHourChange;
        }
    }

    /// <summary>
    /// 時間が進んだときの処理（毎時）
    /// </summary>
    private void HandleHourChange(int hour)
    {
        if (hour == 6 && !bossSpawnedToday)
        {
            foreach (var area in fieldAreas)
            {
                TrySpawnBoss(area);
            }
            bossSpawnedToday = true;
        }
        else if (hour == 0)
        {
            bossSpawnedToday = false; // 日付変更でリセット
        }
    }

    /// <summary>
    /// ボス出現判定
    /// </summary>
    public void TrySpawnBoss(FieldAreaDataSO area)
    {
        foreach (var boss in area.possibleBosses)
        {
            if (Random.value < boss.spawnChance)
            {
                Debug.Log($"<color=red>【ボス出現】{boss.monsterData.displayName} が {area.areaName} に出現！</color>");
                SpawnedMonsterComponent.RegisterBoss(area, boss.monsterData);
                return;
            }
        }
    }

    /// <summary>
    /// 通常モンスター出現判定（探索中・移動時など）
    /// </summary>
    public void TrySpawnMonster(FieldAreaDataSO area)
    {
        foreach (var monster in area.encounterMonsters)
        {
            if (Random.value < monster.spawnRate)
            {
                Debug.Log($"<color=green>【モンスター出現】{monster.monsterData.displayName} が {area.areaName} に出現！</color>");
                SpawnedMonsterComponent.RegisterMonster(area, monster.monsterData);
                return;
            }
        }
    }
}