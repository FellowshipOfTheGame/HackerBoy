using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class OptionsMenu : Menu {

	private int rows, cols;
	private List<GameObject> options;
	
	protected RectTransform cursorTransform; // First row/col X = -200 Y = 40
	protected int cursorPos; 	// Ascending in row order
								// 0 3 6
								// 1 4 7
								// 2 5 8

	public OptionsMenu(
		int rows,
		int cols,
		GameObject cursor, 
		GameObject[] options,
		string name,
		float width = 540, 
		float height = 135)
	: base(cursor, height, width, name){
		
		this.cols = cols;
		this.rows = rows;
		this.options = new List<GameObject>(options);
		for(int i = 0; i < options.Length; i++)
			options[i].transform.SetParent(GameObject.Find("Canvas").transform);
	}

	public override void CloseMenu(){
		// Destroy cursor
		UnityEngine.Object.Destroy(cursor);
		// Destroy options
		foreach (GameObject go in options)
			UnityEngine.Object.Destroy(go);
		options = null; // Destroy list
	}

	// If direction > 0 move right, else move left
	public override void MoveCursorHorizontal(float direction){
		direction /= Math.Abs(direction); // Get normalized value 1 or -1
		Debug.Log("Moving cursor on horizontal axis. Direction: " + direction);
		
		// Move right
		if(direction > 0 /*&& cursorPos + 3 <= opCascade.size*/){
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
	public override void MoveCursorVertical(float direction){
		direction /= Math.Abs(direction); // Get normalized value 1 or -1
		Debug.Log("Moving cursor on vertical axis. Direction: " + direction);

		// Move up
		if(direction > 0){
			cursorPos--;
			float tmp = cursorTransform.localPosition.y + Y_STEP;
			float x = cursorTransform.localPosition.x;
			cursorTransform.localPosition = new Vector2(x, tmp);

		// Move down
		} else if(direction < 0){
			cursorPos++;
			float tmp = cursorTransform.localPosition.y - Y_STEP;
			float x = cursorTransform.localPosition.x;
			cursorTransform.localPosition = new Vector2(x, tmp);
		}
	}
}