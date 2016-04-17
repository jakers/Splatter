using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DraggingPaint : MonoBehaviour {
	public PaintColor color;
	public PaintIcon icon;

	public List<GameObject> colorPrefabs;
	public List<Sprite> colorSprites;
	public AudioClip clip;

	public void OnDrop(UnityEngine.EventSystems.BaseEventData eventData) {
		
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
		if (hit.collider != null) {
			HexTile hexTile = hit.collider.gameObject.GetComponent<HexTile> ();

			if (hexTile.paintColor == PaintColor.None) {
				GameObject paintColorIcon = colorPrefabs [GetColorIndex ()];

				hexTile.paintColor = color;
				GridManager.SetHex (hexTile);
				GameObject go1 = Instantiate (paintColorIcon);
				go1.transform.position = hexTile.transform.position;
				go1.transform.SetParent (hexTile.transform);
				go1.transform.localScale = new Vector3 (0.025F, 0.025F, 1F);

				AudioSource audioSource = GetComponent<AudioSource> ();
				audioSource.PlayOneShot (clip, 2f);

				// call search algorithm to see if tiles are occuplied
				List<Hex> connnectedNeighbors = GridManager.GetConnectedNeighbors (hexTile);

				foreach (Hex connectedNeighbor in connnectedNeighbors) {
					HexTile neighborTile = null;

					foreach (GameObject go in GridManager.gameobjectHexTiles) {
						
						HexTile possibleHexTile = go.GetComponent<HexTile> ();
						
						if (possibleHexTile.X == connectedNeighbor.X && possibleHexTile.Z == connectedNeighbor.Z) {
							neighborTile = possibleHexTile;
							break;
						}
					}

					hexTile.setLine (neighborTile);
					neighborTile.setLine (hexTile);
				}

				GridManager.CheckIfCompleted ();

		

				if (icon.count == 0) {
					Destroy (icon.gameObject);
				}
			}
			Destroy (gameObject);
		} 
	
    else {
			icon.count++;
			icon.textCount.text = icon.count.ToString ();

			PointerEventData ped = new PointerEventData(EventSystem.current);  
			ped.position = Input.mousePosition;

			List<RaycastResult> hits = new List<RaycastResult>();

			EventSystem.current.RaycastAll(ped, hits);

			// check any hits to see if any of them are blocking UI elements

			foreach (RaycastResult r in hits) {
				if (r.gameObject.CompareTag ("LeftMixing") || r.gameObject.CompareTag ("RightMixing")) {
					Image image = r.gameObject.GetComponent<Image> ();
					image.sprite = colorSprites [GetColorIndex ()];
					image.color = Color.white;

					//r.gameObject.transform.localScale = new Vector3 (0.5f, 0.5f, 1f);


					if (r.gameObject.CompareTag ("LeftMixing")) {
						icon.mixer.leftColor = icon;
					} else if (r.gameObject.CompareTag ("RightMixing")) {
						icon.mixer.rightColor = icon;
					}

					break;
				} 
						}

			Destroy (gameObject);
		}
	}

	public void setColor(PaintIcon paintIcon) {
		color = paintIcon.paintColor;
		GetComponent<Image>().sprite = paintIcon.GetComponent<Image> ().sprite;
	}

	public int GetColorIndex() {
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
		default:
			index = 0;
			break;
		}
		return index;
	}

}
