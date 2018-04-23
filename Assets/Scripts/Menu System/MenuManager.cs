using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour {
	
	public static Transform canvas;

	public Font font;
	public Sprite menuImage;
	public GameObject black;
	public GameObject cursorPrefab;
	public Menu activeMenu { get; private set; }
	
	private Stack<Menu> menuStack;
	private Stack<GameObject> fade;

	void Start(){

		// Get a static reference for easy access
		canvas = GameObject.Find("Canvas").transform;
		DontDestroyOnLoad(canvas.gameObject);

		// Black image to create a fade effect for objects behind menu
		fade = new Stack<GameObject>();
		black.GetComponent<Image>().rectTransform.localScale = 
			new Vector2(Screen.width, Screen.height);

		// Menu stack to hold reference to multiple menus (only top is active)
		menuStack = new Stack<Menu>();
		DontDestroyOnLoad(this.gameObject);
	}

	void Update(){}

	public void OpenMenu(Menu menu){ menuStack.Push(menu); }
	public Menu GetCurrentMenu(){ 
		try {
			return menuStack.Peek();
		} catch(System.Exception){
			return null;
		}
	}

	// Closes current active menu
	public void CloseMenu(){ 
		RemoveMenuFadeLayer();
		if(menuStack.Count > 0) 
			menuStack.Pop().CloseMenu(); 
	}

	// Clears menu stack
	public void CloseAllMenus(){
		while(menuStack.Count > 0)
			menuStack.Pop().CloseMenu();
	}

	public void AddMenuFadeLayer(){

		// Instantiate a black image with small alpha
		GameObject img = GameObject.Instantiate(black, canvas);
		img.SetActive(true);
		
		// Apply fade effect
		Image i = img.GetComponent<Image>();
		i.enabled = true;
		i.color = new Color(1f, 1f, 1f, 0.25f);
		fade.Push(img);
	}

	public void RemoveMenuFadeLayer(){ 
		if(fade.Count > 0) Destroy(fade.Pop());
	}

	public GameObject CreateCursor(){
		return GameObject.Instantiate(cursorPrefab, canvas);
	}

	/**********************************************************/
	/**********************************************************/
	public void DebugMenu(){
		
		// Add fade layer first so it doesnt affect the cursor and menu objects
		AddMenuFadeLayer();

		// Create cursor
		GameObject cursor = CreateCursor();

		// Create options for the menu
		int n = 12;
		GameObject[] objects = new GameObject[n];
		SubMenuEntry[] options = new SubMenuEntry[n];
		for(int i = 0; i < n; i++){
			// Create new entry for menu
			objects[i] = new GameObject("option" + (i+1));
			options[i] = objects[i].AddComponent<SubMenuEntry>();

			// Add entry text to show
			var t = objects[i].AddComponent<Text>();
			t.text = "option" + (i+1);
			t.font = font;
			t.fontSize = 20;
		}

		this.OpenMenu(new OptionsMenu(
			// width: ??, height: ??,
			constraint: UnityEngine.UI.GridLayoutGroup.Constraint.FixedRowCount,
			font: font,
			cursor: cursor,
			options: options,
			menuImage: menuImage,
			name: "debug_menu",
			rows: 3, cols: 3)
		);
	}
}
