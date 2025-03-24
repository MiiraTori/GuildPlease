using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterSpawnData
{
    public MonsterDataSO monsterData;
    [Range(0f, 1f)]
    public float spawnRate;
}

[System.Serializable]
public class ItemSpawnData
{
    public ItemDataSO item;
    [Range(0f, 1f)]
    public float spawnRate;
}