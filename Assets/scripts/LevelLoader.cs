using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoader : MonoBehaviour {
	public int level;

	public LevelManager levelManager;
	public Button button;

	private bool LevelIsLocked ()
	{
		return levelManager.maxLevelBeaten + 1 < level;
	}

	bool LevelIsUnlockedButNotBeaten ()
	{
		return levelManager.maxLevelBeaten + 1 == level;
	}

	void Start() {
		levelManager = GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<LevelManager>();

		button = GetComponent<Button> ();

		Text t = GetComponentInChildren<Text> ();
		if (level > 0) {
			t.text = level.ToString ();
		}

		Text text = button.GetComponentInChildren<Text> ();
		text.font = Resources.Load ("Fonts/junegull", typeof(Font)) as Font;
		text.fontSize = 32;

		if (LevelIsLocked ()) {
			button.interactable = false;
			button.image.sprite = Resources.Load ("Sprites/Paints/red", typeof(Sprite)) as Sprite;
			button.image.sprite = null;
			button.image.color = new Color (0F, 0F, 0F, 0F);
			text.color = new Color (0F, 0F, 0F, 0F);

		} else if (LevelIsUnlockedButNotBeaten ()) {
			button.interactable = true;
			button.image.sprite = Resources.Load ("Sprites/Paints/blue", typeof(Sprite)) as Sprite;
			text.color = HexTile.GetColor (PaintColor.ORANGE);
			button.image.color = Color.white;
		} else {
			button.interactable = true;
			button.image.sprite = Resources.Load ("Sprites/Paints/green", typeof(Sprite)) as Sprite;
			text.color = HexTile.GetColor (PaintColor.RED);
			button.image.color = Color.white;
		}
	}

	public void OnClick() {
		SceneManager.LoadScene (level);
	}
}
