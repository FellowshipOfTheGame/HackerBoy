using System;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public enum Direction { N, S, E, W }

	// TODO: make this generic (based on sprite size)
	public readonly float INTERACT_COLLIDER_OFFSET_X = 0.75f;
	public readonly float INTERACT_COLLIDER_OFFSET_Y = 1f;

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
	public Inventory inventory;

	[UnityEngine.SerializeField] 
	private CharacterBase[] party = new CharacterBase[4]; // Also lineup
	[UnityEngine.SerializeField] 
	private BoxCollider2D interactCollider;
	private BattleManager bm;
	private GameManager gm;
	private MenuManager mm;

	void Start(){
		this.pc = new OverworldController(this);
		this.inventory = gameObject.AddComponent<Inventory>();
		this.interactable = null;
		this.inSafeZone = false;

		this.bm = GameObject.Find("BattleManager").GetComponent<BattleManager>();
		this.gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		this.mm = GameObject.Find("MenuManager").GetComponent<MenuManager>();

		/* DEBUG NOTE: For debugging, use hard code values X = +-0.75 Y = +-1*/
		this.interactCollider.offset = new Vector2(0, -1);
		DontDestroyOnLoad(this.gameObject);

		// StartDebugMenu();
	}

	// DEBUG
	private void StartDebugMenu(){
		pc = new MenuController(GameObject.Find("MenuManager")
									.GetComponent<MenuManager>());
	}

	void Update(){
		if(/*DEBUG*/gm == null ||/*ENDDEBUG*/ !gm.gamePaused)
			ProcessInput();
	}

	private void ProcessInput(){

		// Button down press
		if(Input.GetButtonDown("Action")) pc.Action();
		if(Input.GetButtonDown("AltAction")) pc.AltAction();
		if(Input.GetButtonDown("Cancel")) pc.Cancel();
		if(Input.GetButtonDown("Start")) pc.Start();
		
		// Button up
		if(Input.GetButtonUp("Action")) pc.ActionRelease();
		if(Input.GetButtonUp("AltAction")) pc.AltActionRelease();
		if(Input.GetButtonUp("Cancel")) pc.CancelRelease();
		if(Input.GetButtonUp("Start")) pc.StartRelease();

		// Axes
		float hAxis = Input.GetAxisRaw("Horizontal");
		float vAxis = Input.GetAxisRaw("Vertical");
		pc.Horizontal(hAxis);
		pc.Vertical(vAxis);
		if(hAxis == 0 && vAxis == 0) 
			pc.Idle();
	}

	public void SetController(PlayerController pc){ this.pc = pc; }
	public void SetInteractable(Interactable obj){ interactable = obj; }

	public void SetInteractColliderPos(Vector2 pos){ 
		interactCollider.offset = pos; 
	}
	public void SetInteractColliderPos(Direction d){
		switch(d){
		case Direction.N:
			SetInteractColliderPos(new Vector2(0, INTERACT_COLLIDER_OFFSET_Y));
			break;
		case Direction.S:
			SetInteractColliderPos(new Vector2(0, -INTERACT_COLLIDER_OFFSET_Y));
			break;
		case Direction.E:
			SetInteractColliderPos(new Vector2(INTERACT_COLLIDER_OFFSET_X, 0));
			break;
		case Direction.W:
			SetInteractColliderPos(new Vector2(-INTERACT_COLLIDER_OFFSET_X, 0));
			break;
		default:
			Debug.Log("Invalid direction.");
			// throw new Exception("Invalid direction.");
			break;
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		Debug.Log("[MSG]: " + this + " collided with " + other.gameObject);
		if(other.gameObject.tag.Equals("OverworldEnemy")){
			OverworldEnemy enemy = other.gameObject
					.GetComponent<OverworldEnemy>();

			// Should transition scene here or in battle script?
			// TODO: Create a scene manager to transition scenes
			// sceneManager.transition(SceneManager.BATTLE_SCENE)
			Debug.Log("[MSG]: Touched an enemy! Starting battle...");
			bm.StartBattle(this, party, enemy.enemyParty);
		}
	}
}