using System.Collections;
using System.Collections.Generic;
using GameData;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ItemData", fileName = "NewItemData")]
public class ItemDataSO : ScriptableObject
{
    public string itemId;
    public string itemName;
    public ItemType type;
    public int effectValue;
    public bool stackable;
    public Sprite icon;

    public ItemData CreateItemInstance()
    {
        return new ItemData
        {
            itemId = this.itemId,
            itemName = this.itemName,
            type = this.type,
            effectValue = this.effectValue,
            stackable = this.stackable,
            icon = this.icon
        };
    }
}

