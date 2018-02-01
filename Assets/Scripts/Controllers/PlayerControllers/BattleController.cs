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

	public override void Horizontal(){
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

	public override void Action(){
		if(bm.currentState != BattleManager.BattleState.PLAYER_CHOICE)
			return;

		Debug.Log("Input: \"Action\"");

	}

	public override void Cancel(){
		if(bm.currentState != BattleManager.BattleState.PLAYER_CHOICE)
			return;

		Debug.Log("Input: \"Cancel\"");
	}

	public override void Start(){
		if(bm.currentState != BattleManager.BattleState.PLAYER_CHOICE)
			return;

		Debug.Log("Input: \"Start\"");

	}
}
