using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

	public float      speed = 5f;
	public float      yJumpSpeed = 1.1f;
	public Text       debugText;
	public GameObject fellText;
	public bool       DEBUG_MODE = true;

	private Rigidbody rb;
	private Vector3   movement;
	private Vector2   fingerStartPos;

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

	// Taking user input and moving the player... should this be Update() instead?
	void FixedUpdate () {
		int   x = 0;
		float y = 0;
		int   z = 0;

		#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
		if (Input.GetKey("space")) { y =  yJumpSpeed; }
		if (Input.GetKey("up")    || Input.GetKey("w")) { z =  1; }
		if (Input.GetKey("down")  || Input.GetKey("s")) { z = -1; }
		if (Input.GetKey("left")  || Input.GetKey("a")) { x = -1; }
		if (Input.GetKey("right") || Input.GetKey("d")) { x =  1; }
		#else

		int touchCount = Input.touchCount;
		for (int i = 0; i < touchCount; i++) {

			Touch touch = Input.GetTouch(i);
			Vector2 touchPosition = touch.position;
			
			if (DEBUG_MODE) {
				if (i == 0) { debugText.text = ""; }
				// Is there a snprintf() formatted print equivalent in C# ?
				debugText.text += "Touch Position (" + i + "): X:"
					+ touchPosition.x.ToString ("F0") + " (" + Screen.width.ToString ("F0") + "), Y:"
					+ touchPosition.y.ToString ("F0") + " (" + Screen.height.ToString ("F0") + ") - ";
			}

			if (touchPosition.y < (Screen.height / 2)) {
				if (touchPosition.x > (Screen.width * (2.0f / 3.0f))) {
					// Jump Button
					if (DEBUG_MODE) { debugText.text += "Jump\n"; }
					y = yJumpSpeed;
					continue;
				}
			}

		    // Handle finger movements based on touch phase.
		    switch (touch.phase) {
		    case TouchPhase.Began:
				if (DEBUG_MODE) { debugText.text += "Joystick\n"; }
				fingerStartPos = touch.position;
  		        break;

		    // Determine direction by comparing the current touch position with the initial one.
			default:
				if (DEBUG_MODE) { debugText.text += "Joystick\n"; }
				if (touch.position.x > (fingerStartPos.x + (Screen.height / 15))) { x =  1; }
				if (touch.position.x < (fingerStartPos.x - (Screen.height / 15))) { x = -1; }
				if (touch.position.y > (fingerStartPos.y + (Screen.height / 15))) { z =  1; }
				if (touch.position.y < (fingerStartPos.y - (Screen.height / 15))) { z = -1; }
		        break;
		    }
		}
		#endif

		movement.Set (x, 0, z);

		// Normalise the movement vector and make it proportional to the speed per second.
		movement = movement.normalized * speed * Time.deltaTime;
		movement.y = y * speed * Time.deltaTime;
		// Move the player to it's current position plus the movement.
		rb.MovePosition (transform.position + movement);
	}
}
