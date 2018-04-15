using UnityEngine;

public class HpMpBarManager : MonoBehaviour {

	public GameObject hpbar;
	public GameObject mpbar;

	private CharacterBase chr;

	void Start(){
		
		chr = gameObject.GetComponentInParent<CharacterBase>() 
			as CharacterBase;
	}

	public void UpdateHp(){
		hpbar.transform.localScale = new Vector2(chr.currentHp/chr.stats.hp, 1);
	}

	public void UpdateMp(){
		mpbar.transform.localScale = new Vector2(chr.currentMp/chr.stats.mp, 1);
	}
}