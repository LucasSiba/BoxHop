using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

	public float      speed = 2f;
	public Text       debugText;
	public GameObject fellText;
	public bool       DEBUG_MODE = true;

	private Rigidbody rb;
	private Vector3   movement;

	void Start () {
		rb = GetComponent <Rigidbody> ();

		if (DEBUG_MODE) {
			// Make a text box over the screen for debug info
			debugText.rectTransform.sizeDelta = new Vector2 (Screen.width - 40, Screen.height - 40);
		}
	}

	void Update () {
		if (transform.position.y < -10) {
			Invoke("RestartLevel", 2);
			fellText.SetActive(true);
		}
	}

	void RestartLevel () {
		SceneManager.LoadScene (0);
	}

	void FixedUpdate () {
		int touchCount = 0;

		#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
		if (Input.GetMouseButton(0)) { touchCount = 1;	}
		#else
		touchCount = Input.touchCount;
		#endif

		int x = 0;
		int y = 0;
		int z = 0;

		for (int i = 0; i < touchCount; i++) {
			#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
			Vector2 touchPosition = Input.mousePosition;
			#else              
		    Touch touch = Input.GetTouch(i);
		    Vector2 touchPosition = touch.position;
			#endif

			if (DEBUG_MODE) {
				if (i == 0) { debugText.text = ""; }
				// Is there a snprintf() formatted print equivalent in C# ?
				debugText.text += "Touch Position (" + i + "): X:"
				+ touchPosition.x.ToString ("F0") + " (" + Screen.width.ToString ("F0") + "), Y:"
				+ touchPosition.y.ToString ("F0") + " (" + Screen.height.ToString ("F0") + ") - ";
			}

			// Only check the lower 1/2 of the screen for touches ("Touchable Area")
			if (touchPosition.y < (Screen.height / 2)) {
				// Divide the lower 1/2 into thirds (joystick on the left, jump button on the right, nothing in the middle)
				if (touchPosition.x < (Screen.width / 3)) {
					// Joystick
					if (DEBUG_MODE) { debugText.text += "Joystick\n"; }
					// Is the touch in the given quadrent of the joystick area? (minus a small dead-zone in the middle)
					if (touchPosition.y > (((Screen.height / 2) / 2) + (Screen.height / 15))) { z =  1; }
					if (touchPosition.y < (((Screen.height / 2) / 2) - (Screen.height / 15))) { z = -1; }
					if (touchPosition.x > (((Screen.width  / 3) / 2) + (Screen.width  / 15))) { x =  1; }
					if (touchPosition.x < (((Screen.width  / 3) / 2) - (Screen.width  / 15))) { x = -1; }
				}
				else if (touchPosition.x > (Screen.width * (2.0f/3.0f))) {
					// Jump Button
					if (DEBUG_MODE) { debugText.text += "Jump\n"; }
					y = 1;
				}
				else {
					if (DEBUG_MODE) { debugText.text += "non-touch area\n"; }
				}
			} else {
				if (DEBUG_MODE) { debugText.text += "non-touch area\n"; }
			}				
		}

		#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
		if (touchCount == 0) {
			if (Input.GetKey("space"))                      { y =  1; }
			if (Input.GetKey("up")    || Input.GetKey("w")) { z =  1; }
			if (Input.GetKey("down")  || Input.GetKey("s")) { z = -1; }
			if (Input.GetKey("left")  || Input.GetKey("a")) { x = -1; }
			if (Input.GetKey("right") || Input.GetKey("d")) { x =  1; }
		}
		#endif

		// Set the movement vector based on the axis input.
		movement.Set (x, y, z);
		// Normalise the movement vector and make it proportional to the speed per second.
		movement = movement.normalized * speed * Time.deltaTime;
		// Move the player to it's current position plus the movement.
		rb.MovePosition (transform.position + movement);
	}
}
