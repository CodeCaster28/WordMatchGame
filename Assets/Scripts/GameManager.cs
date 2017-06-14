using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public List<Letter> letters;
	public Letter[,] tiles = new Letter[4, 4];
	public LineRenderer line;

	private void Awake() {
		line = GetComponentInChildren<LineRenderer>();
	}

	private void Start() {

		letters = transform.Find("Letters").GetComponentsInChildren<Letter>().ToList();
		Populate();
		Tilelize();
	}

	private void Tilelize() {

		for (int i = 0; i < 4; i++) {

			for (int p = 0; p < 4; p++) {

				tiles[i, p] = letters.ElementAt(p + (i * 4));
				Debug.Log("Writing |" + letters.ElementAt(p + (i * 4)).textChar.text + "| to tiles [" + i + "," + p + "]");
			}
		}

		for (int i = 0; i < 4; i++) {

			for (int p = 0; p < 4; p++) {
				Debug.Log("P: " + p + ", I: " + i);
				if (InRange(i - 1, p)) tiles[i, p].SetNeighbour(tiles[i - 1, p]);
				if (InRange(i + 1, p)) tiles[i, p].SetNeighbour(tiles[i + 1, p]);
				if (InRange(i - 1, p - 1)) tiles[i, p].SetNeighbour(tiles[i - 1, p - 1]);
				if (InRange(i - 1, p + 1)) tiles[i, p].SetNeighbour(tiles[i - 1, p + 1]);
				if (InRange(i + 1, p - 1)) tiles[i, p].SetNeighbour(tiles[i + 1, p - 1]);
				if (InRange(i + 1, p + 1)) tiles[i, p].SetNeighbour(tiles[i + 1, p + 1]);
				if (InRange(i, p + 1)) tiles[i, p].SetNeighbour(tiles[i, p + 1]);
				if (InRange(i, p - 1)) tiles[i, p].SetNeighbour(tiles[i, p - 1]);
			}
		}
	}

	private bool InRange(int a, int b) {
		if ((a >= 0 && a <= 3) && (b >= 0 && b <= 3)) {
			Debug.Log("Success: " + tiles[a, b].textChar.text);
			return true;
		}
		else {
			Debug.Log("Out of range- [" + a + ", " + b + "]- skipping.");
			return false;
		}
	}

	private void Populate() {

		Debug.Log(letters[3]);
		foreach (Letter letter in letters)
			letter.SetChar((char)('A' + Random.Range(0, 26)));
	}
}
