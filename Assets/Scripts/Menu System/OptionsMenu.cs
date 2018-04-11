using UnityEngine;
using UnityEngine.UI;
using Constraint = UnityEngine.UI.GridLayoutGroup.Constraint;
using Axis = UnityEngine.UI.GridLayoutGroup.Axis;
using System;
using System.Collections;
using System.Collections.Generic;

public class OptionsMenu : Menu {

	private int rows, cols;
	private int step;
	private float cursorAutoMoveStart;
	private float cursorAutoMoveDelay;
	private bool vAxisPressed, hAxisPressed;
	private RectTransform optionTransform; // Current indexed option transform
	private List<MenuEntry> options;
	private GridLayoutGroup grid;

	// Number of rows and cols are only relevant if row/col constraint is used
	public OptionsMenu(
		int rows,
		int cols,
		GameObject cursor, 
		MenuEntry[] options,
		Sprite menuImage,
		Font font,
		Constraint constraint = Constraint.FixedRowCount,
		string name = "Option Menu",
		float width = 540, 
		float height = 135,
		float posx = 0,
		float posy = 0)
	: base(cursor, height, width, posx, posy, name, menuImage, font){
		
		// Menu dimension
		this.cols = cols;
		this.rows = rows;
		
		// Create a grid layout
		grid = this.menu.AddComponent<GridLayoutGroup>();
		
		// Position grid inside background image
		// NOTE: compiler is complaining about double conversion, suck it
		grid.padding.left = (int) 40 + (int) cursorTransform.rect.width;
		grid.padding.right = 0;
		grid.padding.top = 24;
		grid.padding.bottom = 12;
		grid.cellSize = new Vector2(100, 33);
		grid.spacing = new Vector2(75, 0);

		grid.startAxis = Axis.Vertical;
		grid.constraint = constraint;

		if(constraint == Constraint.FixedRowCount){
			grid.constraintCount = rows;
			step = rows;
		} else if(constraint == Constraint.FixedColumnCount){
			throw new Exception("Fixed column not supported");
			// grid.constraintCount = cols;
			// NOTE: How to deal with horizontal step here?
			// Maybe we should just fix row count forever.
		}
		
		// Create options
		this.options = new List<MenuEntry>(options);
		float x, y;
		x = 0f;
		y = 0f;

		int borderSize = 32;
		for(int i = 0; i < options.Length; i++){
			options[i].transform.SetParent(this.menu.transform);
			options[i].transform.localScale = new Vector3(1f, 1f, 1f);
		}

		// Force layout rebuild to correctly position the cursor
 		LayoutRebuilder.ForceRebuildLayoutImmediate(this.rectTransform); 

		// Resize menu box to fit contents
		Vector2 dim = CalculateGridDimensions(grid);
		x = this.rectTransform.rect.x;
		y = this.rectTransform.rect.y;
		this.rectTransform.sizeDelta = dim;

		// Reposition menu using its size
		this.menu.transform.localPosition = 
			new Vector3(0f, -Screen.height/2 + dim.y, 0f);

		// Move cursor to first option
		optionTransform = options[0].gameObject.GetComponent<RectTransform>();
		PositionCursor(0);
	}

	public override void CloseMenu(){
		Debug.Log("Closing menu");
		// Destroy cursor
		UnityEngine.Object.Destroy(cursor);
		
		// Destroy options
		foreach (MenuEntry mi in options){
			UnityEngine.Object.Destroy(mi.gameObject);

		}
		options = null; // Destroy list

		// Destroy parent object
		UnityEngine.Object.Destroy(this.menu);
	}

	public override void Idle(){
		vAxisPressed = false;
		hAxisPressed = false;
	}

	public override void Action(){
		options[cursorPos].Action();
	}

	// If direction > 0 move right, else move left
	public override void MoveCursorHorizontal(float direction){

		if(direction == 0f){
			hAxisPressed = false;
			if(!vAxisPressed)
				cursorAutoMoveStart = 0f;
			return;
		}

		// Player already moved once, start counting to auto move cursor
		if(hAxisPressed){
			// Horizontal axis is 0
			if(Math.Abs(direction) < 0.9f){

				cursorAutoMoveStart = 0f;
				cursorAutoMoveDelay = 0f;
				hAxisPressed = false;
				return;
			} 

			// Wait 0.5s to start moving cursor
			if(cursorAutoMoveStart <= 0.5f){
				cursorAutoMoveStart += Time.deltaTime;
				return;
			}
			
			// Now move cursor one position each 0.1s
			else if(cursorAutoMoveDelay <= 0.1f){
				cursorAutoMoveDelay += Time.deltaTime;
				return;
			}
		}

		hAxisPressed = true;
		cursorAutoMoveDelay = 0f;
		direction /= Math.Abs(direction); // Get normalized value 1 or -1

		// Move right
		if(direction > 0 && cursorPos+step < options.Count){
			cursorPos += step;
			PositionCursor(cursorPos);

		// Move left
		} else if(direction < 0 && cursorPos-step >= 0){
			cursorPos -= step;
			PositionCursor(cursorPos);

		}
	}

	// If direction > 0 move up, else move down
	public override void MoveCursorVertical(float direction){
		
		if(direction == 0f){
			vAxisPressed = false;
			if(!hAxisPressed)
				cursorAutoMoveStart = 0f;
			return;
		}

		// Player already moved once, start counting to auto move cursor
		if(vAxisPressed){
			// Horizontal axis is 0
			if(Math.Abs(direction) < 0.9f){

				cursorAutoMoveStart = 0f;
				cursorAutoMoveDelay = 0f;
				vAxisPressed = false;
				return;
			} 

			// Wait 0.5s to start moving cursor
			if(cursorAutoMoveStart <= 0.5f){
				cursorAutoMoveStart += Time.deltaTime;
				return;
			}
			
			// Now move cursor one position each 0.1s
			else if(cursorAutoMoveDelay <= 0.1f){
				cursorAutoMoveDelay += Time.deltaTime;
				return;
			}
		}

		vAxisPressed = true;
		cursorAutoMoveDelay = 0f;
		direction /= Math.Abs(direction); // Get normalized value 1 or -1

		// Move up
		if(direction > 0 && cursorPos-1 >= 0)
			PositionCursor(--cursorPos);
		// Move down
		else if(direction < 0 && cursorPos+1 < options.Count)
			PositionCursor(++cursorPos);
	}

	private void PositionCursor(int index){
		Debug.Log("Moving cursor to index " + index);
		var op = options[index];

		// Move cursor to center of option and subtract half width + extra
		float y = op.transform.position.y;
		float x = op.transform.position.x - 
					optionTransform.rect.width/2 - 
					cursorTransform.rect.width/2;

		cursorTransform.position = new Vector2(x, y);
	}

	private Vector2 CalculateGridDimensions(GridLayoutGroup grid){
		
		float width, height;
		float elementsPerRow = (float) Math.Ceiling((double) options.Count/rows);
		float elementsPerCol = (float) Math.Ceiling((double) options.Count/elementsPerRow);
		// (100 + 75 + 40) * 5
		width = (grid.cellSize.x + grid.spacing.x)*elementsPerRow;
		width += grid.padding.left + grid.padding.right;

		height = (grid.cellSize.y + grid.spacing.y)*elementsPerCol;
		height += grid.padding.top + grid.padding.bottom;

		return new Vector2(width, height);
	}
}