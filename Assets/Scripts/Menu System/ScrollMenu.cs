using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class ScrollMenu : Menu {

	// private ScrollView view;
	private List<GameObject> entries;
	private bool vertical;
	private ScrollRect box;

	public ScrollMenu(
		bool vertical,
		float width,
		float height,
		GameObject cursor, 
		GameObject[] entries,
		string name)
	: base(cursor, height, width, name){
		
		this.vertical = vertical;
		this.box.vertical = vertical;
		this.box.horizontal = !vertical;

	}

	public override void CloseMenu(){

	}
	
	public override void MoveCursorHorizontal(float direction){

	}
	
	public override void MoveCursorVertical(float direction){

	}
	
}
