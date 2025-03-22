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
