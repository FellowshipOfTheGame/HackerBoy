using UnityEngine;
using System.Collections;

// FIXME: How to model battle??
public /*static*/ class BattleManager : MonoBehaviour {

	/*DEBUG*/
	public bool testBattle;
	public Player p;
	public UnityEngine.UI.Text battlePhase;
	/*ENDDEBUG*/

	public enum BattleState {
		START,
		PLAYER_CHOICE,
		ENEMY_CHOICE,
		WIN,
		LOSE,
		NULL
	}

	private CharacterBase[] player;
	private EnemyBase[] enemies;
	public BattleState currentState 	{ get; private set; }
	public BattleBoxManager battleBox 	{ get; private set; }

	void Awake(){
		DontDestroyOnLoad(transform.gameObject);
	}

	void Start(){
		this.battleBox = gameObject.GetComponent<BattleBoxManager>();
		currentState = BattleState.START;

		if(testBattle) {
			Debug.Log("Battle test setup");
			currentState = BattleState.START;
		}
	}

	void Update(){

		// Battle State Machine
		switch(currentState){
		case BattleState.START:
			this.battlePhase.text = "START";
			StartBattle(p, player, null);
			break;

		case BattleState.PLAYER_CHOICE:
			this.battlePhase.text = "PLAYER_CHOICE";
			break;

		case BattleState.ENEMY_CHOICE:
			this.battlePhase.text = "ENEMY_CHOICE";
			break;

		case BattleState.WIN:
			this.battlePhase.text = "WIN";
			break;

		case BattleState.LOSE:
			this.battlePhase.text = "LOSE";
			break;

		case BattleState.NULL:
			this.battlePhase.text = "NULL";
			break;

		default:
			throw new BattleException();
		}
	}

	// Make a function call (static class) or create a new Battle object?
	// How to know which enemies to spawn in each area?
	public void StartBattle(Player player, CharacterBase[] playerParty, EnemyBase[] enemies){
		
		Debug.Log("Starting battle");
		this.player = playerParty;
		this.enemies = enemies;
		
		Debug.Log("Assigning player new controller");
		player.pc = new BattleController(this);

		Debug.Log("Phase: Player's choice");
		currentState = BattleState.PLAYER_CHOICE;
	}

	public void Run(){}
}