using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	public Texture pic;
	public RawImage splashRawImage;
	public Text startText;
	public Text crapMode;
	public Text superMode;

	public void StartGame() {
		startText.text = "Loading...";
		SceneManager.LoadScene (1);
	}

	public void ShowTurtorial () {
		splashRawImage.texture = pic;
	}

	public void CrapMode () {
		crapMode.text  = "Crap mode enabled";
		superMode.text = "";
		QualitySettings.SetQualityLevel(0, true);
	}

	public void SuperMode () {
		superMode.text = "Super mode enabled";
		crapMode.text  = "";
		QualitySettings.SetQualityLevel(5, true);
	}

}
