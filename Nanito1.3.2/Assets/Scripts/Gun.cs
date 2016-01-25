using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	
	public float fireRate = 0;
	public float Damage = 10;
	
	public GameObject projectile;
	public float speed;
	public float delay;
	
	float timeToFire = 0;
	Transform firePoint;
	
	GameObject clone;
	public GameObject nanitoGO;
	
	// Use this for initialization
	void Awake () {
		firePoint = transform.FindChild("FirePoint");
		if (firePoint == null) {
			Debug.LogError("No firePoint? WHAT!");
		}
		
		
	}
	
	// Update is called once per frame
	void Update () {
		//Shoot ();
		if (fireRate == 0) {
			if(Input.GetButtonDown("Fire1")){
				Shoot();
				
			}
		}
		else {
			if(Input.GetButtonDown("Fire1") && Time.time > timeToFire){
				timeToFire = Time.time + 1/fireRate;
				Shoot();
			}
		}
		
	}
	void Shoot(){
		//Debug.Log ("Test");
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		
		NanitoControllerScript nanito = nanitoGO.GetComponent<NanitoControllerScript>();
		
		//Debug.Log ("Test");
		
		clone = (GameObject) Instantiate(projectile, firePointPosition, Quaternion.identity);
		//clone.tag = "bullet";
		if (nanito.facingRight) 
			clone.gameObject.GetComponent<Rigidbody2D> ().velocity = transform.right * speed;
		else {
			clone.gameObject.GetComponent<Rigidbody2D> ().velocity = -transform.right * speed;
		}
		
		
	}
	
	IEnumerator Destroy(){
		
		yield return new WaitForSeconds(delay);
		Destroy (clone);
		
	}
}