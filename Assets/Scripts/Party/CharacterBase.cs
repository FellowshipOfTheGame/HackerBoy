using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public enum StatusEffect { 
	Normal, 
	Spammed, // Paralyzed
	Poisoned, // XXX
	Hacked, // Charmed
	Asleep, // XXX
	Dead
}

public struct Stats {
	public int str, dex, agi, intt, wis, luk, mov;
}

public struct CharacterEquipment {
	public Equipment head;
	public Equipment body;
	public Equipment weapon;
	public Equipment legs;
	public Equipment accessory1;
	public Equipment accessory2;
}

public class CharacterBase : MonoBehaviour {
	
	public /*readonly*/ string charName;
	public int level;
	public int life;
	public int mp; // NOTE: maybe change this name
	public readonly string expName;
	public int exp;
	public Sprite portrait;
	public Stats stats;
	public StatusEffect status;
	public CharacterEquipment equipment;

	private List<Skill> skills; // FIXME: how to model skill combo system?
	// In-battle only 
	public List<SkillColor> lastSkills; //NOTE: reset every battle
	// public int position; // NOTE: scrapped

	public CharacterBase(){}
	public CharacterBase(string name){

	}

	public CharacterBase(string name, int level){

	}
}
