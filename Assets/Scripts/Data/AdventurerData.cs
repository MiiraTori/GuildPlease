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
    public CharacterStats stats;
    public List<ItemData> inventory;

    public int experience;
    public int guildPoints;
    public int currentStamina;
    public int MaxStamina => 100;
    public void GainExperience(int amount)
    {
        experience += amount;

        // 経験値しきい値の計算
        int requiredExp = 100 + (level * 20); // 例：100 + 20×レベル

        // レベルアップ判定
        while (experience >= requiredExp)
        {
            experience -= requiredExp;
            level++;
            stats.maxHP += 10;
            stats.attack += 2;
            stats.defense += 1;

            Debug.Log($"✨ {displayName} がレベル {level} に上がった！");

            requiredExp = 100 + (level * 20); // 次のしきい値を再計算
        }
    }
    public void GainGuildPoints(int amount)
    {
        guildPoints += amount;
    }

    public void ConsumeStamina(int cost)
    {
        currentStamina = Mathf.Max(currentStamina - cost, 0);
    }

    public void RecoverStamina(int amount)
    {
        currentStamina = Mathf.Min(currentStamina + amount, MaxStamina);
    }

    public bool HasEnoughStamina(int cost)
    {
        return currentStamina >= cost;
    }

}
