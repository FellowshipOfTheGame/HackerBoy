using UnityEngine;
using UnityEngine.UI;
using System;

/*
	This menu entry is used to perform an attack in battle
*/
public class AttackMenuEntry : MenuEntry {

	private bool moved;
	private bool blockAction;
	private int target;
	private CharacterBase[] targets;
	private GameObject cursor;
	private RectTransform cursorTransform;
	private BattleManager bm;
	private MenuController mc;
	private Camera cam;

	void Start(){

		bm = GameObject.Find("BattleManager").GetComponent<BattleManager>();

		// Start blocking action input since the button is probably already down
		// we need to wait for player to release the button at least once before
		// starting using this input
		blockAction = true;

		// Specifically for attack, start at first enemy
		target = 0;

		// Concatenate both enemies and player in one array for easy access
		targets = new CharacterBase[bm.enemies.Length+bm.playerParty.Length];
		bm.enemies.CopyTo(targets, 0);
		bm.playerParty.CopyTo(targets, bm.enemies.Length);
	}

	void Update(){

		// If menu controller's input is blocked, the player is selecting the 
		// target to attack - so THIS input is active while the other is blocked
		if(mc != null && mc.blockInput){

			float x = Input.GetAxisRaw("Horizontal");
			float y = Input.GetAxisRaw("Vertical");

			/* Axis movement */
			// Right or down press
			if((x > 0 || y < 0) && !moved) {

				target = (++target)%targets.Length;
				PositionCursor(target);
				moved = true;

			// Left or up press
			} else if((x < 0 || y > 0) && !moved) {

				if(--target < 0) target = targets.Length-1;
				else target = target%targets.Length;
				PositionCursor(target);
				moved = true;
			
			// Idle
			} else if(x == 0 && y == 0)
				moved = false;

			/* Button presses */
			if(Input.GetButtonDown("Action") && !blockAction){
				bm.PushAction(bm.playerParty[bm.currentCharacter].Attack, 
					targets[target]);
				bm.FinishAction();
				FinishAction();

			} else if(Input.GetButtonDown("Cancel"))
				FinishAction();

			if(Input.GetButtonUp("Action"))
				blockAction = false;
		}
	}

	public override void Action(MenuController mc){
		this.mc = mc;
		mc.blockInput = true; // Activating this means player's selecting target
		
		// Create and position cursor at first enemy
		cursor = mc.mm.CreateCursor();
		cursorTransform = cursor.GetComponent<RectTransform>();

		// NOTE: For now, cursor always has memory since i never reset target 
		// index. If an option should exist to disable memory need to reset this
		PositionCursor(target); 
	}

	public void FinishAction(){
		mc.blockInput = false;
		mc = null;
		blockAction = true;

		// Remove cursor
		GameObject.Destroy(cursor);
		cursorTransform = null;
	}

	// If index is 0 - 4, index playerParty, else if 5 - 8 index enemies
	private void PositionCursor(int index){

		if(index < 0 || index >= targets.Length) 
			throw new Exception("Invalid cursor index \"" + index + "\"");

		this.cam = GameObject.Find("Main Camera").GetComponent<Camera>();
		Vector3 camPos;

		camPos = cam.WorldToScreenPoint(targets[index].transform.position);

		// Move cursor to center of option and subtract half width + extra
		float y = camPos.y;
		float x = camPos.x - cursorTransform.rect.width/2;
		// targets[index].gameObject.GetComponent<SpriteRenderer>().bounds.size.x/2 - 

		cursorTransform.position = new Vector2(x, y);
	}
}