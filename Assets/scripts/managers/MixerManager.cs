using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MixerManager : MonoBehaviour
{
	public PaintIcon leftColor;
	public PaintIcon rightColor;
	public GameObject pallet;

	public GameObject paintIconPrefab;
	public List<Sprite> colorSprites;

	void Start ()
	{
		leftColor = null;
		rightColor = null;
	}

	public void OnMix () {
		if (leftColor != null && rightColor != null) {
			if (leftColor.count > 0 || rightColor.count > 0) {
				leftColor.SetCount (leftColor.count - 1);
				rightColor.SetCount (rightColor.count - 1);

				PaintColor mixedColor = PaintColorMap.mix (leftColor.paintColor, rightColor.paintColor);

				if (mixedColor != PaintColor.None) {
					PaintIcon[] colors = pallet.GetComponentsInChildren<PaintIcon> ();

					bool colorAlreadyExistsInPallet = false;
					foreach (PaintIcon icon in colors) {
						if (icon.paintColor == mixedColor) {
							icon.count++;
							icon.textCount.text = icon.count.ToString ();
							colorAlreadyExistsInPallet = true;
						}
					}

					if (!colorAlreadyExistsInPallet) {
						GameObject go = Instantiate (paintIconPrefab);
						PaintIcon icon = go.GetComponent<PaintIcon> ();
						icon.mixer = this;
						icon.count = 1;
						icon.textCount.text = icon.count.ToString();
						icon.paintColor = mixedColor;
						icon.GetComponentInChildren<Text> ().color = HexTile.GetColor(mixedColor);
						go.GetComponent<Image> ().sprite = Resources.Load ("Sprites/Paints/" + mixedColor.ToString(), typeof(Sprite)) as Sprite;
						go.transform.SetParent (pallet.transform );
						go.transform.localScale = Vector3.one;
					}
				}

				if (leftColor.count == 0) {
					Destroy (leftColor.gameObject);
				}
				if (rightColor.count == 0) {
					Destroy (rightColor.gameObject);
				}
			}
		}
	}
}