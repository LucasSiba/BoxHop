using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	public Texture pic;
	public RawImage splashRawImage;

	public void StartGame() {
		SceneManager.LoadScene (1);
	}

	public void ShowTurtorial () {
		splashRawImage.texture = pic;
	    //GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height), pic);
	}
}
