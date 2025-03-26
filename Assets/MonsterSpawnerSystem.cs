using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
/// モンスターの生存管理
/// </summary>
public class MonsterSpawnerSystem : MonoBehaviour
{
    public TimeManager timeManager;
    public FieldAreaListComponent fieldAreaList;
    public MonsterListComponent monsterList;

    // ゲーム内で出現しているモンスター
    public List<SpawnedMonster> spawnedMonster;
    

    private void Start()
    {
        // TimeManager の初期化が確実に終わった後に登録
        TrySubscribeToTimeManager();
    }

    private void OnEnable()
    {
        TrySubscribeToTimeManager();
    }

    private void OnDisable()
    {
        if (timeManager != null )
        {
            timeManager.OnHourChanged -= HandleHourChange;
        
        }
    }

    private void Awake()
    {
           fieldAreaList = GameObject.FindObjectOfType<FieldAreaListComponent>();
      monsterList = GameObject.FindObjectOfType<MonsterListComponent>();

    }
    
    private void TrySubscribeToTimeManager()
    {
        if (timeManager != null)
        {
            timeManager.OnHourChanged -= HandleHourChange;
            timeManager.OnHourChanged += HandleHourChange;
        }
    }

    private void HandleHourChange(int newHour)
    {
        if (newHour == 6)
        {
            TrySpawnMonsters();
        }
    }

    /// <summary>
    /// モンスターの出現
    /// </summary>
    private void TrySpawnMonsters()
    {
        // 全エリアの確認
        foreach (var fieldArea in fieldAreaList.GetAllAreas())
        {
         
            // 該当エリアのモンスター一覧
            foreach (var monster in fieldArea.possibleMonsters)
            {
                // フィールドのモンスター数の確認　少なければOK
               
                    if (AreaSpawnedMonsterCount(fieldArea.areaId) >= fieldArea.maxMonsterCount)
                    {
                        continue; // 上限に達しているのでスキップ
                    }
                    //出現レート
                    if (Random.value < monster.spawnRate)
                    {
                        //　モンスターの出現
                        AddSpawnedMonster(fieldArea.areaId, monster.monsterId);
                    }
                


            }

            //ボスモンスターの確認
            foreach (var boss in fieldArea.possibleBosses)
            {
                // フィールドのモンスター数の確認　少なければOK

                if (fieldArea.maxMonsterCount > AreaSpawnedMonsterCount(fieldArea.areaId))
                {
                    //出現レート
                    if (Random.value < boss.spawnRate)
                    {
                        //　モンスターの出現
                        AddSpawnedMonster(fieldArea.areaId, boss.monsterId);
                    }
                }
            }

            // エリア状況のデバック報告
            Debug.Log($"[モンスターの出現状況]エリア：{fieldArea.areaName}　モンスターの数:{AreaSpawnedMonsterCount(fieldArea.areaId)}/{fieldArea.maxMonsterCount}" );
        }
    }

    /// <summary>
    /// モンスターの生成
    /// </summary>
    /// <param name="fieldAreaId">エリアID</param>
    /// <param name="monsterId">モンスターID</param>
    public void AddSpawnedMonster(string fieldAreaId,string monsterId)
    {
        SpawnedMonster monster = new SpawnedMonster();

        monster.monster = monsterList.GetMonsterById(monsterId);
        monster.fieldArea = fieldAreaList.GetAreaById(fieldAreaId);
        monster.isBoss = false;


        spawnedMonster.Add(monster);

    }

/// <summary>
/// ボスモンスターの生成
/// </summary>
/// <param name="fieldAreaId">エリアID</param>
/// <param name="monsterId">モンスターID</param>
    public void AddSpawnedBossMonster(string fieldAreaId, string monsterId)
    {
        // ボスモンスターは出現させない
        int count = 0;
        foreach (var m in spawnedMonster)
        {
            // 該当エリアの出現したモンスターの数をカウント
            if (m.fieldArea.areaId == fieldAreaId && m.isBoss == true)

                count++;
        }
        if (count != 0) return;

        //　モンスター生成（ボス用）
        SpawnedMonster monster = new SpawnedMonster();

        monster.monster = monsterList.GetMonsterById(monsterId);
        monster.fieldArea = fieldAreaList.GetAreaById(fieldAreaId);
        monster.isBoss = true; //　ボスフラグ

        spawnedMonster.Add(monster);

    }

    /// <summary>
    /// 指定エリアのモンスターの数
    /// </summary>
    /// <param name="fieldAreaId">エ</param>
    /// <returns></returns>リアID
private int AreaSpawnedMonsterCount( string fieldAreaId)
    {
        
                // 出現しても上限の場合は出現しない。
        int count = 0;
        foreach (var m in spawnedMonster)
        {
            // 該当エリアの出現したモンスターの数をカウント
            if (m.fieldArea.areaId == fieldAreaId && m.isBoss==false)

                count++;
        }


        return count;
}
    /// <summary>
    /// 現在生存しているモンスター（エリアのモンスターを１匹）
    /// エリアの上限に遭遇率を調整する。
    /// </summary>
    /// <param name="fieldAreaId"></param>
    /// <returns></returns>
    public SpawnedMonster GetMonster(string fieldAreaId)
    {
        List<SpawnedMonster> list = new List<SpawnedMonster>();
        var area = fieldAreaList.GetAreaById(fieldAreaId);

        // エリアモンスター一覧を作成

        foreach (var monster in spawnedMonster)
        {
            if(monster.fieldArea.areaId == fieldAreaId)
            {
                list.Add(monster);
            }
        }

        //エリアモンスター一覧にモンスター上限値まで空を入れる
       
        for(var i=0;i<=area.maxMonsterCount;i++ )
        {
            list.Add(null);
        }

        //　ランダムにモンスターを出現
        return list[Random.Range(1, list.Count +1)];

    }


}