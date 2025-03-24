using UnityEngine;
using System.Collections.Generic;

public class FieldAreaManager : MonoBehaviour
{
    public List<FieldAreaState> allAreas;

    public void Initialize()
    {
        foreach (var area in allAreas)
        {
            area.Initialize(area.areaId, area.maxMonsterCount);
      ;
        }
    }

    public FieldAreaState GetAreaById(string areaId)
    {
        return allAreas.Find(a => a.areaId == areaId);
    }

    public void ReportMonsterDefeated(string areaId)
    {
        var area = GetAreaById(areaId);
        if (area != null)
        {
            area.DecreaseMonsterCount();
        }
    }

    public void ClearBoss(string areaId)
    {
        var area = GetAreaById(areaId);
        if (area != null)
        {
            area.currentBossId = null;
        }
    }
}