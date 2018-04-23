using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OverworldEnemy : MonoBehaviour {

	public EnemyBase[] enemyParty;
	public bool battled;

	// FIXME: Move to an enemy spawner
	// private MapInformation mapInfo; // Need to know which enemies can appear in this map

	void Start(){

		DontDestroyOnLoad(gameObject);

		/* DEBUG */
		// Fixed enemies for test purposes
		enemyParty = GetComponentsInChildren<EnemyBase>(true);
		/* ENDDEBUG */
	}

	void Update(){
		
	}
}