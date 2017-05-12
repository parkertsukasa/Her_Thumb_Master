using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerScript : MonoBehaviour {

	private GameObject player1;
	private GameObject player2;

	private MoveManagerScript MMS;

	private StateManagerScript SMS_1P;
	private StateManagerScript SMS_2P;

	private ActionManagerScript AMS;

	private float hrzn_1p;//ps4用
	private float hrzn_2p;//ps4用

	private float jchrzn_1p;//Switch用
	private float jchrzn_2p;//Switch用

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




		//------------左スティック入力による移動----------------------------

		//------------ps4コントローラー----------------------------
		if (hrzn_1p >= 0.5f || hrzn_1p <= -0.5f) {
			MMS.Player_Move (player1, hrzn_1p);
		} else {
			if (SMS_1P.runstate == StateManagerScript.move_state.walk || SMS_1P.runstate == StateManagerScript.move_state.idle_dush) {
				SMS_1P.runstate = StateManagerScript.move_state.idle_dush;
			} else {
				SMS_1P.runstate = StateManagerScript.move_state.idle;
			}
		}
		if (hrzn_2p >= 0.5 || hrzn_2p <= -0.5f) {
			MMS.Player_Move (player2, hrzn_2p);
		} else {
			if (SMS_2P.runstate == StateManagerScript.move_state.walk) {
				SMS_2P.runstate = StateManagerScript.move_state.idle_dush;
			} else {
				SMS_2P.runstate = StateManagerScript.move_state.idle;
			}
		}




		//------------Switchコントローラー-------------------------
		if (jchrzn_1p != 0) {
			MMS.Player_Move (player1, jchrzn_1p);
		}
		if (jchrzn_2p != 0) {
			MMS.Player_Move (player2, jchrzn_2p);
		}


		//-----------攻撃等のアクション------------------------------------
		if (Input.GetButtonDown ("1P_Circle")) {
			SMS_1P.Snake ();
			AMS.Snake (player2, player1);
		}
		if (Input.GetButtonDown ("2P_Circle")) {
			SMS_2P.Snake ();
			AMS.Snake (player1, player2);
		}


		if (Input.GetButtonDown ("1P_Square")) {
			SMS_1P.Guard ();
			AMS.Guard (player2);
		}
		if (Input.GetButtonDown ("2P_Square")) {
			SMS_2P.Guard ();
			AMS.Guard (player1);
		}

		if (Input.GetButtonDown ("1P_Cross")) {
			SMS_1P.Attack ();
			AMS.Attack (player2, player1);
		}
		if (Input.GetButtonDown ("2P_Cross")) {
			SMS_2P.Attack ();
			AMS.Attack (player1, player2);
		}

		if (Input.GetButtonDown ("1P_Triangle")) {
			SMS_1P.Check ();
			AMS.Check (player2);
		}
		if (Input.GetButtonDown ("2P_Triangle")) {
			SMS_2P.Check ();
			AMS.Check (player1);
		}
	}
}
