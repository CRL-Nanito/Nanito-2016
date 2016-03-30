using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {

	public float minTime = 0.05f;
	public float maxTime = 1.2f;

	private float timer;
	private Light Source;

	// Use this for initialization
	void Start () {

		Source = GetComponent<Light>();
		timer = Random.Range (minTime, maxTime);
	
	}
	
	// Update is called once per frame
	void Update () {

		timer -= Time.deltaTime;
		if (timer <= 0) {
			Source.enabled = !Source.enabled;
			timer = Random.Range (minTime, maxTime);

		}
	
	}
}
