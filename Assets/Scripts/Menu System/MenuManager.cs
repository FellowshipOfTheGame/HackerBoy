using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour {

	public Sprite black;
	public Menu activeMenu { get; private set; }
	public GameObject cursorPrefab;
	public static Transform canvas;
	
	private Stack<Image> fade;
	private Stack<Menu> menuStack;

	void Start(){

		// Get a static reference for easy access
		canvas = GameObject.Find("Canvas").transform;

		// Black image to create a fade effect for objects behind menu
		fade = new Stack<Image>();

		// Menu stack to hold reference to multiple menus (only top is active)
		menuStack = new Stack<Menu>();
		DontDestroyOnLoad(this.gameObject);
	}

	void Update(){}

	public void OpenMenu(Menu menu){

		Image img = new Image(black).color.a = 0.1f;
		menuStack.Push(menu);
		fade.Push(img)
	}

	// Closes current active menu
	public void CloseMenu(){

	}

	// Clears menu stack
	public void CloseAllMenus(){
		// foreach(Menu menu in menuStack){
		// 	menu.close();
		// }
	}

	public GameObject CreateCursor(){
		return GameObject.Instantiate(cursorPrefab, canvas);
	}

	/**********************************************************/
	/**********************************************************/
	public void DebugMenu(){
		// Create cursor
		GameObject cursor = CreateCursor();

		// Create options for the menu
		GameObject[] options = new GameObject[7];
		for(int i = 0; i < 7; i++){
			options[i] = new GameObject();
			options[i].name = "option" + (i+1);
			options[i].AddComponent<Text>();
			options[i].GetComponent<Text>().text = "option" + (i+1);
		}

		this.OpenMenu(new OptionsMenu(
						// width: ??, height: ??,
						cursor: cursor,
						options: options,
						name: "debug_menu",
						rows: 3, cols: 3)
					);
	}
}
