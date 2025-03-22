using UnityEngine;
using System.Collections.Generic;

public class BossSpawnerSystem : MonoBehaviour
{
    [SerializeField] private MonsterPopulationSystem populationSystem;
    [SerializeField] private List<FieldAreaDataSO> allAreas;

    private void Start()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnTimeChanged += OnTimeAdvanced;
        }
    }

    private void OnDestroy()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnTimeChanged -= OnTimeAdvanced;
        }
    }

    private void OnTimeAdvanced(GameTime time)
    {
        // 毎朝6時にボス出現判定を行う
        if (time.hour == 6 && time.minute == 0)
        {
            TrySpawnBosses();
        }
    }

    private void TrySpawnBosses()
    {
        foreach (var areaData in allAreas)
        {
            var state = populationSystem.GetAreaState(areaData.areaId);
            if (state == null || !string.IsNullOrEmpty(state.currentBossId)) continue;

            foreach (var bossData in areaData.possibleBosses)
            {
                if (Random.value < bossData.spawnChance)
                {
                    state.currentBossId = bossData.boss.monsterId;
                    Debug.Log($"【Boss出現】{areaData.areaName} に {bossData.boss.displayName} が出現！");
                    break;
                }
            }
        }
    }
}