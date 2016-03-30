using UnityEngine;
using System.Collections;

public class boxRotation : MonoBehaviour {

	public int speed = 3;

	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime * speed);
	}
}
