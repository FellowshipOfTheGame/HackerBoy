using UnityEngine;
using System;
using System.Collections;

public class MenuController : PlayerController {

	public bool blockInput = false;

	// Used so each button press will move the cursor only once
	private bool hAxisPressed = false;
	private bool vAxisPressed = false;

	public MenuManager mm { get; private set; }

	public MenuController(MenuManager mm){
		this.mm = mm;
	}

	// Axes
	public override void Horizontal(float axisValue){

		if(blockInput) return;

		try {
			
			Menu menu = this.mm.GetCurrentMenu();
			if(menu != null && menu.isActive) menu.MoveCursorHorizontal(axisValue);

		} catch(NullReferenceException){ /*Debug.Log("Theres no open menu");*/ }
		catch(InvalidOperationException){ /*Debug.Log("Theres no open menu");*/ }
	}

	public override void Vertical(float axisValue){
		
		if(blockInput) return;

		try {
			
			Menu menu = this.mm.GetCurrentMenu();
			if(menu != null && menu.isActive) menu.MoveCursorVertical(axisValue);

		} catch(NullReferenceException){ /*Debug.Log("Theres no open menu");*/ }
		catch(InvalidOperationException){ /*Debug.Log("Theres no open menu");*/ }
	}

	public override void Idle(){
		vAxisPressed = false;
		hAxisPressed = false;
	}

	// Buttons
	public override void Action(){
		
		if(blockInput) return;

		try {
			
			Menu menu = mm.GetCurrentMenu();
			if(menu != null && menu.isActive) menu.Action(this);

		} catch(InvalidOperationException){ // No menu is open, stack is empty
			/* Debug */
			mm.DebugMenu();
		}
	}
	public override void ActionRelease(){}
	
	public override void AltAction(){}
	public override void AltActionRelease(){}

	public override void Cancel(){

		if(blockInput) return;

		try {
			mm.CloseMenu();
		} catch(NullReferenceException){} // No menu open
		catch(InvalidOperationException){} // No menu open
	}
	public override void CancelRelease(){}

	public override void Start(){}
	public override void StartRelease(){}
}
