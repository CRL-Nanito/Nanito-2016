using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NanitoControllerScript : MonoBehaviour {

	public float maxSpeed = 10f;
	public bool facingRight = true;
	private float move;
	public GameObject gunGO;

	Animator anim;
	Animator bridgeAnim;

	float wingsFactor = 600f;
	int wingsCounter = 0;
	bool grounded = false;
	bool shieldFlag = false;
	public bool damagePlayer = false;
	bool blueCellDamage = false;
	bool orangeCellDamage = false;
	bool redCellDamage = false;

	int counter;
	bool gunFlag;
	public int shieldHits;

	public float respawnPosX = -284.7662f;
	public float respawnPosY = -8.521203f;

	public GameObject shieldGO;

	public Camera camera;

	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 1500f;

	bool doubleJump = false;
	private int time;
	private Time timer;

	public Texture2D background;
	public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flas

	public GameObject PopUp;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		BridgePlatformScript bridge = GetComponent<BridgePlatformScript>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		PopUpScript popup = GetComponent<PopUpScript> ();

		//detects colliders
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded); //idle anim

		if (grounded)
			doubleJump = false;

//		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y); //how fast r we going up or down


		move = Input.GetAxis ("Horizontal");

		//mid-air movement enabler
		//if (!grounded && Mathf.Abs(move) > 0.01f) return;

		//handles the anims
		anim.SetFloat ("Speed", Mathf.Abs (move));

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D> ().velocity.y);

		if (move > 0 && !facingRight) {
			Flip ();
			float x = PopUp.transform.localScale.x;
			float y = PopUp.transform.localScale.y;
			float z = PopUp.transform.localScale.z;
			PopUp.transform.localScale = new Vector3 (-x, y, z);
		}
		else if (move < 0 && facingRight) {
			Flip ();
			float x = PopUp.transform.localScale.x;
			float y = PopUp.transform.localScale.y;
			float z = PopUp.transform.localScale.z;
			PopUp.transform.localScale = new Vector3 (-x, y, z);
		}

//		if (Input.GetMouseButtonDown (0)) {
//			Destroy(this.gameObject);
//		}
	}

	void OnDestroy(){
		//check that the player is dead
		HealthScript playerHealth = this.GetComponent<HealthScript> ();
		if (playerHealth != null && HealthScript.hp <= 0) {				//en vez de playerHealth.hp use HealthScript.hp ya que hp es una variable global
			//game over bitches
			Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		bool hell = false;

		//collision with ff
		FerrofluidScript goo = collision.gameObject.GetComponent<FerrofluidScript> ();
		HealthScript playerHealth = GetComponent<HealthScript> ();
		FloorScript floor = collision.gameObject.GetComponent<FloorScript> ();
		PopUpScript popUp = collision.gameObject.GetComponent<PopUpScript> ();
		BossFight boss = collision.gameObject.GetComponent<BossFight> ();
		ShieldScript shield = collision.gameObject.GetComponent<ShieldScript> ();
		ShieldNumber shieldnumber = collision.gameObject.GetComponent<ShieldNumber> ();
		FfScript ffBottle = collision.gameObject.GetComponent<FfScript> ();
		WingsCounter wings = collision.gameObject.GetComponent<WingsCounter> ();
		RobotArm robot = GetComponent<RobotArm> ();
		OrangeCell orangeCell = collision.gameObject.GetComponent<OrangeCell>();
		RedCell redCell = collision.gameObject.GetComponent<RedCell>();
		BlueCell blueCell = collision.gameObject.GetComponent<BlueCell>();
		MetalCell metalCell = collision.gameObject.GetComponent<MetalCell>();
		MazePlatform maze = collision.gameObject.GetComponent<MazePlatform>();

		
		//BridgePlatformScript bridge = GetComponent<BridgePlatformScript> ();

		if (collision.gameObject.tag == "orange") {
//			damagePlayer = true;
//			Debug.Log("shield not hit");
//			if(playerHealth != null) 
//				playerHealth.Damage(orangeCell.damage,respawnPosX,respawnPosY,false);

			if(shieldGO.activeSelf == true) {
				damagePlayer = false;
				shieldHits--;
				metalCell.hitByShield = true;
				Debug.Log("shield hit");
				//orangeCell = null;
				if(playerHealth != null)
					playerHealth.Damage(orangeCell.damage,respawnPosX,respawnPosY,true);
				
			}
			else{
				damagePlayer = true;
				Debug.Log("shield not hit");
				if(playerHealth != null) 
					playerHealth.Damage(orangeCell.damage,respawnPosX,respawnPosY,false);
			}
		}

		if (collision.gameObject.tag == "red") {
			damagePlayer = true;
			Debug.Log("shield not hit");
			if(playerHealth != null) 
				playerHealth.Damage(redCell.damage,respawnPosX,respawnPosY,false);
		}

		if (collision.gameObject.tag == "blue") {
			damagePlayer = true;
			Debug.Log("shield not hit");
			if(playerHealth != null) 
				playerHealth.Damage(blueCell.damage,respawnPosX,respawnPosY,false);
		}

		if (collision.gameObject.tag == "metal") {
			//			damagePlayer = true;
			//			Debug.Log("shield not hit");
			//			if(playerHealth != null) 
			//				playerHealth.Damage(orangeCell.damage,respawnPosX,respawnPosY,false);
			
			if(shieldGO.activeSelf == true) {
				damagePlayer = false;
				shieldHits--;
				metalCell.hitByShield = true;
				Debug.Log("shield hit");
				
			}
			else{
				damagePlayer = true;
				Debug.Log("shield not hit");
				if(playerHealth != null) 
					playerHealth.Damage(orangeCell.damage,respawnPosX,respawnPosY,false);
			}
		}

			
		if (floor != null) {
			damagePlayer = true;
			hell = true;
		}


		if (hell) {
			if(playerHealth != null) playerHealth.Damage(floor.damage,respawnPosX,respawnPosY,false);
		}
		if(collision.gameObject.name == "Checkpoint1") {
			CheckpointScript checkpoint = collision.gameObject.GetComponent<CheckpointScript>();
			respawnPosX = checkpoint.posX;
			respawnPosY = checkpoint.posY;
			Destroy(collision.gameObject.GetComponent<Collider2D>());
		}
		if(collision.gameObject.name == "Checkpoint2") {
			CheckpointScript checkpoint = collision.gameObject.GetComponent<CheckpointScript>();
			respawnPosX = checkpoint.posX;
			respawnPosY = checkpoint.posY;
			Destroy(collision.gameObject.GetComponent<Collider2D>());
		}
		if(collision.gameObject.name == "Checkpoint3") {
			CheckpointScript checkpoint = collision.gameObject.GetComponent<CheckpointScript>();
			respawnPosX = checkpoint.posX;
			respawnPosY = checkpoint.posY;
			Destroy(collision.gameObject.GetComponent<Collider2D>());
		}
		if(collision.gameObject.name == "Checkpoint4") {
			CheckpointScript checkpoint = collision.gameObject.GetComponent<CheckpointScript>();
			respawnPosX = checkpoint.posX;
			respawnPosY = checkpoint.posY;
			Destroy(collision.gameObject.GetComponent<Collider2D>());
		}

//		if (collision.gameObject.name == "atomo") {
//			popUp.showPopUp = true;
//			popUp.gameObject.GetComponent<Renderer>().enabled = false;
//			Destroy(popUp.gameObject.GetComponent<Collider2D>());
//		}

		if (collision.gameObject.tag == "MovingPlatform"){
			this.transform.parent = collision.transform;
		}

//		if (collision.gameObject.tag == "lever"){
//			RobotArm.move = true;
//		}
//
//		if (collision.gameObject.tag == "lever1") {
//			RobotArm.move1 = true;
//		}

		if (collision.gameObject.tag == "Boss") {
			boss.gameObject.GetComponent<Renderer>().enabled = true;
			
			showPopUp = true;
			maxSpeed = 0;
			
			if (i == 5) {
				maxSpeed = 25;
				Destroy(boss.gameObject);
				boss.gameObject.GetComponent<Renderer>().enabled = false;
			}
		}

		if (collision.gameObject.tag == "Door") {
			Application.LoadLevel("Hidrofobia");
		}

		if (wings != null) {
			wings.GetComponent<Renderer>().enabled = false;
			wings.GetComponent<PolygonCollider2D>().enabled = false;
			wingsCounter = 7;
		}

		if (collision.gameObject.tag == "shield") {
			ShieldCounterManager.AddShield(shieldnumber.shieldNumber);
			Destroy(shieldnumber.gameObject);
			shieldFlag = true;
			Debug.Log("Shield available");
		}


		if (collision.gameObject.tag == "ffbottle") {
			FfCounterManager.AddFF(ffBottle.ffNumber);
			gunFlag = true;
			Destroy(ffBottle.gameObject);
		}

		//platform activator
		if (collision.gameObject.tag == "ActivatePlatform") {
			collision.gameObject.GetComponent<Animator>().enabled = true;
		}

		if (collision.gameObject.tag == "Boots") {
			collision.gameObject.GetComponent<Renderer>().enabled = false;
			collision.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
			time = 5;
		}

		if (collision.gameObject.tag == "box") {
			bool founded = false;
			BoxController box = collision.gameObject.GetComponent<BoxController>();
			box.Founded(true);
			Destroy(collision.gameObject);
		}

		if (collision.gameObject.tag == "Bridge1") {
			BridgePlatformScript.bridge1 = true;

		}
		if (collision.gameObject.tag == "Bridge2") {
			BridgePlatformScript.bridge2 = true;

		}
		if (collision.gameObject.tag == "Bridge3") {
			BridgePlatformScript.bridge3 = true;

		}
		if (collision.gameObject.tag == "Bridge4") {
			BridgePlatformScript.bridge4 = true;

		}
		if (collision.gameObject.tag == "Bridge5") {
			BridgePlatformScript.bridge5 = true;

		}
		if (collision.gameObject.tag == "Bridge6") {
			BridgePlatformScript.bridge6 = true;

		}
		if (collision.gameObject.tag == "Bridge7") {
			BridgePlatformScript.bridge7 = true;

		}
		if (collision.gameObject.name == "MetallicFloorFerroFluidMAP 159") {
			MazePlatform.ground = true;
			transform.parent = collision.transform;
		}

		
		
	}
	
	void Update(){
		// si brinco desde el piso o desde el primer brinco
		if ((grounded || !doubleJump) && Input.GetButtonDown ("Jump")) {
			
			//BRETTY GOOD, EH?
			anim.SetBool ("Ground", false);
			this.transform.parent = null;
			float newJumpForce = jumpForce;
			
			//UPDATE WINGS COUNTER ACCORDING TO DOUBLE JUMP OR WINGS COUNTER
			if (wingsCounter > 0) {
				newJumpForce += wingsFactor;
				wingsCounter--;
			}
			if (!doubleJump && !grounded) {
				newJumpForce /= 2;
				doubleJump = true;
			}
			
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, newJumpForce));
		}

		//activate gun
		if ((gunFlag == true || shieldGO.activeSelf == true) && Input.GetButtonDown ("Fire1")) {
			shieldGO.SetActive(false);
			gunGO.SetActive (true);
			Debug.Log ("say hello");
			counter++;
			
			if(Input.GetButtonDown ("Fire1") && counter >= 2){
				counter++;
				Debug.Log("shot fired");
				FfCounterManager.ffScore--;
			}
			
			if(FfCounterManager.ffScore == 0){
				gunFlag = false;
				gunGO.SetActive(false);
			}
			
		}

		//activate shield
		if ((shieldFlag == true || gunGO.activeSelf == true) && Input.GetButtonDown ("Shield")) {
			gunGO.SetActive (false);
			shieldGO.SetActive (true);
			ShieldCounterManager.shieldScore--;
			shieldHits = 3;
			Debug.Log ("shield me bitch");
			counter = 0;

			if (shieldFlag == true && ShieldCounterManager.shieldScore == 0) {
				shieldFlag = false;
				shieldGO.SetActive(false);
			}
		}

		if (shieldHits == 0)
			shieldGO.SetActive (false);

		//laberinto 1 camera zoom
		if (((-143.4f) < this.gameObject.transform.position.x && this.gameObject.transform.position.x < (-12.6f)) && ((23f) < this.gameObject.transform.position.y && this.gameObject.transform.position.y < (103.8f))) {
			camera.orthographicSize = 15;
		} 
		//laberinto 2 zoom
		else if (((820.4f) < this.gameObject.transform.position.x && this.gameObject.transform.position.x < (1051.9f)) && ((-134.8f) < this.gameObject.transform.position.y && this.gameObject.transform.position.y < (-16.2f))) {
			camera.orthographicSize = 15;
		}
		else {
			camera.orthographicSize = 25;
		}



//		if (gunGO.activeSelf == true && Input.GetButtonDown ("Shield")) {
//			gunGO.SetActive(false);
//			shieldGO.SetActive(true);
//		}
//		
		
		// If the player has just been damaged...
		if(damagePlayer)
		{
			// ... set the colour of the damageImage to the flash colour.
			damageImage.color = flashColour;
		}
		// Otherwise...
		else
		{
			// ... transition the colour back to clear.
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		
		// Reset the damaged flag.
		damagePlayer = false;


	}



	//Flip world 180
	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	void OnCollisionExit2D(Collision2D collision){
		if (collision.gameObject.tag == "MovingPlatform"){
			this.transform.parent = null;
		}

	}   

	public bool showPopUp = false;
	
	public static int correct_answer = 0;
	int i = 0;
	int clicks = 0;

	void OnGUI()
	{		
		//show window if you touched collider
		if (showPopUp == true) {
			GUI.Window (0, new Rect ((Screen.width / 2) - 350, (Screen.height / 2) - 130, 300, 250), ShowGUI, "BOSS FIGHT");
			GUI.DrawTexture (new Rect ((Screen.width / 2) - 350, (Screen.height / 2) - 130, 300, 250), background);
		}
	}


	
	void ShowGUI(int windowID)
	{	
		bool damagePlayer = false;
		HealthScript playerHealth = this.GetComponent<HealthScript> ();

		switch (i) {
		case 0:
			GUI.Label (new Rect (30, 40, 250, 500),  "¿A que se debe la forma peculiar de un ferrofluido?\n [A] Las lineas del campo magnetico\n [B] Gravedad\n [C] Temperatura\n");
			if (GUI.Button (new Rect (70, 175, 40, 30), "A")) {
				correct_answer = 1;
				i++;
			} else
			if (GUI.Button (new Rect (120, 175, 40, 30), "B")) {
				correct_answer = 0;
				clicks++;
				if(playerHealth != null) {
					playerHealth.Damage(4,respawnPosX,respawnPosY,false);
				} 
				if (clicks == 3) {
					showPopUp = false;
					maxSpeed = 30;
					anim.enabled = true;
					clicks = 0;
				}
			} else
			if (GUI.Button (new Rect (170, 175, 40, 30), "C")) {
				correct_answer = 0;
				clicks++;
				if(playerHealth != null) {
					playerHealth.Damage(4,respawnPosX,respawnPosY,false);
				} 
				if (clicks == 3) {
					showPopUp = false;
					maxSpeed = 30;
					anim.enabled = true;
					clicks = 0;
				}
			}
			break;
		
		case 1:
			GUI.Label (new Rect (30, 40, 250, 500), "¿Que es un ferrofluido?\n [A] Un liquido que no se polariza en un campo magnetico\n [B] Un liquido que repele el agua\n [C] Un liquido que se polariza en un campo magnetico\n");
			if (GUI.Button (new Rect (70, 175, 40, 30), "A")) {
				correct_answer = 0;
				clicks++;
				if(playerHealth != null) {
					playerHealth.Damage(4,respawnPosX,respawnPosY,false);
				} 
				if (clicks == 3) {
					showPopUp = false;
					maxSpeed = 30;
					anim.enabled = true;
					clicks = 0;
				}
			} else
			if (GUI.Button (new Rect (120, 175, 40, 30), "B")) {
				correct_answer = 0;
				clicks++;
				if(playerHealth != null) {
					playerHealth.Damage(4,respawnPosX,respawnPosY,false);
				} 
				if (clicks == 3) {
					showPopUp = false;
					maxSpeed = 30;
					anim.enabled = true;
					clicks = 0;
				}
			} else
			if (GUI.Button (new Rect (170, 175, 40, 30), "C")) {
				correct_answer = 1;
				i++;
			}
			break;

		case 2:
			GUI.Label (new Rect (30, 40, 250, 500), "¿Como los ferrofluidos ayudan a combatir el cancer?\n [A] Los ferrofluidos no son capaces de combatir el cancer\n [B] Quimeoterapia usando ferrofluidos\n [C] Utilizando una tecnica llamada cocinar el tumor\n");
			if (GUI.Button (new Rect (70, 175, 40, 30), "A")) {
				correct_answer = 0;
				clicks++;
				if(playerHealth != null) {
					playerHealth.Damage(4,respawnPosX,respawnPosY,false);
				} 
				if (clicks == 3) {
					showPopUp = false;
					maxSpeed = 30;
					anim.enabled = true;
					clicks = 0;
				}
			} else
			if (GUI.Button (new Rect (120, 175, 40, 30), "B")) {
				correct_answer = 0;
				clicks++;
				if(playerHealth != null) {
					playerHealth.Damage(4,respawnPosX,respawnPosY,false);
				} 
				if (clicks == 3) {
					showPopUp = false;
					maxSpeed = 30;
					anim.enabled = true;
					clicks = 0;
				}
			} else
			if (GUI.Button (new Rect (170, 175, 40, 30), "C")) {
				correct_answer = 1;
				i++;
			}
			break;

		case 3:
			GUI.Label (new Rect (30, 40, 250, 500), "¿Que le pasa a un iman y un metal si se le aplica ferrofluido?\n [A] Aumenta la friccion\n [B] Disminuye la friccion\n [C] Se queda igual\n");
			if (GUI.Button (new Rect (70, 175, 40, 30), "A")) {
				correct_answer = 0;
				clicks++;
				if(playerHealth != null) {
					playerHealth.Damage(4,respawnPosX,respawnPosY,false);
				} 
				if (clicks == 3) {
					showPopUp = false;
					maxSpeed = 30;
					anim.enabled = true;
					clicks = 0;
				}
			} else
			if (GUI.Button (new Rect (120, 175, 40, 30), "B")) {
				correct_answer = 1;
				i++;
			} else
			if (GUI.Button (new Rect (170, 175, 40, 30), "C")) {
				correct_answer = 0;
				clicks++;
				if(playerHealth != null) {
					playerHealth.Damage(4,respawnPosX,respawnPosY,false);
				} 
				if (clicks == 3) {
					showPopUp = false;
					maxSpeed = 30;
					anim.enabled = true;
					clicks = 0;
				}
			}
			break;

		case 4:
			GUI.Label (new Rect (30, 40, 250, 500), "¿En que parte de los robots se utiliza ferrofluidos?\n [A] La cabeza\n [B] Las coyunturas\n [C] El cuello\n");
			if (GUI.Button (new Rect (70, 175, 40, 30), "A")) {
				correct_answer = 0;
				clicks++;
				if(playerHealth != null) {
					playerHealth.Damage(4,respawnPosX,respawnPosY,false);
				} 
				if (clicks == 3) {
					showPopUp = false;
					maxSpeed = 30;
					anim.enabled = true;
					clicks = 0;
				}
			} else
			if (GUI.Button (new Rect (120, 175, 40, 30), "B")) {
				correct_answer = 1;
				i++;
			} else
			if (GUI.Button (new Rect (170, 175, 40, 30), "C")) {
				correct_answer = 0;
				clicks++;
				if(playerHealth != null) {
					playerHealth.Damage(4,respawnPosX,respawnPosY,false);
				} 
				if (clicks == 3) {
					showPopUp = false;
					maxSpeed = 30;
					anim.enabled = true;
					clicks = 0;
				}
			}
			break;

		case 5:
			showPopUp = false;
			break;
		}
	}
}
