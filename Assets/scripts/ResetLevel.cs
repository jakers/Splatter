using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour {

	public void OnClick() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
		Debug.Log ("HI");
	}
}
