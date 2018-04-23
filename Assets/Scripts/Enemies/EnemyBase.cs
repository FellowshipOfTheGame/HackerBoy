using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[UnityEngine.SerializeField]
public abstract class EnemyBase : CharacterBase {

	protected BattleManager bm;

	void Start(){
		bm = GameObject.Find("BattleManager").GetComponent<BattleManager>();
		bars = GetComponentInChildren<HpMpBarManager>();
		currentHp = calculatedStats.hp;
		currentMp = calculatedStats.mp;
	}

	public abstract Pair<BattleManager.BattleAction, CharacterBase> ChooseAction();
	protected override void Die(){
		status = StatusEffect.Dead;
		GameObject.Find("BattleManager")
			.GetComponent<BattleManager>()
			.OnEnemyDeath(this);
	}
}