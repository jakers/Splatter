using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	public int maxLevelBeaten;

	// Use this for initialization
	void Awake () {
		//PlayerPrefs.DeleteAll ();
		if (PlayerPrefs.HasKey ("MaxLevel")) {
			maxLevelBeaten = PlayerPrefs.GetInt ("MaxLevel");
		} else {
			PlayerPrefs.SetInt ("MaxLevel", 0);
		}
		DontDestroyOnLoad (gameObject.transform);
	}

	public void BeatALevel(int level) {
		if (level > maxLevelBeaten) {
			maxLevelBeaten = level;
			PlayerPrefs.SetInt ("MaxLevel", level);
		}
	}
}
