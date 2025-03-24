using UnityEngine;
using System.Collections.Generic;

public class MonsterSpawnerSystem : MonoBehaviour
{
    public List<FieldAreaDataSO> areaDataList;
    private Dictionary<string, FieldAreaState> areaStates = new();

    private void Start()
    {
        InitializeAreas();
        SpawnInitialMonsters();
    }

    private void InitializeAreas()
    {
        foreach (var area in areaDataList)
        {
            var state = new FieldAreaState();
            state.Initialize(area.areaId, area.maxMonsterCount);
            areaStates[area.areaId] = state;
        }
    }

    private void SpawnInitialMonsters()
    {
        foreach (var area in areaDataList)
        {
            var state = areaStates[area.areaId];
            state.IncreaseMonsterCount(1); // 初期スポーン数
        }
    }

    public void OnMonsterDefeated(string areaId, string monsterId)
    {
        if (!areaStates.TryGetValue(areaId, out var state)) return;

        state.DecreaseMonsterCount();

        if (state.currentBossId == monsterId)
        {
            state.ClearBoss();
            Debug.Log($"💀 ボス {monsterId} が討伐されました");
        }
        else
        {
            Debug.Log($"🗡️ モンスターが1体討伐されました");
        }
    }

    public FieldAreaState GetAreaState(string areaId)
    {
        areaStates.TryGetValue(areaId, out var state);
        return state;
    }
}