using UnityEngine;
using System;

public enum ItemType { Consumable, Equipment }

public abstract class Item : ScriptableObject {

    new public string name; // Hides GameObject name
    public string description;
    public Sprite sprite;
    public ItemType type;
}
