using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HissatsuWazaScript : MonoBehaviour {

	public Image ber;

	public const float up = 10.0f;

	private float pp = 0;

	private StateManagerScript mySMS;
	private Transform myhand;
	private Animator myanim;

	public GameObject opponent;
	private StateManagerScript oppSMS;
	private PlayerHPScript PHPS;
	private Transform opphand;
	private Animator oppanim;

	private bool canrunch = false;

	public GameObject effect;

	public GameObject charged;


	public GameObject bigchargeeffect;


	// Use this for initialization
	void Start () {
		mySMS = GetComponent<StateManagerScript> ();
		myhand = transform.FindChild ("Hand_Model");
		myanim = myhand.GetComponent<Animator> ();

		oppSMS = opponent.GetComponent<StateManagerScript> ();
		opphand = opponent.transform.FindChild ("Hand_Model");
		oppanim = opphand.GetComponent<Animator> ();
		PHPS = opponent.GetComponent<PlayerHPScript> ();

		//effect = transform.FindChild ("hissatu_v1").gameObject;
		 

	}
	
	// Update is called once per frame
	void Update () {
		ber.fillAmount = pp/100;


		if (pp >= 100) {
			canrunch = true;
			charged.SetActive (true);
			pp = 100;
		} else {
			canrunch = false;
			charged.SetActive (false);
		}


		if (mySMS.nowstate == StateManagerScript.state.check) {

			pp += up * Time.deltaTime;//1秒あたり「up」ずつppが増えていく

			if (oppSMS.nowstate == StateManagerScript.state.guard) {

				pp += up * 0.5f * Time.deltaTime;//相手が「guard」なら1.5倍で増える
				bigchargeeffect.SetActive (true);
			} else {
				bigchargeeffect.SetActive (false);
			}
		} else {
			bigchargeeffect.SetActive (false);
		}

	}

	public void Runch(){

		if (canrunch == true) {
			if (mySMS.nowstate == StateManagerScript.state.idle) {
				mySMS.Hissatsu ();
				myanim.SetBool ("Hissatsu", true);

				PHPS.Damage_Hissatsu ();//仮

				oppSMS.Idle ();
				Invoke ("HitHissatsu", 2.4f);

				pp = 0;
				canrunch = false;

				//Instantiate (effect, transform.position + new Vector3(0,0.3f,0), new Quaternion(0,1,0,90));
				effect.SetActive (true);
			}
		}

	}

	private void HitHissatsu(){
		oppanim.SetBool ("HitHissatsu", true);
		Invoke ("HitReset", 2.4f);
	}

	private void HitReset(){
		oppanim.SetBool("HitHissatsu",false);
	}


}
