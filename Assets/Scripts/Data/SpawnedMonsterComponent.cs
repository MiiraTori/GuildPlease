using System.Collections.Generic;
using UnityEngine;

public static class SpawnedMonsterComponent
{
    // 出現中の通常モンスターを記録
    private static Dictionary<FieldAreaDataSO, MonsterDataSO> spawnedMonsters = new Dictionary<FieldAreaDataSO, MonsterDataSO>();

    // 出現中のボスを記録
    private static Dictionary<FieldAreaDataSO, MonsterDataSO> spawnedBosses = new Dictionary<FieldAreaDataSO, MonsterDataSO>();

    /// <summary>
    /// 通常モンスターの出現を記録
    /// </summary>
    public static void RegisterMonster(FieldAreaDataSO area, MonsterDataSO monster)
    {
        spawnedMonsters[area] = monster;
    }

    /// <summary>
    /// ボスの出現を記録
    /// </summary>
    public static void RegisterBoss(FieldAreaDataSO area, MonsterDataSO boss)
    {
        spawnedBosses[area] = boss;
    }

    /// <summary>
    /// 指定エリアに出現中の通常モンスターを取得（null可）
    /// </summary>
    public static MonsterDataSO GetSpawnedMonster(FieldAreaDataSO area)
    {
        return spawnedMonsters.TryGetValue(area, out var monster) ? monster : null;
    }

    /// <summary>
    /// 指定エリアに出現中のボスを取得（null可）
    /// </summary>
    public static MonsterDataSO GetSpawnedBoss(FieldAreaDataSO area)
    {
        return spawnedBosses.TryGetValue(area, out var boss) ? boss : null;
    }

    /// <summary>
    /// 指定エリアの出現情報をリセット
    /// </summary>
    public static void ClearArea(FieldAreaDataSO area)
    {
        spawnedMonsters.Remove(area);
        spawnedBosses.Remove(area);
    }

    /// <summary>
    /// 全エリアの出現情報をリセット（デバッグ用）
    /// </summary>
    public static void ClearAll()
    {
        spawnedMonsters.Clear();
        spawnedBosses.Clear();
    }
}