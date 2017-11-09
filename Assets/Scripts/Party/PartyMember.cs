using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public enum StatusEffect { 
	Normal, 
	Spammed, // Paralyzed
	Poisoned, // XXX
	Hacked, // Charmed
	Asleep // XXX
}

public struct Stats {
	public int str, dex, agi, intt, wis, luk, mov;
}

public class PartyMember : MonoBehaviour {
	
	public readonly string charName;
	public int level;
	public int life;
	public int hackerPoints;
	public readonly string expName;
	public int exp;
	public Sprite portrait;
	public Stats stats;
	public StatusEffect status;

	private List<Skill> skills; // FIXME: how to model skill combo system?
	// In-battle only 
	public List<SkillColor> lastSkills; //NOTE: reset every battle
	public int position;

	public PartyMember(){}
	public PartyMember(string name){

	}

	public PartyMember(string name, int level){

	}
}
