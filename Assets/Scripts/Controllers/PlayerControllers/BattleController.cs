using UnityEngine;
using System.Collections;

public class BattleController : PlayerController {

	// Used so each button press will move the cursor only once
	private bool hAxisPressed = false;
	private bool vAxisPressed = false;

	private BattleManager bm;

	public BattleController(BattleManager bm){
		this.bm = bm;
		Debug.Log("New battle controller created");
	}

	// Axes
	public override void Horizontal(){
		// Do nothing if its not player's phase
		if(bm.currentState != BattleManager.BattleState.PLAYER_CHOICE ||
			hAxisPressed) return;

		if(Input.GetAxis("Horizontal") > 0){
			Debug.Log("Input: \"Horizontal axis: \"" + Input.GetAxis("Horizontal"));
			bm.battleBox.MoveCursorHorizontal(BattleBoxManager.RIGHT);
			hAxisPressed = true;

		} else if(Input.GetAxis("Horizontal") < 0){
			Debug.Log("Input: \"Horizontal axis: \"" + Input.GetAxis("Horizontal"));
			bm.battleBox.MoveCursorHorizontal(BattleBoxManager.LEFT);
			hAxisPressed = true;
		}
	}

	public override void Vertical(){
		// Do nothing if its not player's phase
		if(bm.currentState != BattleManager.BattleState.PLAYER_CHOICE ||
			vAxisPressed) return;

		if(Input.GetAxis("Vertical") > 0){
			Debug.Log("Input: \"Vertical axis: \"" + Input.GetAxis("Vertical"));
			bm.battleBox.MoveCursorVertical(BattleBoxManager.UP);
			vAxisPressed = true;

		} else if(Input.GetAxis("Vertical") < 0){
			Debug.Log("Input: \"Vertical axis: \"" + Input.GetAxis("Vertical"));
			bm.battleBox.MoveCursorVertical(BattleBoxManager.DOWN);
			vAxisPressed = true;

		}
	}

	public override void Idle(){
		vAxisPressed = false;
		hAxisPressed = false;
	}

	// Buttons
	public override void Action(){
		// Do nothing if its not player's phase
		if(bm.currentState != BattleManager.BattleState.PLAYER_CHOICE)
			return;
		Debug.Log("Input: \"Action\"");
	}
	public override void ActionRelease(){}
	
	public override void AltAction(){}
	public override void AltActionRelease(){}

	public override void Cancel(){
		// Do nothing if its not player's phase
		if(bm.currentState != BattleManager.BattleState.PLAYER_CHOICE)
			return;

		Debug.Log("Input: \"Cancel\"");
	}

	public override void CancelRelease(){}

	public override void Start(){
		// Do nothing if its not player's phase
		if(bm.currentState != BattleManager.BattleState.PLAYER_CHOICE)
			return;
		Debug.Log("Input: \"Start\"");
	}
	public override void StartRelease(){}
}
