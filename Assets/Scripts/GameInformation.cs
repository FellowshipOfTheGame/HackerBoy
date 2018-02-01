using UnityEngine;
using System.Collections;

public class GameInformation : MonoBehaviour {

	public enum GameFlags {
	}

	private static int gameFlags;
	public static void SetGameFlag(int flag){

	}

	void Awake(){
		DontDestroyOnLoad(transform.gameObject);
	}
}
