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

	// Use this for initialization
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
							Debug.Log ("Color already Exists");
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
						go.GetComponent<Image> ().sprite = colorSprites [GetColorIndex (mixedColor)];
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

	public int GetColorIndex(PaintColor color) {
		int index = 0;

		switch(color) {
		case PaintColor.RED:
			index = 0;
			break;
		case PaintColor.BLUE:
			index = 1;
			break;
		case PaintColor.YELLOW:
			index = 2;
			break;
		case PaintColor.ORANGE:
			index = 3;
			break;
		case PaintColor.GREEN:
			index = 4;
			break;
		case PaintColor.PURPLE:
			index = 5;
			break;

		case PaintColor.AMBER:
			index = 6;
			break;
		case PaintColor.CHARTREUSE:
			index = 7;
			break;
		case PaintColor.MAGENTA:
			index = 8;
			break;
		case PaintColor.TEAL:
			index = 9;
			break;
		case PaintColor.VERMILLION:
			index = 10;
			break;
		case PaintColor.VIOLET:
			index = 11;
			break;
		default:
			index = 0;
			break;
		}
		return index;
	}


}