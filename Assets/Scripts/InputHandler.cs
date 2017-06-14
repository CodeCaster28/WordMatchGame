using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputHandler : MonoBehaviour {

	public List<Letter> letters;
	public bool holdDown;
	public int numPressed = 0;
	public Letter lastPressed;

	private void Awake() {
		letters = GetComponentsInChildren<Letter>().ToList();
	}

	public bool HoldDown {
		get { return holdDown; }
		set {
			holdDown = value;
			if (value == false) {
				ResetButtons();
			}
		}
	}

	public void ResetButtons() {

		foreach (Letter letter in letters) {
			letter.Press(false);
			lastPressed = null;
			numPressed = 0;
		}
	}

}
