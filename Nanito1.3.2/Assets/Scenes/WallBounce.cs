using UnityEngine;
using System.Collections;

public class WallBounce : MonoBehaviour {
	public GameObject bouncy;

	void Start()
	{
		bouncy = GameObject.FindWithTag ("bouncy");
	}

	void OnCollisionEnter2D(Collision2D otherObject)
	{
		if (otherObject.gameObject.tag == "Player") {
			Vector3 bounce = new Vector3 (50, 50, 50);
			otherObject.gameObject.GetComponent<Rigidbody2D> ().AddForce (bounce, ForceMode2D.Impulse);
		}
	}

	void Update()
	{

	}
}
