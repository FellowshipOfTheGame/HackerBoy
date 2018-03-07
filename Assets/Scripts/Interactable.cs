using UnityEngine;

public abstract class Interactable : MonoBehaviour {

    public abstract void OnInteract(GameObject player);
    public abstract void OnFinishInteract();

    void OnTriggerEnter2D(Collider2D other){
		if(other.tag.Equals("Player")){
			other.gameObject.GetComponent<Player>().SetInteractable(this);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag.Equals("Player")){
			other.gameObject.GetComponent<Player>().SetInteractable(null);
		}
	}
}
