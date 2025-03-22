using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MonsterData
{
    public string monsterId;
    public string displayName;
    public int level;
    public AdventurerStats stats;
    public List<ItemData> dropItems; // ← 修正！
    public List<string> traits;
}

[CreateAssetMenu(menuName = "Data/MonsterData", fileName = "NewMonsterData")]
public class MonsterDataSO : ScriptableObject
{
    public string monsterId;
    public string displayName;
    public int level;
    public AdventurerStats stats;
    public List<ItemDataSO> dropItems; // ← ScriptableObject側はSOのままでOK
    public List<string> traits;

    /// <summary>
    /// ScriptableObjectからMonsterDataのインスタンスを生成
    /// </summary>
    public MonsterData CreateMonsterInstance()
    {
        return new MonsterData
        {
            monsterId = this.monsterId,
            displayName = this.displayName,
            level = this.level,
            stats = new AdventurerStats
            {
                maxHP = this.stats.maxHP,
                currentHP = this.stats.maxHP,
                attack = this.stats.attack,
                defense = this.stats.defense,
                speed = this.stats.speed,
                maxMP = this.stats.maxMP,
                currentMP = this.stats.maxMP,
                intelligence = this.stats.intelligence,
                willpower = this.stats.willpower
            },
            traits = new List<string>(this.traits),
            dropItems = this.dropItems.ConvertAll(itemSO => itemSO.CreateItemInstance())
        };
    }
}