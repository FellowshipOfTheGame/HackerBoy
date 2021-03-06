using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

	public bool fastText = false;
	public bool diagEventIsRunning = false; // test
	public AudioSource source;
	public GameObject textBox; // Text box sprite
	public GameObject autoAdvanceText;
	public Image portrait;

	public bool sentenceFinished { 
		get; private set; 
	}

	private bool autoAdvance = false;
	private Text dialogueText, nameText;
	private Queue<Sentence> sentences;
	private Sentence currentSentence;
	private Dialogue currentDiag;

	void Awake(){
		DontDestroyOnLoad(this.gameObject);
	}

	void Start () {

		sentences = new Queue<Sentence>();
		Text[] aux = textBox.GetComponentsInChildren<Text>();

		dialogueText = aux[0];
		nameText = aux[1];
		textBox.SetActive(false); // Dectivate textbox
		autoAdvanceText.SetActive(autoAdvance);
	}

	public void ToggleAutoAdvance(){
		autoAdvance = !autoAdvance;
		autoAdvanceText.SetActive(autoAdvance);
		
		// FIXME: need to see how to stop a single coroutine
		// Workaround
		/* NOTE: solution!
		Coroutine co;
		if(sentenceFinished){
			if(autoAdvance) {
				co = StartCoroutine(AutoAdvance());
			} else {
				StopCoroutine(co);
			}
		}*/
		if(sentenceFinished){
			if(autoAdvance) StartCoroutine(AutoAdvance());
			else StopAllCoroutines(); // StopCoroutine(AutoAdvance());
		}
	}

	public void Reset(){
		sentences.Clear();
		dialogueText.text = "";
		nameText.text = "";
		textBox.SetActive(false);
	}
	public void StartDialog(Dialogue d, string name, Sprite portrait){
		
		// Clear current dialog state
		sentences.Clear();
		currentDiag = d; // Assign new dialog
		this.portrait.sprite = portrait;
		textBox.SetActive(true); // Activate textbox
		nameText.text = name;

		foreach(Sentence sentence in d.sentences)
			sentences.Enqueue(sentence);

		NextSentence();
	}

	// TODO need a callback for when dialogue events finish
	public void NextSentence(){

		if(diagEventIsRunning) return; // Do nothing while a dialogue event is happening

		if(sentences.Count == 0){
			EndDialogue();
			return;
		}

		currentSentence = sentences.Dequeue();
		dialogueText.text = "";

		// StopCoroutine(PrintChar(currentSentence.text));
		StopAllCoroutines(); // This only stop coroutines on THIS behaviour

		// TODO: block text advance if a dialogue event is happening
		// check for coroutine returning?
		// if(currentSentence.diagEvent != null) currentSentence.diagEvent();
		if(currentSentence.diagEvent != null)
			CallDialogueEvent(currentSentence.diagEvent);
		if(currentSentence.voice != null) {
			source.clip = currentSentence.voice;
			source.Play();
		}

		sentenceFinished = false;
		StartCoroutine(PrintChar(currentSentence.text));
	}

	public void EndDialogue(){
		if(currentDiag.endDialog != null)
			currentDiag.endDialog();
		Reset();
	}

	// This function skips only until next dialogue event
	public void Skip(){}

	public void EndSentence(){
		StopAllCoroutines(); // This only stop coroutines on THIS behaviour
		PrintSentence(currentSentence.text);
	}

	// TODO need a callback for when dialogue events finish
	private void CallDialogueEvent(DEvent devent){
		diagEventIsRunning = true;
		devent();
		diagEventIsRunning = false;
	}

	private void PrintSentence(string sentence){
		dialogueText.text = sentence;
		sentenceFinished = true;
		if(autoAdvance) StartCoroutine(AutoAdvance());
	}

	private IEnumerator PrintChar(string sentence){

		char[] aux = sentence.ToCharArray();
		for(int i = 0; i < aux.Length; i++){
			
			dialogueText.text += aux[i];
			
			// Print additional char
			if(fastText && ++i < aux.Length)
				dialogueText.text += aux[i];

			yield return null;
			if(!fastText) yield return null;
		}

		sentenceFinished = true;
		if(autoAdvance) StartCoroutine(AutoAdvance());
	}

	private IEnumerator AutoAdvance(){
		yield return new WaitForSeconds(currentSentence.delay);
		NextSentence(); // TODO need a callback for when dialogue events finish
	}
}
