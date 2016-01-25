using UnityEngine;
using System.Collections;

public class ShieldScript : MonoBehaviour {

	public int shieldNumber = 1;
	public GameObject nanitoGO;

	// Use this for initialization
	void Start () {
		

	}

	void OnCollisionEnter2D(Collision2D collision){

		HealthScript playerHealth = GetComponent<HealthScript> ();
		OrangeCell orangeCell = collision.gameObject.GetComponent<OrangeCell>();
		RedCell redCell = collision.gameObject.GetComponent<RedCell>();
		BlueCell blueCell = collision.gameObject.GetComponent<BlueCell>();
		NanitoControllerScript nanito = nanitoGO.GetComponent<NanitoControllerScript>();

		if (collision.gameObject.tag == "orange") {
			
			if(nanito.shieldGO.activeSelf == true) {
				nanito.damagePlayer = false;
				nanito.shieldHits--;
				Debug.Log("shield hit");
				//orangeCell = null;
				if(playerHealth != null)
					playerHealth.Damage(orangeCell.damage,nanito.respawnPosX,nanito.respawnPosY,true);

			}
			else{
				nanito.damagePlayer = true;
				Debug.Log("shield not hit");
				if(playerHealth != null) 
					playerHealth.Damage(orangeCell.damage,nanito.respawnPosX,nanito.respawnPosY,false);
			}
		}

		if (collision.gameObject.tag == "red") {
			
			if(nanito.shieldGO.activeSelf == true) {
				nanito.damagePlayer = false;
				nanito.shieldHits--;
				Debug.Log("shield hit");
				//orangeCell = null;
			}
			else{
				nanito.damagePlayer = true;
				Debug.Log("shield not hit");
				if(playerHealth != null) 
					playerHealth.Damage(redCell.damage,nanito.respawnPosX,nanito.respawnPosY,false);
			}
		}

		if (collision.gameObject.tag == "blue") {
			
			if(nanito.shieldGO.activeSelf == true) {
				nanito.damagePlayer = false;
				nanito.shieldHits--;
				Debug.Log("shield hit");
				//orangeCell = null;
			}
			else{
				nanito.damagePlayer = true;
				Debug.Log("shield not hit");
				if(playerHealth != null) 
					playerHealth.Damage(blueCell.damage,nanito.respawnPosX,nanito.respawnPosY, false);
			}
		}

	}


}
