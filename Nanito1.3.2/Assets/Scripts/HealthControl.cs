using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class HealthControl : MonoBehaviour {

	public GameObject hearts;
	public Sprite hearts3_25; //9
	public Sprite hearts3_50; //10
	public Sprite hearts3_75; //11
	public Sprite hearts3_100; // full health 12
	public Sprite hearts2_25; //5
	public Sprite hearts2_50; //6
	public Sprite hearts2_75; //7
	public Sprite hearts2_100; //8
	public Sprite hearts1_25; //1 minimum life
	public Sprite hearts1_50; //2
	public Sprite hearts1_75; //3
	public Sprite hearts1_100; //4


	// Update is called once per frame
	void Update () {

		switch (HealthScript.hp) {
			
		case 12:
			hearts.GetComponent<Image>().color = new Color(1f,1f,1f,1f);
			hearts.GetComponent<Image>().sprite = hearts3_100;
			break;
			
		case 11:
			hearts.GetComponent<Image>().sprite = hearts3_75;	
			break;
			
		case 10:
			hearts.GetComponent<Image>().sprite = hearts3_50;
			break;

		case 9:
			hearts.GetComponent<Image>().sprite = hearts3_25;
			break;
			
		case 8:
			hearts.GetComponent<Image>().sprite = hearts2_100;	
			break;
			
		case 7:
			hearts.GetComponent<Image>().sprite = hearts2_75;
			break;
		
		case 6:
			hearts.GetComponent<Image>().sprite = hearts2_50;
			break;
			
		case 5:
			hearts.GetComponent<Image>().sprite = hearts2_25;	
			break;
			
		case 4:
			hearts.GetComponent<Image>().sprite = hearts1_100;
			break;
		
		case 3:
			hearts.GetComponent<Image>().sprite = hearts1_75;
			break;
			
		case 2:
			hearts.GetComponent<Image>().sprite = hearts1_50;	
			break;
			
		case 1:
			hearts.GetComponent<Image>().sprite = hearts1_25;
			break;

		case 0:
			hearts.GetComponent<Image>().color = new Color(0f,0f,0f,0f);
			hearts.GetComponent<Image>().sprite = null;

			break;
		}
	}
}
