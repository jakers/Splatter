using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour {
	private static HexGrid grid;
	public int radius;

	public List<Hex> goals;

	public static List<Hex> goalHexes;

	public List<HexTile> goalHexTiles;

	public static HashSet<Hex> visited;
	public static HashSet<Hex> goalNodes;

	public static List<GameObject> gameobjectHexTiles;

	public static Animator beatLevelShared;

	public static bool isGridComplete;


	void Awake () {
		beatLevelShared = GetComponent<Animator>();
		isGridComplete = false;	
		InitalizeGrid ();
	}

	public static void SetHex(HexTile hexTile) {
		grid.setHexToColor (hexTile.X, hexTile.Z, hexTile.paintColor);

		Hex hex = grid.getHexAt(hexTile.X, hexTile.Z);
		hex.isGoalTile = hexTile.isGoalTile;
		hex.paintColor = hexTile.paintColor;
	}

	public static List<Hex> GetConnectedNeighbors(HexTile hexTile) {
		List<Hex> neighbors = grid.getNeighbors (hexTile.ToHex());
		List<Hex> connectedNeighbors = new List<Hex> ();

		foreach (Hex neighbor in neighbors) {
			if (PaintColorMap.doColorsConnect(hexTile.paintColor, neighbor.paintColor)) {
				Debug.Log(hexTile.paintColor + " =====> " + neighbor.paintColor);
				connectedNeighbors.Add (neighbor);
			}	
		}
		return connectedNeighbors;
	}

	public static bool CheckIfCompleted() {
		if (!isGridComplete) {

			goalNodes = new HashSet<Hex> (goalHexes);
			visited.Clear ();

			Hex current = goalHexes [0];

			isGridComplete = search (current);

			if (isGridComplete) {
				LevelManager levelManager = GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<LevelManager> ();
				levelManager.BeatALevel (SceneManager.GetActiveScene ().buildIndex);
				Animator anim = GameObject.FindGameObjectWithTag ("BeatLevelBanner").GetComponent<Animator> () as Animator;
				anim.SetBool ("isLevelBeaten", true);
			}

			return isGridComplete;
		} else {
			return isGridComplete;
		}
	}
			
	public static bool search(Hex current) {
		RemoveIfCurrentNodeIsAGoalNode (current);

		return AllGoalNodesAreReached () || CheckForConnectedNeighbors (current);
	}

	private void InitalizeGrid() {
		grid = new HexGrid (radius);
		goalHexes = new List<Hex>();

		foreach (HexTile hexTile in goalHexTiles) {
			goalHexes.Add (grid.getHexAt(hexTile.X, hexTile.Z));	
		}

		Debug.Log ("GOAL LENGTH: " + goalHexes.Count);

		gameobjectHexTiles = new List<GameObject>(GameObject.FindGameObjectsWithTag ("Tile"));

		visited = new HashSet<Hex> ();
		goalNodes = new HashSet<Hex> (goalHexes);
	}

	private static void RemoveIfCurrentNodeIsAGoalNode(Hex current) {
		if (goalNodes.Contains (current)) {
			goalNodes.Remove (current);
		}
	}

	private static bool AllGoalNodesAreReached() {
		return goalNodes.Count == 0;
	}

	private static bool hasNotVisited (Hex neighbor)
	{
		return !visited.Contains (neighbor);
	}

	private static bool CheckForConnectedNeighbors(Hex current) {
		bool isCompleted = false;
		foreach (Hex neighbor in grid.getNeighbors(current)) {
			if (neighbor.paintColor != PaintColor.None) {
				if (hasNotVisited (neighbor)) {
					if (PaintColorMap.doColorsConnect (current.paintColor, neighbor.paintColor)) {
						visited.Add (current);
						isCompleted = search (neighbor) || isCompleted;
					}
				}
			}
		}
		return isCompleted;
	}
}
