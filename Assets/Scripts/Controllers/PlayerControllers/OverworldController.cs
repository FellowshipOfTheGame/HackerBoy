using UnityEngine;
using System.Collections;

public class OverworldController : PlayerController {

	public readonly float runSpeed = 700f;
	public readonly float walkSpeed = 250f;
	public readonly float maxSpeed = 100f;

	public float speed = 300f;
	
	private Player player;
	private Rigidbody2D rb;
	private GameObject pauseMenu;

	public OverworldController(Player p){
		this.player = p;
		this.rb = p.GetComponent<Rigidbody2D>();
		this.pauseMenu = GameObject.Find("PauseMenu");
		this.pauseMenu.SetActive(false);
	}

	public override void Horizontal(){
		Vector3 moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), 0).normalized;
		this.rb.AddForce(speed*moveDir);

        // TODO: let the animator handle this, probably
        // Set where the interaction collider will be (same direction the character is facing)
        if(moveDir.x > 0) player.spriteDir = Player.Direction.E;
        else if(moveDir.x < 0) player.spriteDir = Player.Direction.W;

	}

	public override void Vertical(){
		Vector3 moveDir = new Vector2(0, Input.GetAxisRaw("Vertical")).normalized;
		this.rb.AddForce(speed*moveDir);

        // TODO: let the animator handle this, probably
        // Set where the interaction collider will be (same direction the character is facing)
        if(moveDir.y > 0) player.spriteDir = Player.Direction.N;
        else if(moveDir.y < 0) player.spriteDir = Player.Direction.S;
	}

	public override void Idle(){}
	
	public override void Action(){
		if(player.interactable != null)
			player.interactable.OnInteract(this.player.gameObject);
	}
	public override void ActionRelease(){}
	
	public override void AltAction(){}
	public override void AltActionRelease(){}
	
	public override void Cancel(){ this.speed = runSpeed; }
	public override void CancelRelease(){ this.speed = walkSpeed; }
	
	public override void Start(){
		if(pauseMenu){
			pauseMenu.SetActive(true);
			// Should change this to menu controller? Ideally it should be able
			// to handle both battle menus and pause menu
			
		}
	}

	public override void StartRelease(){}
}


