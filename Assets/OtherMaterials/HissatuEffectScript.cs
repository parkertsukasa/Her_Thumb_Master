using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HissatuEffectScript : MonoBehaviour {
	bool hassyaflag;
	public GameObject charge;
	public GameObject razer;
	public float ChargeTime;
	public float RazerTime;
	// Use this for initialization
	void Start () {
		hassyaflag = false;
		charge.SetActive (true);
		razer.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		Invoke ("hassya", ChargeTime);
		Invoke ("end", ChargeTime + RazerTime);


		if (hassyaflag == true) {
			charge.SetActive (false);
			razer.SetActive (true);
			
		}
		
	}

	void hassya(){
		hassyaflag = true;
	}

	void end(){
		this.gameObject.SetActive (false);
	}
}
