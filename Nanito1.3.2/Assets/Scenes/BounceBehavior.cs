using UnityEngine;
using System.Collections;

public class BounceBehavior : MonoBehaviour {
	public GameObject bouncy;
	public bool bounce = false;

	void Start()
	{
		bouncy = GameObject.FindWithTag ("bouncy");
	}


	void OnCollisionEnter2D(Collision2D otherObject)
	{
		if (otherObject.gameObject.tag == "Player") {
			Vector3 bounce = new Vector3 (150, 150, 0);
			otherObject.gameObject.GetComponent<Rigidbody2D> ().AddForce (bounce, ForceMode2D.Impulse);
		}
	}

	void Update()
	{
		
	}
}
