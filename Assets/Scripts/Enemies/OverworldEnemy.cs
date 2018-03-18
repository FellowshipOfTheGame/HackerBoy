using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OverworldEnemy : MonoBehaviour {

	public EnemyBase[] enemyParty { get; private set; }

	// FIXME: Move to an enemy spawner
	// private MapInformation mapInfo; // Need to know which enemies can appear in this map

	void Start(){
		/* DEBUG */
		// Fixed enemies for test purposes
		// enemyParty[0] = Enemy0;
		// enemyParty[1] = Enemy1;
		// enemyParty[2] = Enemy2;
		// enemyParty[3] = Enemy3;
		/* ENDDEBUG */
	}

	void Update(){
		
	}
}