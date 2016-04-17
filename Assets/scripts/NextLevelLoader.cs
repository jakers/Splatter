using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextLevelLoader : MonoBehaviour {

	public void OnClick() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}
}
