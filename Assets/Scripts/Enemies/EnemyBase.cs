using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EnemyBase : MonoBehaviour {

	public readonly string charName;
	public int level;
	public int life;
	public int hackerPoints;
	public int exp;
	public Stats stats;

	private List<Skill> skills;
	public int position;

	private void ChooseAction(){

	}
}