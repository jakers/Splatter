using UnityEngine;
using System.Collections;

public class HexTile : MonoBehaviour {

	public int X; 
	public int Z;

	public PaintColor paintColor;

	public bool isGoalTile;

	public LineRenderer[] lineRenders;

	public bool[] lineRendersUsed;

	void Start () {
		if (isGoalTile) {
			GridManager.SetHex(this);
		}	

		lineRenders = gameObject.GetComponentsInChildren<LineRenderer> ();

		lineRendersUsed = new bool[lineRenders.Length];
	}

	public Hex ToHex() {
		Hex hex = new Hex (X, Z, paintColor);
		hex.isGoalTile = isGoalTile;
		return hex;
	}

	public void setLine(HexTile hexTile) {
		
		for (int i = 0; i < lineRendersUsed.Length; i++) {
			if (!lineRendersUsed [i]) {
				lineRendersUsed [i] = true;
				lineRenders [i].SetVertexCount (2);
				lineRenders [i].SetWidth (0.2F, 0.2F);

				Vector3 pos1 = new Vector3 (this.transform.position.x, this.transform.position.y, -5F);
				Vector3 pos2 = new Vector3 (hexTile.transform.position.x, hexTile.transform.position.y, -5F);

				lineRenders [i].SetPosition (0, pos1);
				lineRenders [i].SetPosition (1, pos2);
				Color start = HexTile.GetColor (paintColor);
				Color end = HexTile.GetColor (hexTile.paintColor);
				lineRenders [i].SetColors (start, end);
				break;
			}	
		}
	}

	public static Color GetColor(PaintColor paint) {
		string colorResPath = "Materials/Colors/" + paint.ToString();
		Material mat = Resources.Load(colorResPath, typeof(Material)) as Material;
		return mat.color;
	}




}
