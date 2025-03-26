using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedMonster
{
    public string monsterId; // 対応する MonsterDataSO の ID
    public string fieldAreaId; // 出現しているフィールドエリア
    public int currentHP;
    public bool isBoss;
    public float elapsedTime; // 出現からの経過時間（必要なら）
}