using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {

	public GameObject envSpawnObject;
	private Transform envTransform;
	private AudioSource audioSource;

	public GameObject EnemyFireBox1;
	public GameObject EnemyFireBox2;
	public GameObject EnemyPillarOfDoom;

	static bool switchToNextLevel = false;
	public int currentLevel = 0;
	public Text levelText;

	private int Level1EnemyCurCount =  0;
	public int  Level1EnemyMaxCount =  5;
	public AudioClip Level1Audio;
	private int Level2EnemyCurCount =  0;
	public int  Level2EnemyMaxCount = 10;
	public AudioClip Level2Audio;
	private int Level3EnemyCurCount =  0;
	public int  Level3EnemyMaxCount = 20;
	public AudioClip Level3Audio;


	void Start() {
		envTransform = envSpawnObject.GetComponent<Transform> ();
		audioSource = GetComponent<AudioSource> ();
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
			if (currentLevel == 1 && Level1EnemyCurCount >= Level1EnemyMaxCount) { SwitchToNextLevel (); }
			if (currentLevel == 2 && Level2EnemyCurCount >= Level2EnemyMaxCount) { SwitchToNextLevel (); }
			if (currentLevel == 3 && Level3EnemyCurCount >= Level3EnemyMaxCount) { GameIsWon (); }
		}
	}

	void GameIsWon () {
		currentLevel = 0;
		levelText.text = "You win!";
		Invoke("DidntActuallyWin",  3);
	}

	void DidntActuallyWin() {
		levelText.text = "";
		ClearTheBoard (1);
	}

	void ClearTheBoard(int clearAll) {
		Collider[] colliders = Physics.OverlapSphere (new Vector3(0.0f, 0.0f, 0.0f), 1000);
		foreach (Collider c in colliders) {
			Rigidbody rb = c.GetComponent<Rigidbody> ();
			if (rb == null) continue; 
			if (clearAll != 0) {
				rb.AddExplosionForce (2000, new Vector3 (0.0f, 0.0f, 0.0f), 500, 0.5f, ForceMode.Force);
			} else if (rb.gameObject.name != "Player") {
				rb.AddExplosionForce (500, new Vector3 (0.0f, 0.0f, 0.0f), 200, 1.0f, ForceMode.Force);
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
			InvokeRepeating ("Level1Enemies", 0.0f, 1.5f);
			audioSource.clip = Level1Audio;
			audioSource.Play ();
		}

		if (currentLevel == 2) {
			InvokeRepeating ("Level2Enemies", 0.0f, 1.5f);
			audioSource.clip = Level2Audio;
			audioSource.Play ();
		}

		if (currentLevel == 3) {
			InvokeRepeating ("Level3Enemies", 0.0f, 0.5f);
			audioSource.clip = Level3Audio;
			audioSource.Play ();
		}
	}

	void Level1Enemies () {
		Vector3 spawnPointPos = new Vector3 (UnityEngine.Random.Range(-9, 9), 5, UnityEngine.Random.Range(-9, 9));
		Quaternion spawnPointRot = new Quaternion(UnityEngine.Random.Range (0, 180), UnityEngine.Random.Range (0, 180), UnityEngine.Random.Range (0, 180), UnityEngine.Random.Range (0, 180));
		GameObject newEnemy = Instantiate(EnemyFireBox1, spawnPointPos, spawnPointRot) as GameObject;
		newEnemy.transform.SetParent (envTransform);

		Level1EnemyCurCount++;
		if (Level1EnemyCurCount > Level1EnemyMaxCount) {
			CancelInvoke ("Level1Enemies");
		}

	}

	void Level2Enemies () {
		Vector3 spawnPointPos = new Vector3 (UnityEngine.Random.Range(-9, 9), 5, UnityEngine.Random.Range(-9, 9));
		Quaternion spawnPointRot = new Quaternion(UnityEngine.Random.Range (0, 180), UnityEngine.Random.Range (0, 180), UnityEngine.Random.Range (0, 180), UnityEngine.Random.Range (0, 180));
		GameObject newEnemy = Instantiate(EnemyFireBox2, spawnPointPos, spawnPointRot) as GameObject;
		newEnemy.transform.SetParent (envTransform);

		Level2EnemyCurCount++;
		if (Level2EnemyCurCount > Level2EnemyMaxCount) {
			CancelInvoke ("Level2Enemies");
		}
	}

	void Level3Enemies () {
		Vector3 spawnPointPos = new Vector3 (UnityEngine.Random.Range(-9, 9), 5, UnityEngine.Random.Range(-9, 9));
		Quaternion spawnPointRot = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
		GameObject newEnemy = Instantiate(EnemyPillarOfDoom, spawnPointPos, spawnPointRot) as GameObject;
		newEnemy.transform.SetParent (envTransform);

		Level3EnemyCurCount++;
		if (Level3EnemyCurCount > Level3EnemyMaxCount) {
			CancelInvoke ("Level3Enemies");
		}
	}

}