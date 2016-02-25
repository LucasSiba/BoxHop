using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {

	static bool switchToNextLevel = false;
	public GameObject envSpawnObject;
	public GameObject EnemyFireBox1;

	//public GameObject policeCarPrefab;
	public int currentLevel = 0;
	public Text levelText;

	private Transform envTransform;

	void Start() {
		envTransform = envSpawnObject.GetComponent<Transform> ();
	}

	static public void SwitchToNextLevel() {
		switchToNextLevel = true;
	}
	
	void Update() {
		if (switchToNextLevel) {
			switchToNextLevel = false;
			ClearTheBoard ();
			ClearTheGameLogicStuff ();
			currentLevel++;
			StartLevelText ();
			StartLoadLevelAssets ();
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

	void ClearTheGameLogicStuff () {
		int children = transform.childCount;
		for (int i = 0; i < children; ++i) {
			Transform tr = transform.GetChild (i);
			Destroy(tr.gameObject);
		}
	}

	void StartLevelText () { Invoke("ShowLevelText",  2); }
	void ShowLevelText  () { Invoke("ClearLevelText", 3); levelText.text = "Level " + currentLevel;	}
	void ClearLevelText () { levelText.text = ""; }

//	void SpawnPoliceCar () {
//		Vector3 spawnPointPos = new Vector3 (0.0f, Random.Range(5, 20), 0.0f);
//		Quaternion spawnPointRot = new Quaternion(Random.Range (0, 180), Random.Range (0, 180), Random.Range (0, 180), Random.Range (0, 180));
//		GameObject newCar = Instantiate(policeCarPrefab, spawnPointPos, spawnPointRot) as GameObject;
//		newCar.transform.SetParent (transform);
//	}

	void StartLoadLevelAssets () { Invoke ("LoadLevelAssets", 3); }

	void LoadLevelAssets () {
		if (currentLevel == 1) {
			Vector3 spawnPointPos = new Vector3 (UnityEngine.Random.Range(-5, 5), 5, UnityEngine.Random.Range(-5, 5));
			Quaternion spawnPointRot = new Quaternion(UnityEngine.Random.Range (0, 180), UnityEngine.Random.Range (0, 180), UnityEngine.Random.Range (0, 180), UnityEngine.Random.Range (0, 180));
			GameObject newEnemy = Instantiate(EnemyFireBox1, spawnPointPos, spawnPointRot) as GameObject;
			newEnemy.transform.SetParent (envTransform);
		}
	}
}


