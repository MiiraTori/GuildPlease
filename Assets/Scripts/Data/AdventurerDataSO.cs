using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/AdventurerDataSO", fileName = "NewAdventurerDataSO")]
public class AdventurerDataSO : ScriptableObject
{
 
    public string adventurerId;
    public string displayName;
    public string job;
    public int level;
    public CharacterStats stats;
    public List<ItemData> inventory;

    public int experience;
    public int guildPoints;
    public int currentStamina;

    public void GainExperience(int amount)
    {
        experience += amount;
    }

    /// <summary>
    /// 実行時用の AdventurerData インスタンスを生成
    /// </summary>
    public AdventurerData CreateAdventurerInstance()
    {
        return new AdventurerData
        {
            adventurerId = this.adventurerId,
            displayName = this.displayName,
            job = this.job,
            level = this.level,
            stats = this.stats,
            experience = this.experience,
            guildPoints = this.guildPoints,
            inventory = new List<ItemData>(this.inventory),
            currentStamina = this.currentStamina

    };
    }
}