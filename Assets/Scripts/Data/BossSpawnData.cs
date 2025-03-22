using UnityEngine;

[System.Serializable]
public class BossSpawnData
{
    /// <summary>出現するボスのデータ</summary>
    public MonsterDataSO boss;

    /// <summary>出現確率（0.0～1.0）</summary>
    [Range(0f, 1f)]
    public float spawnChance;
}