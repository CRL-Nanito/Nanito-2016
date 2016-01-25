using UnityEngine;
using System.Collections;

public class MazePlatform : MonoBehaviour {


	Animator anim;
	public static bool ground = false;
	
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	void Update () {
		NanitoControllerScript nanito = GetComponent<NanitoControllerScript> ();

		if (ground == true) {
			anim.SetBool ("ground", true);
			
		}
		
	}
}
