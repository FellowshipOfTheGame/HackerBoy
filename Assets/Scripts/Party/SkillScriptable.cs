using UnityEngine;
using System;

public enum SkillColor { Red, Blue, Green, Yellow }

[CreateAssetMenu(fileName="Skill", menuName="Scriptable/Skill", order=1)]
public class SkillScriptable : ScriptableObject {

	[UnityEngine.SerializeField] private string skillName;
	[UnityEngine.SerializeField] private string skillDescription;
	public bool usableInBattle = true;
	public bool usableInOverworld = true;
	public int power;
	public float accuracy;
	public float effectChance;
	public StatusEffect effect;
	public SkillColor color;
}
