using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string itemId;
    public string itemName;
    public ItemType type;
    public int effectValue;
    public bool stackable;
    public Sprite icon;
}

