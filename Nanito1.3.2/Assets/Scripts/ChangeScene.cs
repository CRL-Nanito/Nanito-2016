using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {
	
	//public GameObject nanito;
	public GameObject manager;
	
	// Receives parameter to change scenes
	public int sceneSelected;
	
	public void sceneSelect (int sceneSelected1) {
		if (sceneSelected1 == 100) {
			Application.LoadLevel ("LevelSelect");
		} else {
			sceneSelected = sceneSelected1;
			Application.LoadLevel ("LevelSelect");
			DontDestroyOnLoad (manager);
		}
	}
	
	public void ChangeToScene (int gameSelected) {
		if (gameSelected == 0) {
			Application.LoadLevel ("nano");
			//nanito.gameObject.SetActive(true);
		} else if (gameSelected == 1) {
			Application.LoadLevel ("car");
			//			nanito.gameObject.SetActive(true);
		}
		//		DontDestroyOnLoad (characterSelected);
		DontDestroyOnLoad (manager);
	}
	
	
}
