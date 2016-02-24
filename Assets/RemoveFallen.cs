using UnityEngine;
using System.Collections;

public class RemoveFallen : MonoBehaviour {

	public GameObject policeCarPrefab;

	void Update () {
		CheckChildren (transform);
	}

	void CheckChildren (Transform tr) {
		// Check all sub-children
		int children = tr.childCount;
		for (int i = 0; i < children; ++i) {
			CheckChildren (tr.GetChild (i));
		}

		// then check this child
		if (tr.position.y < -10) {
			Destroy (tr.gameObject);
			if (tr.gameObject.name == "Cube") {
				SpawnPoliceCar ();
			}
		} else {
			Rigidbody rb = tr.gameObject.GetComponent<Rigidbody> ();
			if (rb == null && children == 0) {
				Destroy (tr.gameObject);
			}
		}
	}

	void SpawnPoliceCar () {
	    Vector3 spawnPointPos = new Vector3 (0.0f, 10.0f, 0.0f);
	    Quaternion spawnPointRot = new Quaternion(Random.Range (0, 180), Random.Range (0, 180), Random.Range (0, 180), Random.Range (0, 180));
	    GameObject newCar = Instantiate(policeCarPrefab, spawnPointPos, spawnPointRot) as GameObject;
	    newCar.transform.SetParent (transform);
	}

}
