using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[UnityEngine.SerializeField]
public class DebugEnemy : EnemyBase {

	public override Pair<BattleManager.BattleAction, CharacterBase> ChooseAction(){
		
		CharacterBase target = bm.playerParty[0];

		// Attack enemy with least hp
		for (int i = 1; i < bm.playerParty.Length; i++){
			if(bm.playerParty[i].status != StatusEffect.Dead && 
				bm.playerParty[i].currentHp < target.currentHp){
				target = bm.playerParty[i];
			}
		}

		return new Pair<BattleManager.BattleAction, CharacterBase>(this.Attack, target);
	}
}