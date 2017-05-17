using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----------- StateManagerScriptを継承したクラス
public class HoldManagerScript : StateManagerScript {

	public int escape = 0;

	private float hrzn;
	private float vrti;

	private enum input_state{neutral,up,down,left,right};//長押しによる入力を防ぐためのstate

	private input_state state_1p = input_state.neutral;
	private input_state state_2p = input_state.neutral;

	private GameObject player1;
	private GameObject player2;

	private StateManagerScript SMS_1P;
	private StateManagerScript SMS_2P;

	private PlayerHPScript PHPS_1P;
	private PlayerHPScript PHPS_2P;


	// Use this for initialization
	void Start () {

		player1 = GameObject.Find ("1P_Manager");
		player2 = GameObject.Find ("2P_Manager");

		SMS_1P = player1.GetComponent<StateManagerScript> ();
		SMS_2P = player2.GetComponent<StateManagerScript> ();

		PHPS_1P = player1.GetComponent<PlayerHPScript> ();
		PHPS_2P = player2.GetComponent<PlayerHPScript> ();

	}
	
	// Update is called once per frame
	void Update () {
		Holded_by_1P ();

	}

	//---------- 1PがHold状態に入った時の処理
	private void Holded_by_1P(){
		
		if (SMS_1P.nowstate == state.hold_l ||
			SMS_1P.nowstate == state.hold_m ||
			SMS_1P.nowstate == state.hold_s) {
			float timer = 0.0f;
			timer += Time.deltaTime;

			int release = 30;//ぐるぐる入力による解除の閾値

			//----- holdの種類によって解除難度の変更
			switch (SMS_1P.nowstate) {
			case state.hold_l:
				release = 50;
				break;
			case state.hold_m:
				release = 40;
				break;
			case state.hold_s:
				release = 30;
				break;
			}

			//ぐるぐるの入力で解除
			if (escape >= release) {
				SMS_1P.nowstate = state.idle;
				SMS_2P.nowstate = state.idle;
			}
			//攻撃ボタンを離したら解除
			if(Input.GetButtonUp("1P_Circle") || Input.GetButtonUp("1P_Cross")){
				SMS_1P.nowstate = state.idle;
				SMS_2P.nowstate = state.idle;
			}
			//10秒経過で勝利
			if (timer >= 10.0f) {
				//1Pの勝利

			}
		}

	}

	//---------- 2PがHold状態に入った時の処理
	private void Holded_by_2P(){

		if (SMS_2P.nowstate == state.hold_l ||
			SMS_2P.nowstate == state.hold_m ||
			SMS_2P.nowstate == state.hold_s) {
			float timer = 0.0f;
			timer += Time.deltaTime;

			int release = 30;//ぐるぐる入力による解除の閾値

			//----- holdの種類によって解除難度の変更
			switch (SMS_2P.nowstate) {
			case state.hold_l:
				release = 50;
				break;
			case state.hold_m:
				release = 40;
				break;
			case state.hold_s:
				release = 30;
				break;
			}
			//ぐるぐるの入力5で解除
			if (escape >= release) {
				SMS_1P.nowstate = state.idle;
				SMS_2P.nowstate = state.idle;
			}
			//攻撃ボタンを離したら解除
			if(Input.GetButtonUp("2P_Circle") || Input.GetButtonUp("2P_Cross")){
				SMS_1P.nowstate = state.idle;
				SMS_2P.nowstate = state.idle;
			}
			//10秒経過で勝利
			if (timer >= 10.0f) {
				//1Pの勝利

			}
		}

	}

	//----------- ぐるぐる入力のカウント
	public void Resist_1P(){
		hrzn = Input.GetAxis ("1P_LeftHorizontal");
		vrti = Input.GetAxis ("1P_LeftVertical");

		//-----「↑』入力時直前に「↑」が押されていなければカウント
		if (hrzn >= 0.8f) {
			if(state_1p != input_state.up){
			state_1p = input_state.up;
			escape += 1;
			}
		}

		//-----「↓』入力時直前に「↓」が押されていなければカウント
		if (hrzn <= -0.8f) {
			if(state_1p != input_state.down){
			state_1p = input_state.down;
			escape += 1;
			}
		}

		//-----「→』入力時直前に「→」が押されていなければカウント
		if (vrti >= 0.8f) {
			if(state_1p != input_state.right){
			state_1p = input_state.right;
			escape += 1;
			}
		}

		//-----「←』入力時直前に「←」が押されていなければカウント
		if (vrti <= -0.8f) {
			if (state_1p != input_state.left) {
				state_1p = input_state.left;
				escape += 1;
			}
		}

		Debug.Log ("ぐるぐる" + escape);
	}

	public void Resist_2P(){
		hrzn = Input.GetAxis ("2P_LeftHorizontal");
		vrti = Input.GetAxis ("2P_LeftVertical");

		//-----「↑』入力時直前に「↑」が押されていなければカウント
		if (hrzn >= 0.8f) {
			if(state_2p != input_state.up){
				state_2p = input_state.up;
				escape += 1;
			}
		}

		//-----「↓』入力時直前に「↓」が押されていなければカウント
		if (hrzn <= -0.8f) {
			if(state_2p != input_state.down){
				state_2p = input_state.down;
				escape += 1;
			}
		}

		//-----「→』入力時直前に「→」が押されていなければカウント
		if (vrti >= 0.8f) {
			if(state_2p != input_state.right){
				state_2p = input_state.right;
				escape += 1;
			}
		}

		//-----「←』入力時直前に「←」が押されていなければカウント
		if (vrti <= -0.8f) {
			if (state_2p != input_state.left) {
				state_2p = input_state.left;
				escape += 1;
			}
		}

		Debug.Log ("ぐるぐる" + escape);
	}
}
