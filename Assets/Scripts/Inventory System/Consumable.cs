using UnityEngine;
using System;

[CreateAssetMenu(fileName="Consumable", menuName="Scriptable/Consumable", order=1)]
public class Consumable : Item {

    // FIXME How to model item effectiveness?
    public int effectValue;
    public int quantity;

    public void Use(){

    }
}
