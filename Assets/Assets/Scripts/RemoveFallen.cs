using UnityEngine;
using System.Collections;

public class RemoveFallen : MonoBehaviour {

	void Update () {
		CheckChildren (transform);
	}

	void CheckChildren (Transform tr) {
		// Check all sub-children
		int children = tr.childCount;
		for (int i = 0; i < children; ++i) {
			CheckChildren (tr.GetChild (i));
		}

		if (tr == transform) {
			return;
		}

		// then check this child
		if (tr.position.y < -10) {
			Destroy (tr.gameObject);
		} else {
			Rigidbody rb = tr.gameObject.GetComponent<Rigidbody> ();
			if (rb == null && children == 0) {
				Destroy (tr.gameObject);
			}
		}
	}
}
