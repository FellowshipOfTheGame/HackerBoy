using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class ScrollMenu : Menu {

	private bool vAxisPressed, hAxisPressed;
	// private ScrollView view;
	private bool vertical;
	private ScrollRect box;

	public ScrollMenu(
		bool vertical,
		GameObject cursor, 
		GameObject[] entries,
		string name,
		Sprite menuImage,
		Font font,
		float width = 540, 
		float height = 135,
		float posx = 0,
		float posy = 0)
	: base(cursor, height, width, posx, posy, name, menuImage, font){
		
		this.vertical = vertical;
		this.box.vertical = vertical;
		this.box.horizontal = !vertical;

	}

	public override void CloseMenu(){

	}

	public override void Idle(){
		vAxisPressed = false;
		hAxisPressed = false;
	}
	
	public override void Action(){}
	
	public override void MoveCursorHorizontal(float direction){

	}
	
	public override void MoveCursorVertical(float direction){

	}
	
}
