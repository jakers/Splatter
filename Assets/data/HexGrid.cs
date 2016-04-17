using UnityEngine;
using System.Collections.Generic;

public class HexGrid {
	private Hex[,] hexGrid;
	private int radius;
	private int size;

	public HexGrid(int radius) {
		this.radius = radius;
		size = 1 + 2 * radius;

		hexGrid = new Hex [size, size];

		int r = -radius;
		int q = -radius;

		for (int col = 0; col < size; col++) {
			for (int row = 0; row < size; row++) {

				if (Mathf.Abs(q + r) <= radius) {
					hexGrid[col, row] = new Hex(q, r);
				} else {

					hexGrid[col, row] = null;
				}
				q++;
			}
			q = -radius;
			r++;
		}
	}

	public void setHexToColor(int q, int r, PaintColor color) {
		int col = r + radius;
		int row = q + radius + Mathf.Min (0, Mathf.Abs (r));

		if (col < size && col >= 0 && row < size && row >= 0) {
			hexGrid [col, row].paintColor = color;
		}
	}

	public PaintColor getColorAtHex(int q, int r) {
		PaintColor color = PaintColor.None;

		int col = getColumn(r);
		int row = getRow(q, r);

		if (isInMap(col, row) && hexGrid [col, row] != null) {
			color = hexGrid [col, row].paintColor;
		}

		return color;
	}

	public Hex getHexAt(int q, int r) {
		Hex tile = null;
		
		int col = getColumn(r);
		int row = getRow(q, r);
		
		if (isInMap(col, row)) {
			tile = hexGrid [col, row];
		}
		
		return tile;
	}

	public List<Hex> getAllHexes() {
		List<Hex> allHexes = new List<Hex> ();
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				allHexes.Add (hexGrid[i, j]);
			}
		}

		return allHexes;
	}


	public bool isInMap(int col, int row) {
		return col >= 0 && col < size && row >= 0 && row < size;
	}

	public List<Hex> getNeighbors(Hex hex) {
		List<Hex> neighbors = new List<Hex> ();

		int deltaCol = hex.X;
		int deltaRow = hex.Z - 1;

		// this was easier to implement then a loop for some reason
		if (isInMap (getColumn (deltaRow), getRow (deltaCol, deltaRow))) {
			Hex neighbor = getHexAt (deltaCol, deltaRow);
			if (neighbor != null) {
				neighbors.Add (neighbor);
			}
		}

		deltaCol = hex.X + 1;
		deltaRow = hex.Z - 1;
		
		if (isInMap (getColumn (deltaRow), getRow (deltaCol, deltaRow))) {
			Hex neighbor = getHexAt (deltaCol, deltaRow);
			if (neighbor != null) {
				neighbors.Add (neighbor);
			}
		}

		deltaCol = hex.X + 1;
		deltaRow = hex.Z;
		
		if (isInMap (getColumn (deltaRow), getRow (deltaCol, deltaRow))) {
			Hex neighbor = getHexAt (deltaCol, deltaRow);
			if (neighbor != null) {
				neighbors.Add (neighbor);
			}
		}

		deltaCol = hex.X;
		deltaRow = hex.Z + 1;
		
		if (isInMap (getColumn (deltaRow), getRow (deltaCol, deltaRow))) {
			Hex neighbor = getHexAt (deltaCol, deltaRow);
			if (neighbor != null) {
				neighbors.Add (neighbor);
			}
		}

		deltaCol = hex.X - 1;
		deltaRow = hex.Z + 1;
		
		if (isInMap (getColumn (deltaRow), getRow (deltaCol, deltaRow))) {
			Hex neighbor = getHexAt (deltaCol, deltaRow);
			if (neighbor != null) {
				neighbors.Add (neighbor);
			}
		}

		deltaCol = hex.X - 1;
		deltaRow = hex.Z;
		
		if (isInMap (getColumn (deltaRow), getRow (deltaCol, deltaRow))) {
			Hex neighbor = getHexAt (deltaCol, deltaRow);
			if (neighbor != null) {
				neighbors.Add (neighbor);
			}
		}
		 
		return neighbors;
	}

	public int getColumn(int r) {
		return r + radius;
	}

	public int getRow(int q, int r) {
		return q + radius + Mathf.Min (0, Mathf.Abs (r));
	}

}
