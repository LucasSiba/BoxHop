﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {

	public GameObject envSpawnObject;
	private Transform envTransform;
	private AudioSource audioSource;
	public  AudioClip   winSound;

	public GameObject EnemyFireBox1;
	public GameObject EnemyFireBox2;
	public GameObject EnemyPillarOfDoom;
	public GameObject EnemyBouncer;

	static bool switchToNextLevel = false;
	public static int currentLevel = 0;
	public Text levelText;

	private int Level1EnemyCurCount =  0;
	public int  Level1EnemyMaxCount =  5;
	public AudioClip Level1Audio;

	private int Level2EnemyCurCount =  0;
	public int  Level2EnemyMaxCount = 12;
	public AudioClip Level2Audio;

	private int Level3EnemyCurCount =  0;
	public int  Level3EnemyMaxCount = 25;
	public AudioClip Level3Audio;

	private int Level4EnemyCurCount =  0;
	public int  Level4EnemyMaxCount = 10;
	public AudioClip Level4Audio;

	private int Level5EnemyCurCount =  0;
	public int  Level5EnemyMaxCount = 30;

	void Awake() {
		Application.targetFrameRate = 60;
	}

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
			if (currentLevel == 3 && Level3EnemyCurCount >= Level3EnemyMaxCount) { SwitchToNextLevel (); }
			if (currentLevel == 4 && Level4EnemyCurCount >= Level4EnemyMaxCount) { SceneManager.LoadScene (2); }
			if (currentLevel == 5 && Level5EnemyCurCount >= Level5EnemyMaxCount) { GameIsWon (); }
		}
	}

	void GameIsWon () {
		currentLevel = 0;
		levelText.text = "You win!";
		audioSource.clip = winSound;
		audioSource.Play ();
		audioSource.loop = false;
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
				rb.AddExplosionForce (1000, new Vector3 (0.0f, 0.0f, 0.0f), 500, 0.5f, ForceMode.Force);
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

		if (currentLevel == 4) {
			InvokeRepeating ("Level4Enemies", 0.0f, 0.2f);
			audioSource.clip = Level4Audio;
			audioSource.Play ();
		}

		if (currentLevel == 5) {
			InvokeRepeating ("Level5Enemies", 0.0f, 0.5f);
			audioSource.clip = Level1Audio;
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

	void Level4Enemies () {
		Vector3 spawnPointPos = new Vector3 (UnityEngine.Random.Range(-9, 9), 5, UnityEngine.Random.Range(-9, 9));
		Quaternion spawnPointRot = new Quaternion(UnityEngine.Random.Range (0, 180), UnityEngine.Random.Range (0, 180), UnityEngine.Random.Range (0, 180), UnityEngine.Random.Range (0, 180));
		GameObject newEnemy = Instantiate(EnemyBouncer, spawnPointPos, spawnPointRot) as GameObject;
		newEnemy.transform.SetParent (envTransform);

		Level4EnemyCurCount++;
		if (Level4EnemyCurCount > Level4EnemyMaxCount) {
			CancelInvoke ("Level4Enemies");
		}
	}

	void Level5Enemies () {
		Vector3 spawnPointPos = new Vector3 (UnityEngine.Random.Range(-7, 7), 5, UnityEngine.Random.Range(0, 13));
		Quaternion spawnPointRot = new Quaternion(UnityEngine.Random.Range (0, 180), UnityEngine.Random.Range (0, 180), UnityEngine.Random.Range (0, 180), UnityEngine.Random.Range (0, 180));
		GameObject newEnemy = Instantiate(EnemyFireBox1, spawnPointPos, spawnPointRot) as GameObject;
		newEnemy.transform.SetParent (envTransform);

		Level5EnemyCurCount++;
		if (Level5EnemyCurCount > Level5EnemyMaxCount) {
			CancelInvoke ("Level5Enemies");
		}

	}

}