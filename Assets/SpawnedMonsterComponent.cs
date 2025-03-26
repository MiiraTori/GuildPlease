using System.Collections.Generic;
using UnityEngine;

public class SpawnedMonsterComponent : MonoBehaviour
{
    public List<SpawnedMonster> activeMonsters = new List<SpawnedMonster>();

    public void AddMonster(SpawnedMonster monster)
    {
        activeMonsters.Add(monster);
    }

    public void RemoveMonster(SpawnedMonster monster)
    {
        activeMonsters.Remove(monster);
    }
}