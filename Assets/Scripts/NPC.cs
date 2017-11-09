using UnityEngine;
using System.Collections;

public class NPC : Interactable {

	// FIXME: How to represent side-quests or events?
	public readonly string NPCName;
	private Dialogue dialogue;
	private int localEventFlags;	// Flags to represent this npc's events, i.e
									// first time talking, or side quest done.

	
	public override void OnInteract(){

	}
}