using UnityEngine;

public class GameManager : MonoBehaviour {

	public bool gamePaused { get; private set; }

	void Start(){
		DontDestroyOnLoad(this.gameObject);
	}

	void Update(){

	}
}
