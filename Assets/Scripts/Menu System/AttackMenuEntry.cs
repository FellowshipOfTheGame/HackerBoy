using UnityEngine;
using UnityEngine.UI;
using System;

/*
	This menu entry is used to perform an attack in battle
*/
public class AttackMenuEntry : MenuEntry {

	private int selectedEnemy;
	private GameObject cursor;
	private RectTransform cursorTransform;
	private BattleManager bm;
	private MenuController mc;

	void Start(){
		this.bm = GameObject.Find("BattleManager").GetComponent<BattleManager>();
	}

	void Update(){

		// If menu controller's input is blocked, the player is selecting the 
		// target to attack - so THIS input is active while the other is blocked
		if(mc.blockInput){
			if(Input.GetAxis("Horizontal") > 0 || 
				Input.GetAxis("Vertical") < 0) {
				PositionCursor((++selectedEnemy)%bm.enemies.Length);

			} else if(Input.GetAxis("Horizontal") < 0 || 
				Input.GetAxis("Vertical") > 0) {
				PositionCursor((--selectedEnemy)%bm.enemies.Length);
			}

			if(Input.GetButtonDown("Action")){
				bm.PushAction(, selectedEnemy);

			} else if(Input.GetButtonDown("Cancel")){
				GameObject.Destroy(cursor);
				cursorTransform = null;
				mc.blockInput = false;
			}
		}
	}

	public override void Action(MenuController mc){
		this.mc = mc;
		mc.blockInput = true; // Activating this means player's selecting target
		selectedEnemy = 0;
		
		// Create and position cursor at first enemy
		cursor = mc.mm.CreateCursor();
		cursorTransform = cursor.GetComponent<RectTransform>();
		PositionCursor(0);
	}

	private void PositionCursor(int index){
		var op = bm.enemies[index];

		// Move cursor to center of option and subtract half width + extra
		float y = bm.enemies[index].transform.position.y;
		float x = bm.enemies[index].transform.position.x - 
					bm.enemies[index].GetComponent<RectTransform>().rect.width/2 - 
					cursorTransform.rect.width/2;

		cursorTransform.position = new Vector2(x, y);
	}
}