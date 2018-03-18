using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC : Interactable {

	// FIXME: How to represent side-quests or events?
	public string NPCName;
	public Sprite portrait;

	private PlayerController pcHolder;
	private Player p;
	private Dialogue dialogue;
	private DialogManager dm;
	private int localEventFlags;	// Flags to represent this npc's events, i.e
									// first time talking, or side quest done.

	void Start(){
		this.dm = GameObject.Find("DialogManager").GetComponent<DialogManager>();
	}

	private void DebugCamEffect(){
		ScreenFader sf = GameObject.Find("Main Camera").GetComponent<ScreenFader>();
		sf.StartFadeIn();
	}

	private void DebugCamEffect2(){
		ScreenFader sf = GameObject.Find("Main Camera").GetComponent<ScreenFader>();
		sf.StartFadeOut();
	}

	private void DebugDialogue(){
		this.dialogue = new Dialogue(6);
		this.dialogue.endDialog = OnFinishInteract;
		
		dialogue.sentences[0].text = "Olá, eu sou o Dejurg, o Jureg testador.\nEstou aqui para testar interação e diálogo!";
		dialogue.sentences[1].text = "Como vai seu dia?";
		dialogue.sentences[2].text = "Olha o efeito de câmera!";
		dialogue.sentences[3].text = "Desfazendo o efeito";
		dialogue.sentences[4].text = "Anal, diálogo tá acabando, queria conversar mais :(";
		dialogue.sentences[5].text = "Preciso ir-me, bom desenvolvimento!";

		dialogue.sentences[0].diagEvent = null;
		dialogue.sentences[1].diagEvent = null;
		dialogue.sentences[2].diagEvent = DebugCamEffect;
		dialogue.sentences[3].diagEvent = DebugCamEffect2;
		dialogue.sentences[4].diagEvent = null;
		dialogue.sentences[5].diagEvent = null;

		dm.StartDialog(this.dialogue, "DeJurg", this.portrait);
	}
	
	public override void OnInteract(GameObject playerObj){

		// Store previous controller
		this.p = playerObj.GetComponent<Player>();
		this.pcHolder = this.p.pc;
		DebugDialogue();
		this.p.pc = new TextController();
	}

	public override void OnFinishInteract(){

		// Restore previous controller to player
		this.p.pc = this.pcHolder;
	}
}