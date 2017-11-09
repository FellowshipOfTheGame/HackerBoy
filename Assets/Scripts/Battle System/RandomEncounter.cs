using UnityEngine;
using System.Collections;

public static class RandomEncounter {

	public enum EncounterRate {
		Low,
		Normal,
		High
	}

	public static EncounterRate rate = EncounterRate.Normal;

	public static void StartEncounter(){
		// Transition to battle scene
	}
}