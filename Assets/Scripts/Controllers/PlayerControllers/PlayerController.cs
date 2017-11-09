using UnityEngine;
using System.Collections;

public abstract class PlayerController : MonoBehaviour {

	public abstract void Horizontal();
	public abstract void Vertical();
	public abstract void Action();
	public abstract void Cancel();
	public abstract void Start();
}
