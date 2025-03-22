using UnityEngine;
using System.Collections.Generic;

public class MonsterPopulationSystem : MonoBehaviour
{
    [SerializeField] private List<FieldAreaDataSO> allFieldAreas;

    private Dictionary<string, FieldAreaState> areaStates = new();

    private void Awake()
    {
        foreach (var area in allFieldAreas)
        {
            var state = new FieldAreaState { areaId = area.areaId };
            state.Initialize(area, initialCount: 1); // ← ここで初期化を明示的に

            areaStates.Add(area.areaId, state);
        }

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
        foreach (var area in allFieldAreas)
        {
            if (areaStates.TryGetValue(area.areaId, out var state))
            {
                state.IncreaseMonsters(area, maxCountPerMonster: 5); // ← 上限を指定
            }
        }
    }

    public FieldAreaState GetAreaState(string areaId)
    {
        return areaStates.TryGetValue(areaId, out var state) ? state : null;
    }

    public void NotifyMonsterDefeated(string areaId, string monsterId)
    {
        var state = GetAreaState(areaId);
        state?.OnMonsterDefeated(monsterId);
    }

    /// <summary>すべてのエリア状態をログに出力（デバッグ用）</summary>
    public void LogAllAreaStates()
    {
        foreach (var kv in areaStates)
        {
            kv.Value.LogStatus();
        }
    }
}