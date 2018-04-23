using UnityEngine;
using UnityEngine.UI;

public abstract class Menu {

	/* Constants */
	public static readonly float UP = 1.0f;
	public static readonly float RIGHT = 1.0f;
	public static readonly float LEFT = -1.0f;
	public static readonly float DOWN = -1.0f;

	protected readonly float X_STEP = 250f;
	protected readonly float Y_STEP = 40f;
	
	/* Attributes */
	public bool isActive { get; private set; }
	public Sprite menuImage;
	protected Font font;

	/* Menu object att */
	protected GameObject menu;
	protected RectTransform rectTransform;
	public RectTransform rect { get { return this.rectTransform; } }

	/* Cursor object att */
	protected GameObject cursor;
	protected RectTransform cursorTransform; // First row/col X = -200 Y = 40
	protected int cursorPos; 	// Ascending in row order
								// 0 3 6
								// 1 4 7
								// 2 5 8

	/*
	TODO: make this dynamic
	Row and col increments
	X = 0	Y = 0
	X = 250	Y = -40
	X = 500	Y = -80
	*/

	public Menu(GameObject cursor, 
				float height, 
				float width, 
				float posx,
				float posy,
				string name, 
				Sprite menuImage,
				Font font){
		
		// Create a gameobject to hold all menu parts
		this.isActive = true;
		this.menu = new GameObject(name);
		this.menu.transform.SetParent(MenuManager.canvas);

		// Create menu background
		Image img = this.menu.AddComponent<Image>();
		img.sprite = menuImage;
		this.font = font;

		// NOTE: important to do this AFTER adding an image component!
		// Position and scale menu
		this.menu.transform.localScale = new Vector3(1f, 1f, 1f);
		// this.menu.transform.position = new Vector3 (Screen.width/2, 1f, 1f);
		this.rectTransform = this.menu.GetComponent<RectTransform>();
		this.rectTransform.sizeDelta = new Vector2(width, height);
		
		// Move menu behind cursor in hierarchy
		menu.transform.SetSiblingIndex(menu.transform.GetSiblingIndex()-1);

		// Create menu cursor
		this.cursor = cursor;
		this.cursorTransform = cursor.GetComponent<RectTransform>();
		this.cursorPos = 0;
	}

	public Vector2 GetDimensions(){ return this.rectTransform.sizeDelta; }
	public void SetActive(bool active){
		isActive = active;
		menu.SetActive(active);
		cursor.SetActive(active);
		Debug.Log("[Debug]: Setting " + this + " to active = " + active);
	}

	public abstract void CloseMenu();
	public abstract void Idle();
	public abstract void Action(MenuController mc);
	public abstract void MoveCursorHorizontal(float direction);
	public abstract void MoveCursorVertical(float direction);
}

