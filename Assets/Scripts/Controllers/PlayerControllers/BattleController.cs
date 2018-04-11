// using UnityEngine;
// using System.Collections;

// NOTE REFACTORED INTO MenuController.cs!!!
// public class BattleController : PlayerController {

// 	// Used so each button press will move the cursor only once
// 	private bool hAxisPressed = false;
// 	private bool vAxisPressed = false;

// 	private BattleManager bm;

// 	public BattleController(BattleManager bm){
// 		this.bm = bm;
// 		Debug.Log("New battle controller created");
// 	}

// 	// Axes
// 	public override void Horizontal(float axisValue){
// 		// Do nothing if its not player's phase
// 		if(bm.currentState != BattleManager.BattleState.PLAYER_CHOICE ||
// 			hAxisPressed) return;

// 		if(axisValue > 0){
// 			bm.battleBox.MoveCursorHorizontal(BattleBoxManager.RIGHT);
// 			hAxisPressed = true;

// 		} else if(axisValue < 0){
// 			bm.battleBox.MoveCursorHorizontal(BattleBoxManager.LEFT);
// 			hAxisPressed = true;
// 		}
// 	}

// 	public override void Vertical(float axisValue){
// 		// Do nothing if its not player's phase
// 		if(bm.currentState != BattleManager.BattleState.PLAYER_CHOICE ||
// 			vAxisPressed) return;

// 		if(axisValue > 0){
// 			bm.battleBox.MoveCursorVertical(BattleBoxManager.UP);
// 			vAxisPressed = true;

// 		} else if(axisValue < 0){
// 			bm.battleBox.MoveCursorVertical(BattleBoxManager.DOWN);
// 			vAxisPressed = true;

// 		}
// 	}

// 	public override void Idle(){
// 		vAxisPressed = false;
// 		hAxisPressed = false;
// 	}

// 	// Buttons
// 	public override void Action(){
// 		// Do nothing if its not player's phase
// 		if(bm.currentState != BattleManager.BattleState.PLAYER_CHOICE)
// 			return;
// 	}
// 	public override void ActionRelease(){}
	
// 	public override void AltAction(){}
// 	public override void AltActionRelease(){}

// 	public override void Cancel(){
// 		// Do nothing if its not player's phase
// 		if(bm.currentState != BattleManager.BattleState.PLAYER_CHOICE)
// 			return;
// 	}

// 	public override void CancelRelease(){}

// 	public override void Start(){
// 		// Do nothing if its not player's phase
// 		if(bm.currentState != BattleManager.BattleState.PLAYER_CHOICE)
// 			return;
// 	}
// 	public override void StartRelease(){}
// }
