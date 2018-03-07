using System;
using UnityEngine;
using System.Collections;

public class InteractableCollider : MonoBehaviour {

	private Player p;

	void Awake(){
		p = transform.parent.GetComponent<Player>();
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log("[MSG]: " + this + " Setting interactable to " + other.gameObject);
		p.SetInteractable(other.gameObject.GetComponent<Interactable>());
	}

	void OnTriggerExit2D(Collider2D other){
		Debug.Log("[MSG]: " + this + " Setting interactable to null");
		p.SetInteractable(null);
	}
}