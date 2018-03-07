using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DEvent();

[System.Serializable]
public class Dialogue {
	public Sentence[] sentences;
	public DEvent endDialog;

	public Dialogue(int n){
		sentences = new Sentence[n];

		for(int i = 0; i < n; i++)
			sentences[i] = new Sentence();
	}
}

[System.Serializable]
public class Sentence {
	
	[TextArea(3, 5)]
	public string text;
	
	private float _delay; 
	private float TIME_PER_CHAR = 0.5f; // In seconds
	public float delay { // Delay for auto advance 
		get { return this._delay; }
		set { this._delay = text.Length*TIME_PER_CHAR; }
	}
	
	public AudioClip voice;

	// Dialogue event
	public DEvent diagEvent;
}
