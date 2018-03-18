using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Chest : Interactable {

	public Item item;

	[UnityEngine.SerializeField]
	private Sprite closedSprite;
	[UnityEngine.SerializeField]
	private Sprite openSprite;

	private bool open = false;
	private PlayerController pcHolder;
	private Player p;
	private Dialogue dialogue;
	private DialogManager dm;

	void Start(){
		this.dm = GameObject.Find("DialogManager").GetComponent<DialogManager>();
	}

	private void GetItemDialogue(){
		
		// Add item and open chest
		this.p.inventory.AddItem(this.item);
		this.open = true;
		this.GetComponent<SpriteRenderer>().sprite = openSprite;
		
		// Prepare dialogue
		this.dialogue = new Dialogue(1);
		this.dialogue.endDialog = OnFinishInteract;

		this.dialogue.sentences[0].text = "Found a " + item.name;
		this.dialogue.sentences[0].diagEvent = null;

		dm.StartDialog(this.dialogue, "", null);
	}

	private void OpenedChestDialogue(){
		this.dialogue = new Dialogue(1);
		this.dialogue.endDialog = OnFinishInteract;
		
		dialogue.sentences[0].text = "Already open";
		dialogue.sentences[0].diagEvent = null;

		dm.StartDialog(this.dialogue, "", null);
	}
	
	public override void OnInteract(GameObject playerObj){

		// Store previous controller
		this.p = playerObj.GetComponent<Player>();
		this.pcHolder = this.p.pc;
		
		if(!open) GetItemDialogue();
		else OpenedChestDialogue();
		
		this.p.pc = new TextController();
	}

	public override void OnFinishInteract(){
		// Restore previous controller to player
		this.p.pc = this.pcHolder;
	}
}