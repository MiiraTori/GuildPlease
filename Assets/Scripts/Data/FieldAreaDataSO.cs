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
    public List<MonsterDataSO> possibleBosses;

    [Header("アイテムドロップ設定")]
    public List<ItemData> possibleItemDrops;


    // 将来的な拡張用（例えばBOSS）
    [System.Serializable]
    public class MonsterSpawnData
    {
        public MonsterDataSO monsterData;  // 出現するモンスターのデータ
        [Range(0f, 1f)]
        public float spawnRate = 1f;       // 出現確率（合計で1未満でもOK）
    }
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

