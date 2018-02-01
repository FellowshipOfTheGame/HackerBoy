using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class BattleBoxManager : MonoBehaviour {

	public static readonly float UP = 1.0f;
	public static readonly float RIGHT = 1.0f;
	public static readonly float LEFT = -1.0f;
	public static readonly float DOWN = -1.0f;

	private readonly int X_STEP = 250;
	private readonly int Y_STEP = 40;

	private Image portrait;
	private RectTransform cursorTransform; // First row/col X = -200 Y = 40
	private int cursorPos; 	// Ascending in row order
							// 0 3 6
							// 1 4 7
							// 2 5 8
	private List<Text> options;

	/*
	TODO: make this dynamic
	Row and col increments
	X = 0	Y = 0
	X = 250	Y = -40
	X = 500	Y = -80
	*/

	void Start(){
		GameObject tmp = GameObject.Find("/Canvas/BattleTextBox/Cursor");
		if(!tmp) throw new Exception("Cursor object not found.");
		// this.cursor = tmp.GetComponent<Image>();
		this.cursorTransform = tmp.GetComponent<RectTransform>();
	}

	// If direction > 0 move right, else move left
	public void MoveCursorHorizontal(float direction){
		direction /= Math.Abs(direction); // Get normalized value 1 or -1
		Debug.Log("Moving cursor on horizontal axis. Direction: " + direction);
		
		// Move right
		if(direction > 0){
			cursorPos += 3;
			float tmp = cursorTransform.localPosition.x + X_STEP;
			float y = cursorTransform.localPosition.y;
			cursorTransform.localPosition = new Vector2(tmp, y);

		// Move left
		} else if(direction < 0){
			cursorPos -= 3;
			float tmp = cursorTransform.localPosition.x - X_STEP;
			float y = cursorTransform.localPosition.y;
			cursorTransform.localPosition = new Vector2(tmp, y);
		}
	}

	// If direction > 0 move up, else move down
	public void MoveCursorVertical(float direction){
		direction /= Math.Abs(direction); // Get normalized value 1 or -1
		Debug.Log("Moving cursor on vertical axis. Direction: " + direction);

		// Move up
		if(direction > 0){
			cursorPos++;
			float tmp = cursorTransform.localPosition.y + Y_STEP;
			float x = cursorTransform.localPosition.x;
			cursorTransform.localPosition = new Vector2(x, tmp);

		// Move down
		} else if(direction < 0){
			cursorPos--;
			float tmp = cursorTransform.localPosition.y - Y_STEP;
			float x = cursorTransform.localPosition.x;
			cursorTransform.localPosition = new Vector2(x, tmp);
		}
	}
}
