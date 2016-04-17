using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReturnToLevelSelect : MonoBehaviour {

	public void OnClick() {
		SceneManager.LoadScene ("Level_Selecting_Scene");
	}
}
