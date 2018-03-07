using System;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public enum Direction { N, S, E, W }

	// TODO: make this generic (based on sprite size)
	public readonly float INTERACTABLE_COLLIDER_OFFSET_X = 0.75f;
	public readonly float INTERACTABLE_COLLIDER_OFFSET_Y = 1f;

	private Direction _spriteDir;
	public Direction spriteDir {
		get { return this._spriteDir; }
		set {
			this._spriteDir = value;
			this.SetInteractColliderPos(value);
		}
	}
	
	public bool inSafeZone;
	public PlayerController pc;
	public Interactable interactable;

	[UnityEngine.SerializeField]
	private Inventory inventory;
	[UnityEngine.SerializeField] 
	private CharacterBase[] party = new CharacterBase[4]; // Also lineup
	[UnityEngine.SerializeField] 
	private BoxCollider2D interactCollider;

	void Start(){
		this.pc = new OverworldController(this);
		this.interactable = null;
		this.inSafeZone = false;

		/* DEBUG NOTE: For debugging, use hard code values X = +-0.75 Y = +-1*/
		this.interactCollider.offset = new Vector2(0, -1);
	}

	void Update(){
		ProcessInput();
	}

	private void ProcessInput(){

		// Button down press
		if(Input.GetButtonDown("Action")) this.pc.Action();
		if(Input.GetButtonDown("AltAction")) this.pc.AltAction();
		if(Input.GetButtonDown("Cancel")) this.pc.Cancel();
		if(Input.GetButtonDown("Start")) this.pc.Start();
		
		// Button up
		if(Input.GetButtonUp("Action")) this.pc.ActionRelease();
		if(Input.GetButtonUp("AltAction")) this.pc.AltActionRelease();
		if(Input.GetButtonUp("Cancel")) this.pc.CancelRelease();
		if(Input.GetButtonUp("Start")) this.pc.StartRelease();

		// Axes
		if(Input.GetAxis("Horizontal") != 0) this.pc.Horizontal();
		if(Input.GetAxis("Vertical") != 0) this.pc.Vertical();
		if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
			this.pc.Idle();
	}

	public void SetController(PlayerController pc){ this.pc = pc; }
	public void SetInteractable(Interactable obj){ this.interactable = obj; }

	public void SetInteractColliderPos(Vector2 pos){ 
		this.interactCollider.offset = pos; 
	}
	public void SetInteractColliderPos(Direction d){
		switch(d){
		case Direction.N:
			SetInteractColliderPos(new Vector2(0, INTERACTABLE_COLLIDER_OFFSET_Y));
			break;
		case Direction.S:
			SetInteractColliderPos(new Vector2(0, -INTERACTABLE_COLLIDER_OFFSET_Y));
			break;
		case Direction.E:
			SetInteractColliderPos(new Vector2(INTERACTABLE_COLLIDER_OFFSET_X, 0));
			break;
		case Direction.W:
			SetInteractColliderPos(new Vector2(-INTERACTABLE_COLLIDER_OFFSET_X, 0));
			break;
		default:
			Debug.Log("Invalid direction.");
			// throw new Exception("Invalid direction.");
			break;
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		Debug.Log("[MSG]: " + this + " collided with " + other.gameObject);
	}
}