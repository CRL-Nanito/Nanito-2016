using UnityEngine;
using System.Collections;

public class PulsingLight : MonoBehaviour {

	public Light Source;
	public float PULSE_RANGE = 4.0f;
	public float PULSE_SPEED = 3.0f;

	void Start () {
		Source.range = 0;
	}
	
	void Update () {
		Source.range = Mathf.PingPong(Time.time * PULSE_SPEED, PULSE_RANGE);
		}
}
