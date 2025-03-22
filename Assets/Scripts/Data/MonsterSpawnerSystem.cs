using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerSystem : MonoBehaviour
{
    [SerializeField] private List<FieldAreaDataSO> fieldAreas; // 全フィールドの参照
    private bool bossSpawnedToday = false;

    private void Start()
    {
        // 毎朝6時にボス判定を行う（TimeManagerが必要）
        TimeManager.Instance.OnHourChanged += HandleHourChange;
    }

    private void OnDestroy()
    {
        TimeManager.Instance.OnHourChanged -= HandleHourChange;
    }

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
            bossSpawnedToday = false; // 翌日リセット
        }
    }

    public void TrySpawnBoss(FieldAreaDataSO area)
    {
        foreach (var boss in area.possibleBosses)
        {
            if (Random.value < boss.spawnChance)
            {
                Debug.Log($"<color=red>【ボス出現】{boss.boss.displayName} が {area.areaName} に出現！</color>");
                SpawnedMonsterComponent.RegisterBoss(area, boss.boss.displayName);
                return;
            }
        }
    }

    public void TrySpawnMonster(FieldAreaDataSO area)
    {
        foreach (var monster in area.possibleMonsters)
        {
            if (Random.value < monster.spawnChance)
            {
                Debug.Log($"<color=green>【モンスター出現】{monster.monsterData.characterName} が {area.areaName} に出現！</color>");
                SpawnedMonsterComponent.RegisterMonster(area, monster.monsterData);
                return;
            }
        }
    }
}