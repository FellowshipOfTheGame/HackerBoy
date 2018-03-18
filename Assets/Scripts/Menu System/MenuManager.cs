using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour {
	
	public static Transform canvas;

	public GameObject black;
	public Menu activeMenu { get; private set; }
	public GameObject cursorPrefab;
	
	private Stack<GameObject> fade;
	private Stack<Menu> menuStack;

	void Start(){

		// Get a static reference for easy access
		canvas = GameObject.Find("Canvas").transform;

		// Black image to create a fade effect for objects behind menu
		fade = new Stack<GameObject>();
		black.GetComponent<Image>().rectTransform.localScale = 
			new Vector2(Screen.width,Screen.height);

		// Menu stack to hold reference to multiple menus (only top is active)
		menuStack = new Stack<Menu>();
		DontDestroyOnLoad(this.gameObject);
	}

	void Update(){}

	public void OpenMenu(Menu menu){

		GameObject img = GameObject.Instantiate(black, canvas);
		img.SetActive(true);
		menuStack.Push(menu);
		fade.Push(img);
	}

	// Closes current active menu
	public void CloseMenu(){
		menuStack.Pop().CloseMenu();
	}

	// Clears menu stack
	public void CloseAllMenus(){
		Menu menu;
		while((menu = menuStack.Pop()) != null)
			menu.CloseMenu();
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
