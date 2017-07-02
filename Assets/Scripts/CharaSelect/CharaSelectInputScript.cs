using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaSelectInputScript : MonoBehaviour {

	private bool canselect1p = true;
	private bool canselect2p = true;

	private float input1p = 0;
	private float input2p = 0;

	private GameObject CSM;
	private CharaStatusScript CSS;

	// Use this for initialization
	void Start () {
		CSM = GameObject.Find ("CharaStatusManager");
		CSS = CSM.GetComponent<CharaStatusScript> ();
	}
	
	// Update is called once per frame
	void Update () {

		//----------- 1Pの選択 
		if (Input.GetAxis ("1P_LeftHorizontal") >= 0.5f || Input.GetAxis ("1P_LeftHorizontal") <= -0.5f) {
			input1p = Input.GetAxis ("1P_LeftHorizontal");
		} else {
			input1p = 0.0f;
		}

		if (canselect1p == true) {
			if (input1p > 0.5f) {
				CSS.num1p++;
				if (CSS.num1p == 3) {
					CSS.num1p = 0;
				}
				canselect1p = false;
				Invoke ("Reselect1P", 0.2f);
			} else if (input1p < -0.5f) {
				CSS.num1p--;
				if (CSS.num1p == -1) {
					CSS.num1p = 2;
				}
				canselect1p = false;
				Invoke ("Reselect1P", 0.2f);
			}
		}

		//----------- 2Pの選択
		if (Input.GetAxis ("2P_LeftHorizontal") >= 0.5f || Input.GetAxis ("2P_LeftHorizontal") <= -0.5f) {
			input2p = Input.GetAxis ("2P_LeftHorizontal");
		} else {
			input2p = 0.0f;
		}
		if (canselect2p == true) {
			if (input2p > 0.5f) {
				CSS.num2p++;
				if (CSS.num2p == 3) {
					CSS.num2p = 0;
				}
				canselect2p = false;
				Invoke ("Reselect2P", 0.2f);
			} else if (input2p < -0.5f) {
				CSS.num2p--;
				if (CSS.num2p == -1) {
					CSS.num2p = 2;
				}
				canselect2p = false;
				Invoke ("Reselect2P", 0.2f);
			}
		}	

	}

	void Reselect1P(){
		canselect1p = true;
	}
	void Reselect2P(){
		canselect2p = true;
	}
}
