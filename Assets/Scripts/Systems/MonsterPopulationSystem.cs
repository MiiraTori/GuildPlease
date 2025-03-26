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
            var state = new FieldAreaState();
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

    public IEnumerable<FieldAreaState> GetAllAreaStates()
    {
        return areaStates.Values;
    }
}