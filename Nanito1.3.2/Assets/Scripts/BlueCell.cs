using UnityEngine;
using System.Collections;

public class BlueCell : MonoBehaviour {

	public int damage = 1;
	public bool slow = true;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnCollisionEnter2D(Collision2D collision){
		
		NanitoControllerScript nanito = GetComponent<NanitoControllerScript> ();
		
		if (collision.gameObject.tag == "bullet") {
			
			Destroy(this.gameObject);
		}
		
	}
}
