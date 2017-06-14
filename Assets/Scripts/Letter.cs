using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Letter : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler {

	private InputHandler handler;
	public Text textChar;
	private Image image;
	private Color defColor;
	private List<Letter> neighbours;
	private GameManager game;
	private bool highlighted;
	private Vector3 anchor;

	private void Awake() {

		textChar = GetComponentInChildren<Text>();
		image = transform.GetChild(0).GetComponent<Image>();
		handler = GetComponentInParent<InputHandler>();
		game = GetComponentInParent<GameManager>();
		defColor = image.color;
		neighbours = new List<Letter>();
		anchor = GetComponent<RectTransform>().anchoredPosition;
	}

	private void Start() {

	}

	public void SetNeighbour(Letter neighbour) {
		neighbours.Add(neighbour);
	}

	/*public void PrintNeighbour() {

		Debug.Log("Printing...");
		foreach(Letter n in neighbours) {
			Debug.Log(n.textChar.text);
		}
	}*/

	public void SetChar(char c) {
		textChar.text = c.ToString();
	}

	public void OnPointerDown (PointerEventData eventData) {

		handler.HoldDown = true;

		Press(true);
	}

	public void OnPointerEnter (PointerEventData eventData) {

		if (handler.HoldDown && highlighted == false) {

			foreach (Letter n in handler.lastPressed.neighbours) {
				if (this == n) {
					Press(true);
					break;
				}
			}
		}
	}

	public void OnPointerUp (PointerEventData eventData) {
		game.line.positionCount = 0;
		handler.HoldDown = false;
	}

	public void Press(bool press) {

		if (press) {
			handler.numPressed++;
			game.line.positionCount = handler.numPressed;
			game.line.SetPosition(handler.numPressed - 1, new Vector3(anchor.x, anchor.y, -50));
			handler.lastPressed = this;
			highlighted = true;
			image.color = new Color(0.2f, 0.68f, 0.98f);
		}
		else {
			highlighted = false;
			image.color = defColor;
		}
	}
}
