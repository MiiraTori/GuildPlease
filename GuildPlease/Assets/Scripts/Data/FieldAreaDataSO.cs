using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Data/Field Area", fileName = "NewFieldArea")]
public class FieldAreaDataSO : ScriptableObject
{
    [Header("エリア基本情報")]
    public string areaId;
    public string areaName;
    [TextArea]
    public string description;
    public AreaType areaType;
    public int dangerLevel;

    [Header("移動・接続情報")]
    public float travelTimeMinutes;
    public List<string> connectedAreas;

    [Header("遭遇設定")]
    [Range(0f, 1f)]
    public float encounterRate;
    public List<MonsterSpawnData> encounterMonsters;

    [Header("採取可能アイテム")]
    public List<ItemSpawnData> gatherableItems;

    [Header("ボス出現設定")]
    public List<BossSpawnData> possibleBosses;

}

public enum AreaType
{
    Forest,
    Cave,
    Village,
    Field,
    Dungeon,
    Special
}

[System.Serializable]
public class MonsterSpawnData
{
    public MonsterDataSO monster;
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