using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HissatsuWazaScript : MonoBehaviour {

	public Text text;

	public const float up = 10.0f;

	private float pp = 0;

	private StateManagerScript mySMS;
	private Transform myhand;
	private Animator myanim;

	public GameObject opponent;
	private StateManagerScript oppSMS;

	private bool canrunch = false;

	public GameObject effect;

	// Use this for initialization
	void Start () {
		mySMS = GetComponent<StateManagerScript> ();
		myhand = transform.FindChild ("Hand_Model");
		myanim = myhand.GetComponent<Animator> ();

		oppSMS = opponent.GetComponent<StateManagerScript> ();
		 

	}
	
	// Update is called once per frame
	void Update () {
		text.text = pp.ToString ("f0");


		if (pp >= 100) {
			canrunch = true;
		} else {
			canrunch = false;
		}


		if (mySMS.nowstate == StateManagerScript.state.check) {

			pp += up * Time.deltaTime;//1秒あたり「up」ずつppが増えていく

			if (oppSMS.nowstate == StateManagerScript.state.guard) {

				pp += up * 0.5f * Time.deltaTime;//相手が「guard」なら1.5倍で増える

			}
		}

	}

	public void Runch(){

		if (canrunch == true) {

			mySMS.Hissatsu ();
			myanim.SetBool ("Hissatsu", true);

			pp = 0;
			canrunch = false;

			Instantiate (effect, transform.position + new Vector3(0,0.3f,0), new Quaternion(0,1,0,90));

		}

	}
}
