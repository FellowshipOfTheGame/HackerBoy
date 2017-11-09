using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private bool inSafeZone;
	private Inventory inventory;
	private PlayerController pc;
	private PartyMember[] party = new PartyMember[4];

	void Start(){

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
		
		if(Input.GetAxis("Horizontal") > 0)
			this.pc.Horizontal();
		
		if(Input.GetAxis("Vertical") > 0 )
			this.pc.Vertical();

	}

	public void SetController(PlayerController pc){ this.pc = pc; }

}