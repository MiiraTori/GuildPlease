using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MonsterData
{
    public string monsterId;
    public string displayName;
    public int level;
    public CharacterStats stats;
    public List<ItemData> dropItems; // ← 修正！
    public List<string> traits;
}

