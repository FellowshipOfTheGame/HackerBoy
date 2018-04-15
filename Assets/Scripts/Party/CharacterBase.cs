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
	public int hp, mp, str, dex, agi, intt, wis, luk;
}

public struct StatsGrowth {
	public float hp, mp, str, dex, agi, intt, wis, luk;

	public StatsGrowth(float hp, float mp, float str, float dex, 
						float agi, float intt, float wis, float luk){
		this.hp = hp;
		this.mp = mp;
		this.str = str;
		this.dex = dex;
		this.agi = agi;
		this.intt = intt;
		this.wis = wis;
		this.luk = luk;
	}
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

	// Custom display values
	public string charName;
	public string expName;
	public Sprite portrait;
	private GameObject barScaler;
	private HpMpBarManager bars;

	// Stats
	public int currentHp;
	public int currentMp;
	public Stats stats;
	public StatsGrowth growths;
	public StatusEffect status;

	// Equipped Equipment and inventory
	public CharacterEquipment equipment;
	private Inventory inventory;
	
	// Exp and levelling
	public int expToNextLevel = 100;
	public int exp { get; private set; }

	private int _level;
	public int level {
		get { return this._level; }
		private set {
			// Check value range?
			this._level = value;
			LevelUp(this.level);
		}
	}

	// Skills
	private List<Skill> skills; // FIXME: how to model skill combo system?
	// In-battle only 
	public List<SkillColor> lastSkills; //NOTE: reset every battle

	void Start(){
		bars = GameObject.GetComponentInChildren<HpMpBarManager>();
	}

	public void IncrementHp(int value){
		// this.
		if(!bars) bars.UpdateHp();
	}

	public void IncrementMp(int value){

		if(!bars) bars.UpdateMp();
	}

	public void IncrementExp(int value){
		exp += value;
		if(exp >= expToNextLevel){
			exp -= expToNextLevel;
			LevelUp();
		}
	}

	// Level only one level
	protected void LevelUp(){
		
		// Increment level
		level++;

		// Update next level exp
		expToNextLevel *= 2; // TODO: probably need rebalance this

		stats.hp += Mathf.RoundToInt(Random.Range(0f, growths.hp));
		stats.mp += Mathf.RoundToInt(Random.Range(0f, growths.mp));
		stats.str += Mathf.RoundToInt(Random.Range(0f, growths.str));
		stats.dex += Mathf.RoundToInt(Random.Range(0f, growths.dex));
		stats.agi += Mathf.RoundToInt(Random.Range(0f, growths.agi));
		stats.intt += Mathf.RoundToInt(Random.Range(0f, growths.intt));
		stats.wis += Mathf.RoundToInt(Random.Range(0f, growths.wis));
		stats.luk += Mathf.RoundToInt(Random.Range(0f, growths.luk));
	}

	// Level up to targetLevel
	protected void LevelUp(int targetLevel){
		for (int i = 0; i < targetLevel; i++)
			LevelUp();
	}
}
