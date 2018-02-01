using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public bool inSafeZone;
	public Interactable interactable;

	public PlayerController pc;
	[UnityEngine.SerializeField] 
	private Inventory inventory;
	[UnityEngine.SerializeField] 
	private CharacterBase[] party = new CharacterBase[4]; // Also lineup

	void Start(){
		pc = new OverworldController(this);
		interactable = null;
		inSafeZone = false;
	}

	void Update(){

		ProcessInput();
	}

	private void ProcessInput(){

		if(Input.GetButton("Action"))
			this.pc.Action();
		
		if(Input.GetButton("Cancel"))
			this.pc.Cancel();
		
		if(Input.GetButton("Start"))
			this.pc.Start();
		
		if(Input.GetAxis("Horizontal") != 0)
			this.pc.Horizontal();
		
		if(Input.GetAxis("Vertical") != 0)
			this.pc.Vertical();

		if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
			this.pc.Idle();

	}

	public void SetInteractable(Interactable obj){ this.interactable = obj; }
	public void SetController(PlayerController pc){ this.pc = pc; }
}