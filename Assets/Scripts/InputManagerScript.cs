using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerScript : MonoBehaviour {

	GameObject player1;
	GameObject player2;

	MoveManagerScript MMS;

	StateManagerScript SMS_1P;
	StateManagerScript SMS_2P;

	ActionManagerScript AMS;

	float hrzn_1p;
	float hrzn_2p;

	float jchrzn_1p;
	float jchrzn_2p;

	// Use this for initialization
	void Start () {
		player1 = GameObject.Find ("1P_Manager");
		player2 = GameObject.Find ("2P_Manager");

		MMS = GetComponent<MoveManagerScript> ();
		SMS_1P = player1.GetComponent<StateManagerScript> ();
		SMS_2P = player2.GetComponent<StateManagerScript> ();
		AMS = GetComponent<ActionManagerScript> ();
	}
	
	// Update is called once per frame
	void Update () {

		hrzn_1p = Input.GetAxis ("1P_LeftHorizontal");
		hrzn_2p = Input.GetAxis ("2P_LeftHorizontal");


		jchrzn_1p = Input.GetAxis ("JoyCon1_Horizontal");
		jchrzn_2p = Input.GetAxis ("JoyCon2_Horizontal");

		Debug.Log (jchrzn_2p);



		//------------左スティック入力による移動----------------------------
		if (hrzn_1p != 0) {
			Transform t = player1.transform;
			MMS.Player_Move (t, hrzn_1p);
		}
		if (hrzn_2p != 0) {
			Transform t = player2.transform;
			MMS.Player_Move (t, hrzn_2p);
		}	

		if (jchrzn_1p != 0) {
			Transform t = player1.transform;
			MMS.Player_Move (t, jchrzn_1p);
		}
		if (jchrzn_2p != 0) {
			Transform t = player2.transform;
			MMS.Player_Move (t, jchrzn_2p);
		}


		//-----------攻撃等のアクション------------------------------------
		if (Input.GetButton ("1P_Circle")) {
			SMS_1P.Snake ();
			AMS.Snake (player2, player1);
		}
		if (Input.GetButton ("2P_Circle")) {
			SMS_2P.Snake ();
			AMS.Snake (player1, player2);
		}


		if (Input.GetButton ("1P_Square")) {
			SMS_1P.Guard ();
			AMS.Guard (player2);
		}
		if (Input.GetButton ("2P_Square")) {
			SMS_2P.Guard ();
			AMS.Guard (player1);
		}

		if (Input.GetButton ("1P_Cross")) {
			SMS_1P.Attack ();
			AMS.Attack (player2, player1);
		}
		if (Input.GetButton ("2P_Cross")) {
			SMS_2P.Attack ();
			AMS.Attack (player1, player2);
		}

		if (Input.GetButton ("1P_Triangle")) {
			SMS_1P.Check ();
			AMS.Check (player2);
		}
		if (Input.GetButton ("2P_Triangle")) {
			SMS_2P.Check ();
			AMS.Check (player1);
		}
	}
}
