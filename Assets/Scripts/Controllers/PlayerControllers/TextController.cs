using UnityEngine;

public class TextController : PlayerController {

	private DialogManager dm;

	public TextController(){
		this.dm = GameObject.Find("DialogManager").GetComponent<DialogManager>();
	}

	// Axes
	public override void Horizontal(float axisValue){}
	public override void Vertical(float axisValue){}
	public override void Idle(){}
	
	// Buttons
	public override void Action(){
		if(dm.sentenceFinished) dm.NextSentence();
		else dm.EndSentence();
	}
	public override void ActionRelease(){}
	
	public override void AltAction(){ dm.ToggleAutoAdvance(); }
	public override void AltActionRelease(){}

	public override void Cancel(){ dm.fastText = true; }
	public override void CancelRelease(){ dm.fastText = false; }

	public override void Start(){ dm.Skip(); }
	public override void StartRelease(){}
}
