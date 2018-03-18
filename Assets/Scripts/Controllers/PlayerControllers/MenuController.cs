using UnityEngine;
using System.Collections;

public class MenuController : PlayerController {

	// Used so each button press will move the cursor only once
	private bool hAxisPressed = false;
	private bool vAxisPressed = false;

	private MenuManager mm;

	public MenuController(MenuManager mm){
		this.mm = mm;
	}

	// Axes
	public override void Horizontal(){

		// if(Input.GetAxis("Horizontal") > 0){
		// 	Debug.Log("Input: \"Horizontal axis: \"" + Input.GetAxis("Horizontal"));
		// 	mm.battleBox.MoveCursorHorizontal(BattleBoxManager.RIGHT);
		// 	hAxisPressed = true;

		// } else if(Input.GetAxis("Horizontal") < 0){
		// 	Debug.Log("Input: \"Horizontal axis: \"" + Input.GetAxis("Horizontal"));
		// 	mm.battleBox.MoveCursorHorizontal(BattleBoxManager.LEFT);
		// 	hAxisPressed = true;
		// }
	}

	public override void Vertical(){

		// if(Input.GetAxis("Vertical") > 0){
		// 	Debug.Log("Input: \"Vertical axis: \"" + Input.GetAxis("Vertical"));
		// 	mm.battleBox.MoveCursorVertical(BattleBoxManager.UP);
		// 	vAxisPressed = true;

		// } else if(Input.GetAxis("Vertical") < 0){
		// 	Debug.Log("Input: \"Vertical axis: \"" + Input.GetAxis("Vertical"));
		// 	mm.battleBox.MoveCursorVertical(BattleBoxManager.DOWN);
		// 	vAxisPressed = true;

		// }
	}

	public override void Idle(){
		vAxisPressed = false;
		hAxisPressed = false;
	}

	// Buttons
	public override void Action(){}
	public override void ActionRelease(){}
	
	public override void AltAction(){}
	public override void AltActionRelease(){}

	public override void Cancel(){}
	public override void CancelRelease(){}

	public override void Start(){}
	public override void StartRelease(){}
}
