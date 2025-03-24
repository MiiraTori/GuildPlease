using System.Collections.Generic;
using UnityEngine;

public class FieldAreaState
{
    public string areaId;
    public List<MonsterData> activeMonsters = new List<MonsterData>();

    /// <summary>
    /// エリアデータに基づいてモンスターを初期化する
    /// </summary>
    public void Initialize(FieldAreaDataSO areaData)
    {
        areaId = areaData.areaId;
        activeMonsters.Clear();

        foreach (var spawn in areaData.possibleMonsters)
        {
            // 出現確率に応じてスポーン
            if (Random.value <= spawn.spawnRate)
            {
                MonsterData monster = spawn.CreateMonsterInstance();
                activeMonsters.Add(monster);

                Debug.Log($"🟢 モンスター出現: {monster.displayName} (エリア: {areaId})");
            }
        }
    }

    /// <summary>
    /// ランダムなモンスターを1体返す（いない場合は null）
    /// </summary>
    public MonsterData GetRandomMonster()
    {
        if (activeMonsters.Count == 0) return null;
        int index = Random.Range(0, activeMonsters.Count);
        return activeMonsters[index];
    }

    /// <summary>
    /// モンスターを倒した処理。該当のモンスターをリストから削除
    /// </summary>
    public void OnMonsterDefeated(string monsterId)
    {
        var defeated = activeMonsters.Find(m => m.monsterId == monsterId);
        if (defeated != null)
        {
            activeMonsters.Remove(defeated);
            Debug.Log($"⚔️ モンスター撃破: {defeated.displayName} (ID: {monsterId})");
        }
    }

    /// <summary>
    /// モンスターの残数チェック
    /// </summary>
    public bool HasMonsters()
    {
        return activeMonsters.Count > 0;
    }
}