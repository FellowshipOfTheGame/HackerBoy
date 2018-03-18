using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// FIXME: How to model battle??
public /*static*/ class BattleManager : MonoBehaviour {

	/*DEBUG*/
	public bool testBattle;
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

	public BattleState currentState 	{ get; private set; }
	public BattleBoxManager battleBox 	{ get; private set; }
	
	private Player p;
	private Vector2 playerOldPos;
	private CharacterBase[] playerParty;
	private EnemyBase[] enemies;

	void Awake(){
		DontDestroyOnLoad(transform.gameObject);
	}

	void Start(){
		this.battleBox = gameObject.GetComponent<BattleBoxManager>();
		currentState = BattleState.NULL;

		if(testBattle) {
			Debug.Log("Battle test setup");
			currentState = BattleState.START;
		}
	}

	void Update(){

		// Battle State Machine
		switch(currentState){
		case BattleState.START:
			if(this.battlePhase) 
				this.battlePhase.text = "START";
			break;

		case BattleState.PLAYER_CHOICE:
			if(this.battlePhase) 
				this.battlePhase.text = "PLAYER_CHOICE";
			break;

		case BattleState.ENEMY_CHOICE:
			if(this.battlePhase) 
				this.battlePhase.text = "ENEMY_CHOICE";
			break;

		case BattleState.WIN:
			if(this.battlePhase) 
				this.battlePhase.text = "WIN";
			break;

		case BattleState.LOSE:
			if(this.battlePhase) 
				this.battlePhase.text = "LOSE";
			break;

		case BattleState.NULL:
			if(this.battlePhase)
				this.battlePhase.text = "NULL";
			break;

		default:
			throw new BattleException();
		}
	}

	// Make a function call (static class) or create a new Battle object?
	// How to know which enemies to spawn in each area?
	public void StartBattle(Player player, CharacterBase[] playerParty, EnemyBase[] enemies){
		
		SceneManager.LoadScene("Assets/Scenes/Battle.unity");

		Debug.Log("Starting battle");
		this.playerParty = playerParty;
		this.enemies = enemies;
		this.p = player;

		Debug.Log("Assigning player new controller");
		this.p.pc = new BattleController(this);
		// NOTE: cant deactivate player cause the controller script needs to run
		playerOldPos = p.transform.position;
		this.p.transform.position = new Vector2(10000f, 10000f);

		Debug.Log("Phase: Start");
		currentState = BattleState.START;
		currentState = BattleState.PLAYER_CHOICE; /*DEBUG*/

		// TODO: make a smooth scene transition
		// StartCoroutine(LoadBattleScene()); // FIXME: do we need async load?
	}

	public void EndBattle(){
		p.transform.position = playerOldPos;
		p.pc = new OverworldController(p);
	}

	/* From unity documentation https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadSceneAsync.html */
	IEnumerator LoadBattleScene(){
        // The Application loads the Scene in the background at the same time as
        // the current Scene. This is particularly good for creating loading 
        // screens. You could also load the Scene by build number.
        AsyncOperation asyncLoad = SceneManager
        		.LoadSceneAsync("Assets/Scenes/Battle.unity");

        //Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        	yield return null;
    }

	public void Run(){}
}