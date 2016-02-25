using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour {

	static bool switchToNextLevel = false;

	public GameObject policeCarPrefab;
	public int currentLevel = 0;

	static public void SwitchToNextLevel() {
		switchToNextLevel = true;
	}
	
	void Update() {
		if (switchToNextLevel) {
			switchToNextLevel = false;
			ClearTheBoard ();
			currentLevel++;
			ShowLevelText ();
		}
	}

	void ClearTheBoard() {
		Collider[] colliders = Physics.OverlapSphere (new Vector3(0.0f, 0.0f, 0.0f), 1000);
		foreach (Collider c in colliders) {
			Rigidbody rb = c.GetComponent<Rigidbody> ();
			if (rb == null) continue; 
			if (rb.gameObject.name != "Player") {
				rb.AddExplosionForce (300, new Vector3(0.0f, 0.0f, 0.0f), 1000, 1.0f, ForceMode.Force);
			}
		}
	}

	void ShowLevelText () {

	}

	void SpawnPoliceCar () {
		Vector3 spawnPointPos = new Vector3 (0.0f, Random.Range(5, 20), 0.0f);
		Quaternion spawnPointRot = new Quaternion(Random.Range (0, 180), Random.Range (0, 180), Random.Range (0, 180), Random.Range (0, 180));
		GameObject newCar = Instantiate(policeCarPrefab, spawnPointPos, spawnPointRot) as GameObject;
		newCar.transform.SetParent (transform);
	}

}


