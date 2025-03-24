using UnityEngine;

public class BattleTestRunner : MonoBehaviour
{
    public AdventurerDataSO adventurerSO;
    public MonsterDataSO monsterSO;
    public BattleSystem battleSystem;

    private void Start()
    {
        var adventurer = adventurerSO.CreateAdventurerInstance();
        var monster = monsterSO.CreateMonsterInstance();

        battleSystem.StartBattle(adventurer, monster);
    }
}