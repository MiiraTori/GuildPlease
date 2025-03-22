using UnityEngine;

/// <summary>
/// 1対1の戦闘を管理するシステム
/// </summary>
public class BattleSystem : MonoBehaviour
{
    /// <summary>
    /// 戦闘を開始します。
    /// </summary>
    /// <param name="adventurer">冒険者のステータス</param>
    /// <param name="monster">モンスターのステータス</param>
    public void StartBattle(CharacterStats adventurer, CharacterStats monster)
    {
        Debug.Log("戦闘開始！");
        Debug.Log($"冒険者 HP: {adventurer.currentHP}/{adventurer.maxHP}");
        Debug.Log($"モンスター HP: {monster.currentHP}/{monster.maxHP}");

        // 戦闘ループ
        while (adventurer.currentHP > 0 && monster.currentHP > 0)
        {
            // 冒険者の攻撃
            PerformAttack(adventurer, monster);

            // モンスターが生存していれば反撃
            if (monster.currentHP > 0)
            {
                PerformAttack(monster, adventurer);
            }
        }

        // 戦闘結果
        if (adventurer.currentHP > 0)
        {
            Debug.Log("冒険者の勝利！");
        }
        else
        {
            Debug.Log("モンスターの勝利...");
        }
    }

    /// <summary>
    /// 攻撃を実行します。
    /// </summary>
    /// <param name="attacker">攻撃者のステータス</param>
    /// <param name="defender">防御者のステータス</param>
    private void PerformAttack(CharacterStats attacker, CharacterStats defender)
    {
        int damage = Mathf.Max(0, attacker.attack - defender.defense);
        defender.currentHP -= damage;
        defender.currentHP = Mathf.Clamp(defender.currentHP, 0, defender.maxHP);

        Debug.Log($"{attacker} が {defender} に {damage} ダメージを与えた！");
        Debug.Log($"{defender} の残りHP: {defender.currentHP}/{defender.maxHP}");
    }
}