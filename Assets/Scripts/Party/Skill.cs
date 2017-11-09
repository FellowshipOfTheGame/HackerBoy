using UnityEngine;
using System.Collections;

// public class Skill : ScriptableObject {
public abstract class Skill {

	private SkillScriptable skill;

	public abstract void BattleUse();
	public abstract void OverworldUse();
}