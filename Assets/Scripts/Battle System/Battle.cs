using UnityEngine;
using System.Collections;

// FIXME: How to model battle??
public /*static*/ class Battle : MonoBehaviour {

	private bool playerTurn = true;
	private int[] playerPos = new int[4];
	private int[] enemyPos = new int[4];

	void Start(){

	}

	void Update(){

		if(playerTurn){

		} else {

		}
	}

	// Make a function call (static class) or create a new Battle object?
	// How to know which enemies to spawn in each area?
	public void StartBattle(){

	}

	public void EndTurn(){ playerTurn = !playerTurn; }
	public void Run(){}
}