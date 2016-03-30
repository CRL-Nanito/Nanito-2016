using UnityEngine;
using System.Collections;

public class PopUpScript : MonoBehaviour {
	public GameObject popup;
	public Sprite atomo1;

	void OnTriggerEnter2D(Collider2D otherObject){
		
		if(otherObject.tag == "Player") {
			popup.GetComponent<SpriteRenderer>().sprite = atomo1;
			popup.SetActive(true);
			this.GetComponent<SpriteRenderer>().enabled = false;
			this.GetComponent<Collider2D>().enabled = false;
		}
	}
		
	void OnTriggerEnter(Collider otherObject){
		
		if(otherObject.tag == "Player") {
			Debug.Log ("sii");
			popup.GetComponent<SpriteRenderer>().sprite = atomo1;
			popup.SetActive(true);
			this.gameObject.SetActive (false);
		}
	}

	void Update () {
		if (Input.GetButtonDown("Return")) {
			popup.SetActive(false);
		}

	}
	
}
