using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NanitoController2Script : MonoBehaviour {

	float maxSpeed = 30f;
	public bool facingRight = true;
	private float move;

	Animator anim;


	bool grounded = false;

	int counter;


	public float respawnPosX = -284.7662f;
	public float respawnPosY = -8.521203f;

	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	float jumpForce = 2000f;

	bool doubleJump = false;
	private int time;
	private Time timer;
                                  
	public GameObject PopUp;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();

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

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (move*maxSpeed, GetComponent<Rigidbody2D> ().velocity.y);

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


	}
		


	void Update(){
		// si brinco desde el piso o desde el primer brinco
		if ((grounded || !doubleJump) && Input.GetButtonDown ("Jump")) {

			//BRETTY GOOD, EH?
			anim.SetBool ("Ground", false);
			this.transform.parent = null;
			float newJumpForce = jumpForce;


			if (!doubleJump && !grounded) {
				newJumpForce /= 2;
				doubleJump = true;
			}

			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, newJumpForce));
		}


	}



	//Flip world 180
	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
		


}
