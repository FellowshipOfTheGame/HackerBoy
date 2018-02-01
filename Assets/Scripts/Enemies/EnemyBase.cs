using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EnemyBase : CharacterBase {

	public EnemyBase() : base() {}
	public EnemyBase(string name) : base(name) {}
	public EnemyBase(string name, int level) : base(name, level) {}

	private void ChooseAction(){

	}
}