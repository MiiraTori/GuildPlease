using UnityEngine;

public class MonsterSpawnerSystem : MonoBehaviour
{
    public TimeManager timeManager;
    public FieldAreaListComponent fieldAreaList;
    public MonsterListComponent monsterList;

    void Start()
    {
        timeManager.OnNewDay += TrySpawnMonsters;
    }

    void TrySpawnMonsters()
    {
        foreach (var fieldArea in fieldAreaList.fieldAreas)
        {
            var spawnedComponent = fieldArea.GetComponent<SpawnedMonsterComponent>();
            int currentCount = spawnedComponent.activeMonsters.Count;

            // 上限に達していればスキップ
            if (currentCount >= fieldArea.maxMonsterCount) continue;

            // 通常モンスターの抽選
            foreach (var spawnData in fieldArea.spawnableMonsters)
            {
                if (Random.value <= spawnData.spawnRate)
                {
                    var monster = CreateMonster(spawnData.monsterId, fieldArea.id, false);
                    spawnedComponent.AddMonster(monster);
                }
            }

            // ボスの抽選
            foreach (var bossData in fieldArea.possibleBosses)
            {
                if (Random.value <= bossData.spawnChance)
                {
                    var boss = CreateMonster(bossData.monsterId, fieldArea.id, true);
                    spawnedComponent.AddMonster(boss);
                }
            }
        }
    }

    SpawnedMonster CreateMonster(string monsterId, string fieldAreaId, bool isBoss)
    {
        return new SpawnedMonster
        {
            monsterId = monsterId,
            fieldAreaId = fieldAreaId,
            currentHP = monsterList.GetData(monsterId).health,
            isBoss = isBoss,
            elapsedTime = 0f
        };
    }
}