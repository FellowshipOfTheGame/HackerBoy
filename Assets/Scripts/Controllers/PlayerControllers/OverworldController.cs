using UnityEngine;
using System.Collections;

public class OverworldController : PlayerController {

	public float speed = 0.15f;
	
	private Player player;

	public OverworldController(Player p){
		this.player = p;
	}

	public override void Horizontal(){
		Vector3 moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), 0).normalized;
        player.gameObject.transform.Translate(moveDir * speed);
	}

	public override void Vertical(){
		Vector3 moveDir = new Vector2(0, Input.GetAxisRaw("Vertical")).normalized;
        player.gameObject.transform.Translate(moveDir * speed);
	}

	public override void Idle(){

	}
	
	public override void Action(){
		if(player.interactable != null)
			player.interactable.OnInteract();
	}
	
	public override void Cancel(){

	}
	
	public override void Start(){

	}
}


