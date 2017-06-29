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

	private float r2_1p;
	private bool r2pushed_1p = false;
	private bool r2_1p_canpush = true;

	private float r2_2p;
	private bool r2pushed_2p = false;
	private bool r2_2p_canpush = true;

	private float l2_1p;
	private bool l2pushed_1p = false;
	private bool l2_1p_canpush = true;

	private float l2_2p;
	private bool l2pushed_2p = false;
	private bool l2_2p_canpush = true;

	private bool startbattle; 

	// Use this for initialization
	void Start () {
		player1 = GameObject.Find ("1P_Manager");
		player2 = GameObject.Find ("2P_Manager");


		MMS = GetComponent<MoveManagerScript> ();
		SMS_1P = player1.GetComponent<StateManagerScript> ();
		SMS_2P = player2.GetComponent<StateManagerScript> ();
		AMS = GetComponent<ActionManagerScript> ();

		startbattle = false;
		Invoke ("BattleStart", 2.0f);
	}

	private void BattleStart(){
		startbattle = true;
	}


	
	// Update is called once per frame
	void Update () {
		if (startbattle == true) {
			InputField ();
		}
	}

	void InputField(){

		hrzn_1p = Input.GetAxis ("1P_LeftHorizontal");
		hrzn_2p = Input.GetAxis ("2P_LeftHorizontal");


		jchrzn_1p = Input.GetAxis ("JoyCon1_Horizontal");
		jchrzn_2p = Input.GetAxis ("JoyCon2_Horizontal");

		r2_1p = Input.GetAxis ("1P_R2");
		r2_2p = Input.GetAxis ("2P_R2");

		l2_1p = Input.GetAxis ("1P_L2");
		l2_2p = Input.GetAxis ("2P_L2");


		//---------- 1PのR2が押された時 ----------
		if (r2_1p > 0.5f) {
			if (r2_1p_canpush == true) {
				r2pushed_1p = true;
				r2_1p_canpush = false;
			}
		}
		if (r2_1p < 0.0f) {
			r2_1p_canpush = true;
		}

		if (r2pushed_1p == true || Input.GetKeyDown(KeyCode.R)) {
			AMS.Snake (player2, player1);//Snake
			r2pushed_1p = false;
		}

		//---------- 2PのR2が押された時 ----------
		if (r2_2p > 0.5f) {
			if (r2_2p_canpush == true) {
				r2pushed_2p = true;
				r2_2p_canpush = false;
			}
		}
		if (r2_2p < 0.0f) {
			r2_2p_canpush = true;
		}

		if (r2pushed_2p == true) {
			AMS.Snake (player1, player2);//Snake
			r2pushed_2p = false;
		}


		//---------- 1PのL2が押された時 ----------
		if (l2_1p > 0.5f) {
			if (l2_1p_canpush == true) {
				l2pushed_1p = true;
				l2_1p_canpush = false;
			}
		}
		if (l2_1p < 0.0f) {
			l2_1p_canpush = true;
		}

		if (l2pushed_1p == true || Input.GetKeyDown(KeyCode.L)) {

			HissatsuWazaScript HWS = player1.GetComponent<HissatsuWazaScript> ();
			HWS.Runch ();

			l2pushed_1p = false;
		}

		//----------- デバッグ用 ----------
		if (Input.GetKeyDown (KeyCode.H)) {
			HissatsuWazaScript HWS = player1.GetComponent<HissatsuWazaScript> ();
			HWS.Runch ();
		}

		//---------- 2PのL2が押された時 ----------
		if (l2_2p > 0.5f) {
			if (l2_2p_canpush == true) {
				l2pushed_2p = true;
				l2_2p_canpush = false;
			}
		}
		if (l2_2p < 0.0f) {
			l2_2p_canpush = true;
		}

		if (l2pushed_2p == true) {

			HissatsuWazaScript HWS = player2.GetComponent<HissatsuWazaScript> ();
			HWS.Runch ();

			l2pushed_2p = false;
		}






		//------------左スティック入力による移動----------------------------

		//------------ps4コントローラー----------------------------
		if (hrzn_1p >= 0.5f || hrzn_1p <= -0.5f) {
			MMS.Player_Move (player1, hrzn_1p,1);
		} else {
			MMS.Player_Stop (player1);//速度0に
			if (SMS_1P.runstate == StateManagerScript.move_state.walk || SMS_1P.runstate == StateManagerScript.move_state.idle_dush) {
				SMS_1P.runstate = StateManagerScript.move_state.idle_dush;
			} else {
				SMS_1P.runstate = StateManagerScript.move_state.idle;
			}
		}
		if (hrzn_2p >= 0.5 || hrzn_2p <= -0.5f) {
			MMS.Player_Move (player2, hrzn_2p,2);
		} else {
			MMS.Player_Stop (player2);//速度0に
			if (SMS_2P.runstate == StateManagerScript.move_state.walk) {
				SMS_2P.runstate = StateManagerScript.move_state.idle_dush;
			} else {
				SMS_2P.runstate = StateManagerScript.move_state.idle;
			}
		}




		//------------Switchコントローラー-------------------------
		if (jchrzn_1p != 0) {
			MMS.Player_Move (player1, jchrzn_1p,1);
		}
		if (jchrzn_2p != 0) {
			MMS.Player_Move (player2, jchrzn_2p,1);
		}


		//-----------攻撃等のアクション------------------------------------
		//----- ○ボタン
		if (Input.GetButtonDown ("1P_Circle") || Input.GetKeyDown(KeyCode.D)) {
			AMS.Pistol (player2, player1,1);
		}
		if (Input.GetButtonDown ("2P_Circle") || Input.GetKeyDown(KeyCode.H)) {
			AMS.Pistol (player1, player2,2);
		}


		//----- □ボタン
		if (Input.GetButtonDown ("1P_Square") || Input.GetKeyDown(KeyCode.A)) {
			AMS.Guard (player2, player1);
		}
		if (Input.GetButtonDown ("2P_Square")|| Input.GetKeyDown(KeyCode.F)) {
			AMS.Guard (player1, player2);
		}
		if (Input.GetButtonUp ("1P_Square")|| Input.GetKeyUp(KeyCode.A)) {
			SMS_1P.Idle ();
			AMS.Exit_from_Guard (player2, player1);
		}
		if (Input.GetButtonUp ("2P_Square")|| Input.GetKeyUp(KeyCode.F)) {
			SMS_2P.Idle ();
			AMS.Exit_from_Guard (player1, player2);
		}


		//----- ×ボタン
		if (Input.GetButtonDown ("1P_Cross") || Input.GetKeyDown(KeyCode.S)) {
			//SMS_1P.Attack ();
			AMS.Attack (player2, player1);
		}
		if (Input.GetButtonDown ("2P_Cross")|| Input.GetKeyDown(KeyCode.G)) {
			//SMS_2P.Attack ();
			AMS.Attack (player1, player2);
		}


		//----- △ボタン
		if (Input.GetButtonDown ("1P_Triangle") || Input.GetKeyDown(KeyCode.W) ){
			AMS.Check (player2, player1);
		}
		if (Input.GetButtonDown ("2P_Triangle")|| Input.GetKeyDown(KeyCode.T)) {
			AMS.Check (player1, player2);
		}
		if (Input.GetButtonUp ("1P_Triangle") || Input.GetKeyUp(KeyCode.W) ) {
			if(SMS_1P.nowstate == StateManagerScript.state.check){
			SMS_1P.Idle ();
		}
			AMS.Exit_from_Check (player2, player1);
		}
		if (Input.GetButtonUp ("2P_Triangle")|| Input.GetKeyUp(KeyCode.T)) {
			if (SMS_2P.nowstate == StateManagerScript.state.check) {
				SMS_2P.Idle ();
			}
			AMS.Exit_from_Check (player1, player2);
		}
	}
}
