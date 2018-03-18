using UnityEngine;

public abstract class Menu {

	public static readonly float UP = 1.0f;
	public static readonly float RIGHT = 1.0f;
	public static readonly float LEFT = -1.0f;
	public static readonly float DOWN = -1.0f;

	protected readonly float X_STEP = 250f;
	protected readonly float Y_STEP = 40f;
	
	public string name;

	protected GameObject menu;
	protected GameObject cursor;
	protected float height, width;

	/*
	TODO: make this dynamic
	Row and col increments
	X = 0	Y = 0
	X = 250	Y = -40
	X = 500	Y = -80
	*/

	public Menu(GameObject cursor, float height, float width, string name){
		
		// Create a gameobject to hold all menu parts
		this.menu = new GameObject();
		this.menu.name = this.name;
		this.menu.transform.SetParent(MenuManager.canvas);

		this.cursor = cursor;
		this.height = height;
		this.width = width;
		this.name = name;
	}

	public abstract void CloseMenu();
	public abstract void MoveCursorHorizontal(float direction);
	public abstract void MoveCursorVertical(float direction);
}

