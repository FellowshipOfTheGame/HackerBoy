using UnityEngine;
using System;

public class EquipStats {
    public int str, dex, agi, intt, wis, luk; // Change these?
}

public enum EquipType { Weapon, Armor, Head, Boot } // Change these?

[CreateAssetMenu(fileName="Equipment", menuName="Scriptable/Equipment", order=1)]
public class Equipment : Item {

    public EquipType equipType;
    public EquipStats stats;
    
}
