using UnityEngine;
using System.Collections;

public class MetalCell : MonoBehaviour {
	
	public int damage = 2;
	public bool slow = false;
	public int count = 0;
	public bool hitByShield = false;

	// Use this for initialization
	void Start () {
		
	}

	void Update () {

		NanitoControllerScript nanito = GetComponent<NanitoControllerScript> ();
		BulletScript bullet = GetComponent<BulletScript> ();

		if (count >= 3 && hitByShield == true) {
			Destroy(this.gameObject);
		}
	}
}
