using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Data/Field Area", fileName = "NewFieldAreaData")]
public class FieldAreaDataSO : ScriptableObject
{
    [Header("エリア基本情報")]
    public string areaId;
    public string areaName;

    [Header("モンスター出現設定")]
    public int maxMonsterCount = 5;
    public List<MonsterDataSO> possibleMonsters;

    [Header("ボス出現設定")]
    public List<BossSpawnData> possibleBosses;

    [Header("アイテムドロップ設定")]
    public List<ItemData> possibleItemDrops;
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

