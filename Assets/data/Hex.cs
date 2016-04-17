using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hex {
	public int X { get; set;}
	public int Z { get; set;}
	public PaintColor paintColor { get; set;}
	public bool isGoalTile;

	public Hex(int x, int z) {
		X = x;
		Z = z;
		paintColor = PaintColor.None;
		isGoalTile = false;
	}

	public Hex(int x, int z, PaintColor color) {
		X = x;
		Z = z;
		paintColor = color;
		isGoalTile = false;
	}
}
