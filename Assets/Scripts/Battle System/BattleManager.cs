using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour {

	public delegate void BattleAction(CharacterBase target);

	public enum BattleState {
		START,
		PLAYER_CHOICE,
		ENEMY_CHOICE,
		RESOLVE_ACTIONS,
		RESOLVE_EFFECTS,
		WIN,
		LOSE,
		NULL
	}

	public int currentCharacter; // Used to index player party for actions
	public BattleState currentState { get; private set; }
	public EnemyBase[] enemies { get; private set; }
	public CharacterBase[] playerParty { get; private set; }
	public GameObject currentPortrait;

	private OverworldEnemy enemyWhoStartedBattle;
	private int enemyToProcess;
	private Player p;
	private List<Pair<BattleAction, CharacterBase>> turnActions;
	private MenuManager mm;

	void Awake(){
		DontDestroyOnLoad(transform.gameObject);
	}

	void Start(){
		mm = GameObject.Find("MenuManager").GetComponent<MenuManager>();
		currentState = BattleState.NULL;
		turnActions = new List<Pair<BattleAction, CharacterBase>>();
	}

	void Update(){

		// Battle State Machine
		/*
		NOTE: is this state actually necessary? there is already a 
		StartBattle function which is called before any of these states
		
		NOTE 2: does the state machine necessarily need to stay in update?
		it can be implicetly done via function calls
		*/
		switch(currentState){
		case BattleState.START:
			
			// Create battle menu
			BattleMenu();

			// Create and position portrait left of battle menu
			currentPortrait = new GameObject("Portrait");
			currentPortrait.transform.SetParent(MenuManager.canvas);
			currentPortrait.transform.localScale = new Vector3(1f, 1f, 1f);
			var img = currentPortrait.AddComponent<Image>();

			img.sprite = playerParty[currentCharacter].portrait;
			RectTransform trans = mm.GetCurrentMenu().rect;

			float x = trans.anchoredPosition.x - trans.sizeDelta.x/2 - 
					img.GetComponent<RectTransform>().sizeDelta.x/2;
			float y = trans.anchoredPosition.y;
			currentPortrait.transform.localPosition = new Vector3(x, y, 0f);
			
			currentState = BattleState.PLAYER_CHOICE;
			// mm.GetCurrentMenu().SetActive(true);
			currentPortrait.GetComponent<Image>().enabled = true;
			currentCharacter = FindNextCharacter(0);
			break;

		case BattleState.PLAYER_CHOICE:
			
			if(mm.GetCurrentMenu() == null){
				currentCharacter = FindNextCharacter(0);
				BattleMenu();
				img = currentPortrait.GetComponent<Image>();
				img.enabled = true;
				img.sprite = playerParty[currentCharacter].portrait;
			}

			enemyToProcess = 0;
			break;

		case BattleState.ENEMY_CHOICE:
			if(enemyToProcess < enemies.Length){

				PushAction(enemies[enemyToProcess].ChooseAction());
				enemyToProcess++;
			
			} else currentState = BattleState.RESOLVE_ACTIONS;

			break;
		
		case BattleState.RESOLVE_ACTIONS:
			// TODO sort action list by character agility for correct turn order
			if(turnActions.Count > 0){

				var action = DequeueAction();
				
				// If target has already died for some reason, find next 
				// plausible target TODO
				// NOTE: for now, just do nothing
				if(action.second.status != StatusEffect.Dead){
					// TODO: pass this to a coroutine so we can make battle animations
					action.first(action.second);
				}

			
			} else currentState = BattleState.RESOLVE_EFFECTS;

			break;

		case BattleState.RESOLVE_EFFECTS:

			// Apply effects like damage over time
			currentState = BattleState.PLAYER_CHOICE;
			break;

		case BattleState.WIN:
			EndBattle();
			currentState = BattleState.NULL;
			break;

		case BattleState.LOSE:
			// GameOver();
			currentState = BattleState.NULL;
			break;

		case BattleState.NULL:
			break;

		default:
			throw new BattleException();
		}
	}

	// Make a function call (static class) or create a new Battle object?
	// How to know which enemies to spawn in each area?
	public void StartBattle(Player player, CharacterBase[] playerParty, 
		OverworldEnemy oe){
		
		// NOTE: probably temporary
		foreach (EnemyBase enmy in oe.enemyParty)
			enmy.gameObject.SetActive(true);
		foreach (CharacterBase chr in playerParty)
			chr.gameObject.SetActive(true);

		// TODO make this AsyncLoad [1]
		SceneManager.LoadScene("Assets/Scenes/Battle.unity");

		this.enemyWhoStartedBattle = oe;
		this.playerParty = playerParty;
		this.enemies = oe.enemyParty;
		this.p = player;

		this.p.pc = new MenuController(mm);
		// NOTE: cant deactivate player cause the controller script needs to run
		this.p.GetComponent<SpriteRenderer>().enabled = false;

		currentState = BattleState.START;

		// TODO: make a smooth scene transition
		// StartCoroutine(LoadBattleScene()); // FIXME: do we need async load? see [1]
	}

	public void EndBattle(){
		Debug.Log("[MSG]: Ending battle.");

		// NOTE: probably temporary
		foreach (CharacterBase chr in playerParty)
			chr.gameObject.SetActive(false);

		enemyWhoStartedBattle.gameObject.SetActive(false);
		GameObject.Destroy(enemyWhoStartedBattle);
		enemyWhoStartedBattle = null;
		
		p.pc = new OverworldController(p);

		this.p.GetComponent<SpriteRenderer>().enabled = true;
		GameObject.Destroy(this.currentPortrait);
		this.currentPortrait = null;
		
		SceneManager.LoadScene("Assets/Scenes/Prototype.unity");
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

	public void FinishAction(){
		
		var img = currentPortrait.GetComponent<Image>();
		
		currentCharacter = FindNextCharacter(currentCharacter+1);

		// If player has made an action for every character, start enemy turn
		if(currentCharacter >= playerParty.Length){
		
			// mm.GetCurrentMenu().SetActive(false);
			mm.CloseMenu();
			Debug.Log("[MSG]: All characters have made an action, starting enemy turn.");
			currentState = BattleState.ENEMY_CHOICE;
			img.enabled = false;
		
		} else img.sprite = playerParty[currentCharacter].portrait;
	}

	public void CancelAction(){
		currentCharacter = FindPrevCharacter(currentCharacter);
		PopAction();
	}

	public void PushAction(BattleAction act, CharacterBase param){
		turnActions.Add(new Pair<BattleAction, CharacterBase>(act, param));
	}

	public void PushAction(Pair<BattleAction, CharacterBase> pair){
		turnActions.Add(pair);
	}

	public Pair<BattleAction, CharacterBase> PopAction(){
		
		try {
		
			var ret = turnActions[turnActions.Count-1];
			turnActions.RemoveAt(turnActions.Count-1);
			return ret;
		
		} catch(System.Exception){
			return null;
		}
	}

	public Pair<BattleAction, CharacterBase> DequeueAction(){
		
		try {
		
			var ret = turnActions[0];
			turnActions.RemoveAt(0);
			return ret;
		
		} catch(System.Exception){
			return null;
		}
	}

	public void BattleMenu(){

		/* Options list
			Attack
			Skills
			Items
			Defend
			Run
			Pass
		*/

		// Create options for the menu
		string[] names = { "Attack", "Skills", "Items", "Defend", "Pass", "Run" };
		GameObject[] objects = new GameObject[names.Length];
		MenuEntry[] options = new MenuEntry[names.Length];

		for(int i = 0; i < names.Length; i++){
			// Create new entry for menu
			objects[i] = new GameObject(names[i]);

			// Add entry text to show
			var t = objects[i].AddComponent<Text>();
			t.text = names[i];
			t.font = mm.font;
			t.fontSize = 20;
		}
		
		options[0] = objects[0].AddComponent<AttackMenuEntry>();
		options[1] = objects[1].AddComponent<SubMenuEntry>();
		options[2] = objects[2].AddComponent<SubMenuEntry>();
		options[3] = objects[3].AddComponent<SubMenuEntry>();
		options[4] = objects[4].AddComponent<SubMenuEntry>();
		options[5] = objects[5].AddComponent<SubMenuEntry>();

		Menu m = new OptionsMenu(
			constraint: GridLayoutGroup.Constraint.FixedRowCount,
			font: mm.font,
			cursor: mm.CreateCursor(),
			options: options,
			menuImage: mm.menuImage,
			name: "BattleMenu",
			rows: 2, cols: 2);

		mm.OpenMenu(m);
		// m.SetActive(false);
	}

	public void OnCharacterDeath(CharacterBase chr){
		foreach (CharacterBase cb in playerParty) {
			if(cb.status != StatusEffect.Dead)
				return;
		}

		// GameOver();
		Debug.Log("[MSG]: Game Over!");
		currentState = BattleState.LOSE;
	}
	
	public void OnEnemyDeath(EnemyBase enemy){

		// Create new array excluding null values
		EnemyBase[] tmp = new EnemyBase[enemies.Length-1];
		int j = 0;
		for (int i = 0; i < enemies.Length; i++){
			if(enemies[i].status == StatusEffect.Dead)
				enemies[i] = null;
			else tmp[j++] = enemies[i];
		}

		enemies = tmp;
		// GameObject.Destroy(enemy);

		if(enemies.Length == 0) currentState = BattleState.WIN;
	}

	/* NOTE: please dont ask about these functions. */
	// Return the next non dead character starting at index
	private int FindNextCharacter(int index){
		try{
			if(playerParty[index].status != StatusEffect.Dead)
			return index;

			// Skip dead characters
			while(index < playerParty.Length &&
				playerParty[index++].status == StatusEffect.Dead);

			return index-1;
		} catch(System.Exception){
			Debug.Log("[Error]: Invalid index in searching for next character: " + index);
			return index+1;
		}
	}

	// Return the prev non dead character starting at index
	private int FindPrevCharacter(int index){
		try{
			if(playerParty[index].status != StatusEffect.Dead)
			return index;

			// Skip dead characters
			while(index > 0 && playerParty[index--].status == StatusEffect.Dead);

			return index+1;
		} catch(System.Exception){
			Debug.Log("[Error]: Invalid index in searching for prev character: " + index);
			return index-1;
		}
	}
}