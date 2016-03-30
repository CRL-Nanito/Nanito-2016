using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

	public float barDisplay; //current progress
	public Vector2 pos = new Vector2(20,80);
	public Vector2 size = new Vector2(90,120);
	public Texture2D emptyTex;
	public Texture2D fullTex;
	public Slider slider;
	public bool refuel;
    public bool cloudRefuel;
	bool circulo = false;
	bool triangulo = false;

    public float energyFactor;

	public Image fadeimage;
	public Image finalimage;
	public float fadespeed = 30f;
	public Color fadecolor = new Color (0f, 0f, 0f, 1f); 

	void Start(){
		slider.value = 100;
        cloudRefuel = true;
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "spotlight" && cloudRefuel) {
			refuel = true;
		} else if (other.gameObject.tag == "teleport 1") {
			triangulo = false;
			circulo = true;
			transform.position = new Vector3 (-280f, -55.6f, 727.6f);
			this.gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			transform.rotation = Quaternion.Euler (359.69f, 179.9999f, 0);

			Debug.Log ("fade back");


		} else if (other.gameObject.tag == "teleport 2") {
			triangulo = true;
			circulo = false;
			transform.position = new Vector3 (-570.7f, -60f, -25.4f);
			transform.rotation = Quaternion.Euler (359.69f, 181.4886f, 0);
			this.gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;		
		
		}
		if (other.gameObject.tag == "finish") {
			Color c = finalimage.color;
			c.a = 255;
			finalimage.color = c;
		}

	}
	void OnTriggerExit(Collider other){
		if(other.gameObject.tag == "spotlight"){
			refuel = false;
		}
		else if(other.gameObject.tag == "teleport 1"){
			fadeimage.color = Color.Lerp (fadeimage.color, Color.clear, fadespeed * Time.deltaTime);

		}
		else if(other.gameObject.tag == "teleport 2"){
			fadeimage.color = Color.Lerp (fadeimage.color, Color.clear, fadespeed * Time.deltaTime);

		}
	}
	
	void Update() {
		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)|| Input.GetAxis("Vertical")> 0 
		   || Input.GetAxis("Vertical")< 0 ){
			slider.value = slider.value - energyFactor ;
		}
		else if(refuel){
			slider.value = slider.value + 0.007f;
		}else {
			slider.value = slider.value - 0.00015f;
		}

		if (slider.value <= 0) {
			if (circulo) {
				transform.position = new Vector3 (-280f, -55.6f, 727.6f);
				this.gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
				slider.value = 100;
				transform.rotation = Quaternion.Euler (359.69f, 179.9999f, 0);
			} else if (triangulo) {
				
				transform.position = new Vector3 (-570.7f, -60f, -25.4f);
				transform.rotation = Quaternion.Euler (359.69f, 181.4886f, 0);
				slider.value = 100;
				this.gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			
	//			transform.position = new Vector3 (-770.4f, -60f, 177.1f);
	//			transform.rotation = Quaternion.Euler(359.69f, 181.4886f, 0);
	//			this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

			} else 
				Application.LoadLevel ("Car");
		}
	}

    public void overload()
    {
        slider.value = 100;
    }
}
