using UnityEngine;
using System.Collections;

public class NextLevelCrateScript : MonoBehaviour {

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.name == "Player") {
			NextLevel.SwitchToNextLevel ();
		}
	}
}
