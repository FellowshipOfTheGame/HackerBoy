using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EnemyBase : CharacterBase {

	public enum Actions { Attack, Skill }

	protected abstract void ChooseAction();
	protected override void Die(){
		
	}
}