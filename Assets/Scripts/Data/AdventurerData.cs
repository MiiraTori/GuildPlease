using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.ParticleSystem;

[System.Serializable]
public class AdventurerData
{
    public string adventurerId;
    public string displayName;
    public string job;
    public int level;
    public AdventurerStats stats;
    public List<ItemData> inventory;
}

[CreateAssetMenu(menuName = "Data/AdventurerData", fileName = "NewAdventurerData")]
public class AdventurerDataSO : ScriptableObject
{
    public string adventurerId;
    public string displayName;
    public string job;
    public int level;
    public AdventurerStats stats;
    public List<ItemDataSO> inventory;

    public AdventurerData CreateAdventurerInstance()
    {
        return new AdventurerData
        {
            adventurerId = this.adventurerId,
            displayName = this.displayName,
            job = this.job,
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
            inventory = this.inventory.ConvertAll(itemSO => itemSO.CreateItemInstance())
        };
    }
}