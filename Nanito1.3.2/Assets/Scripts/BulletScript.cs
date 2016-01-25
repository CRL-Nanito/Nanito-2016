using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public int count;
	
	void OnTriggerEnter2D(Collider2D collision){

		MetalCell metalCell = collision.gameObject.GetComponent<MetalCell>();
		NanitoControllerScript nanito = collision.gameObject.GetComponent<NanitoControllerScript>();


		
		if (collision.gameObject.tag == "Enemy") {
			Destroy(this.gameObject);
			Destroy(collision.gameObject);	
		}

		if (collision.gameObject.tag == "orange") {
			Destroy(this.gameObject);
			Destroy(collision.gameObject);
			
		}

		if (collision.gameObject.tag == "red") {
			Destroy(this.gameObject);
			Destroy(collision.gameObject);

		}

		if (collision.gameObject.tag == "blue") {
			Destroy(this.gameObject);
			Destroy(collision.gameObject);

		}

		if (collision.gameObject.tag == "metal") {
			metalCell.count++;
			Destroy(this.gameObject);

		}
		
		if (collision.gameObject.tag == "platform") {
			Destroy(this.gameObject);
			
		}
		
		if (collision.gameObject.tag == "lever") {
			Destroy(this.gameObject);
			
		}

		
		if (collision.gameObject.tag == "lever1") {
			Destroy(this.gameObject);
			
		}
		
		if (collision.gameObject.tag == "lever") {
			if (LeverScript.hitcount == 2) {
				RobotArm.move = true;
				collision.isTrigger = true;
			} else LeverScript.hitcount += 1;
		}
		
		if (collision.gameObject.tag == "lever1") {
			if (LeverScript.hitcount1 == 2) {
				RobotArm.move1 = true;
				collision.isTrigger = true;
			} else LeverScript.hitcount1 += 1;
			
		}
		
		
	}
	
}
