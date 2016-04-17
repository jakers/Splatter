using UnityEngine;
using System.Collections;

public class BeatLevelBanner : MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	public void PlayBeatenLevelAnimation() {
		anim.SetBool ("isLevelBeaten", true);
	}
}
