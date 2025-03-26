using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedMonster
{
    public MonsterData monster; // 対応する MonsterDataSO の ID
    public FieldAreaDataSO fieldArea; // 出現しているフィールドエリア
    public bool isBoss;
}