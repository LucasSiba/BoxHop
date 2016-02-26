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
	public GameObject EnemyFireBox2;

	public int currentLevel = 0;
	public Text levelText;

	public int enemiesPerLevel = 5;

	private Transform envTransform;

	private int Level1EnemyCount = 0;
	private int Level2EnemyCount = 0;


	void Start() {
		envTransform = envSpawnObject.GetComponent<Transform> ();
	}

	static public void SwitchToNextLevel() {
		switchToNextLevel = true;
	}
	
	void Update() {
		if (switchToNextLevel) {
			switchToNextLevel = false;
			ClearTheBoard (0);
			ClearTheGameLogicStuff ();
			currentLevel++;
			StartLevelText ();
			StartLoadLevelAssets ();
		}

		if (envTransform.childCount == 0) {
			if (currentLevel == 1 && Level1EnemyCount >= enemiesPerLevel) { SwitchToNextLevel (); }
			if (currentLevel == 2 && Level2EnemyCount > enemiesPerLevel * 2) { GameIsWon (); }
		}
	}

	void GameIsWon () {
		currentLevel = 0;
		levelText.text = "You win!";
		Invoke("DidntWin",  3);
	}

	void DidntWin() {
		levelText.text = "";
		ClearTheBoard (1);
	}

	void ClearTheBoard(int clearAll) {
		Collider[] colliders = Physics.OverlapSphere (new Vector3(0.0f, 0.0f, 0.0f), 1000);
		foreach (Collider c in colliders) {
			Rigidbody rb = c.GetComponent<Rigidbody> ();
			if (rb == null) continue; 
			if (clearAll != 0 || rb.gameObject.name != "Player") {
				rb.AddExplosionForce (500, new Vector3(0.0f, 0.0f, 0.0f), 200, 1.0f, ForceMode.Force);
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

	void StartLoadLevelAssets () { Invoke ("LoadLevelAssets", 3); }

	void LoadLevelAssets () {
		if (currentLevel == 1) {
			InvokeRepeating ("InvokeEnemyFireBox1", 0.0f, 1.5f);
		}

		if (currentLevel == 2) {
			InvokeRepeating ("InvokeEnemyFireBox2", 0.0f, 1.5f);
		}
	}

	void InvokeEnemyFireBox1 () {
		Vector3 spawnPointPos = new Vector3 (UnityEngine.Random.Range(-9, 9), 5, UnityEngine.Random.Range(-9, 9));
		Quaternion spawnPointRot = new Quaternion(UnityEngine.Random.Range (0, 180), UnityEngine.Random.Range (0, 180), UnityEngine.Random.Range (0, 180), UnityEngine.Random.Range (0, 180));
		GameObject newEnemy = Instantiate(EnemyFireBox1, spawnPointPos, spawnPointRot) as GameObject;
		newEnemy.transform.SetParent (envTransform);

		if (currentLevel == 1) {
			Level1EnemyCount++;
			if (Level1EnemyCount > enemiesPerLevel) {
				CancelInvoke ("InvokeEnemyFireBox1");
			}
		}
	}



	void InvokeEnemyFireBox2 () {
		Vector3 spawnPointPos = new Vector3 (UnityEngine.Random.Range(-9, 9), 5, UnityEngine.Random.Range(-9, 9));
		Quaternion spawnPointRot = new Quaternion(UnityEngine.Random.Range (0, 180), UnityEngine.Random.Range (0, 180), UnityEngine.Random.Range (0, 180), UnityEngine.Random.Range (0, 180));
		GameObject newEnemy = Instantiate(EnemyFireBox2, spawnPointPos, spawnPointRot) as GameObject;
		newEnemy.transform.SetParent (envTransform);

		if (currentLevel == 2) {
			Level2EnemyCount++;
			if (Level2EnemyCount > enemiesPerLevel * 2) {
				CancelInvoke ("InvokeEnemyFireBox2");
			}
		}
	}

}


