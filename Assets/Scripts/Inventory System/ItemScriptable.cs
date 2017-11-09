using UnityEngine;
using System;

public enum ItemType { Consumable, Equipment }

public abstract class ItemScriptable : ScriptableObject {

    public readonly string itemName;
    public readonly Sprite sprite;
    public readonly ItemType type;
    
}
