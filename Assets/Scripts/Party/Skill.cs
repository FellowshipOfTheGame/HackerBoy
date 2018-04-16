using UnityEngine;
using System.Collections;

public enum SkillColor { Red, Blue, Green, Yellow }

[CreateAssetMenu(fileName="Skill", menuName="Scriptable/Skill", order=1)]
public abstract class Skill : ScriptableObject {

	public string skillName;
	public string skillDescription;
	public bool usableInBattle = true;
	public bool usableInOverworld = true;
	public int power;
	public float accuracy;
	public float effectChance;
	public StatusEffect effect;
	public SkillColor color;

	public abstract void BattleUse();
	public abstract void OverworldUse();
}