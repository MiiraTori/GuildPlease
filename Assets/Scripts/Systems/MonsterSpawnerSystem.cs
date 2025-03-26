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
            areaStates[area.areaId] = state;
        }
    }

    private void SpawnInitialMonsters()
    {
        foreach (var area in areaDataList)
        {
            var state = areaStates[area.areaId];
          //  state.IncreaseMonsterCount(1); // 初期スポーン数
        }
    }

    public void OnMonsterDefeated(string areaId, string monsterId)
    {
        if (!areaStates.TryGetValue(areaId, out var state)) return;

        
    }

    public FieldAreaState GetAreaState(string areaId)
    {
        areaStates.TryGetValue(areaId, out var state);
        return state;
    }

    public MonsterData GetRandomMonsterInArea(string areaId)
    {
        if (areaStates.TryGetValue(areaId, out var state))
        {
            return state.GetRandomMonster();
        }
        Debug.LogWarning($"🛑 モンスターが見つかりません: areaId = {areaId}");
        return null;
    }
  
}