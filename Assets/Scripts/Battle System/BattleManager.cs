using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour {

	public delegate void BattleAction(CharacterBase target);

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

	public BattleState currentState { get; private set; }
	public EnemyBase[] enemies { get; private set; }

	private Player p;
	private Vector2 playerOldPos;
	private CharacterBase[] playerParty;
	private List<Pair<BattleAction, CharacterBase>> turnActions;
	private MenuManager mm;

	void Awake(){
		DontDestroyOnLoad(transform.gameObject);
	}

	void Start(){
		mm = GameObject.Find("MenuManager").GetComponent<MenuManager>();
		currentState = BattleState.NULL;
		turnActions = new List<Pair<BattleAction, CharacterBase>>();

		if(testBattle) {
			Debug.Log("Battle test setup");
			currentState = BattleState.START;
		}
	}

	void Update(){

		// Battle State Machine
		switch(currentState){
		// NOTE: is this state actually necessary? there is already a 
		// StartBattle function which is called before any of these states
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
		
		// TODO make this AsyncLoad [1]
		SceneManager.LoadScene("Assets/Scenes/Battle.unity");

		this.playerParty = playerParty;
		this.enemies = enemies;
		this.p = player;

		this.p.pc = new MenuController(mm);
		// NOTE: cant deactivate player cause the controller script needs to run
		playerOldPos = p.transform.position;
		this.p.transform.position = new Vector2(10000f, 10000f);

		Debug.Log("Phase: Start");
		currentState = BattleState.START;
		currentState = BattleState.PLAYER_CHOICE; /*DEBUG*/

		// TODO: make a smooth scene transition
		// StartCoroutine(LoadBattleScene()); // FIXME: do we need async load? see [1]

		// Create battle menu
		BattleMenu();
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

	public void Run(CharacterBase cb){

	}

	public void PushAction(BattleAction act, CharacterBase param){
		turnActions.Add(new Pair<BattleAction, CharacterBase>(act, param));
	}

	public void BattleMenu(){

		/* Options list
			Attack
			Skills
			Items
			Run
			Defend ???
		*/

		// Create options for the menu
		string[] names = { "Attack", "Skills", "Items", "Run" };
		GameObject[] objects = new GameObject[names.Length];
		MenuEntry[] options = new MenuEntry[names.Length];

		for(int i = 0; i < names.Length; i++){
			// Create new entry for menu
			objects[i] = new GameObject(names[i]);
			options[i] = objects[i].AddComponent<SubMenuEntry>();

			// Add entry text to show
			var t = objects[i].AddComponent<UnityEngine.UI.Text>();
			t.text = names[i];
			t.font = mm.font;
			t.fontSize = 20;
		}

		mm.OpenMenu(new OptionsMenu(
			constraint: UnityEngine.UI.GridLayoutGroup.Constraint.FixedRowCount,
			font: mm.font,
			cursor: mm.CreateCursor(),
			options: options,
			menuImage: mm.menuImage,
			name: "BattleMenu",
			rows: 2, cols: 2)
		);
	}

	public static void OnCharacterDeath(){

	}
	
	public static void OnEnemyDeath(){

	}
}