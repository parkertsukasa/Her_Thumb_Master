using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//----------- StateManagerScriptを継承したクラス
public class HoldManagerScript : StateManagerScript {

	public GameObject count;
	private RawImage countsorce;

	public Texture[] counttexture;

	public GameObject win;
	private Text wintext;

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

	public float holdeddistance;

	//private PlayerHPScript PHPS_1P;
	//private PlayerHPScript PHPS_2P;

	private float holdtimer = 10.0f;





	// Use this for initialization
	void Start () {

		player1 = GameObject.Find ("1P_Manager");
		player2 = GameObject.Find ("2P_Manager");

		SMS_1P = player1.GetComponent<StateManagerScript> ();
		SMS_2P = player2.GetComponent<StateManagerScript> ();

		//PHPS_1P = player1.GetComponent<PlayerHPScript> ();
		//PHPS_2P = player2.GetComponent<PlayerHPScript> ();

		countsorce = count.GetComponent<RawImage> ();
		wintext = win.GetComponent<Text> ();

	}
	
	// Update is called once per frame
	void Update () {
		Holded_by_1P ();
		Holded_by_2P ();
	}

	//---------- 1PがHold状態に入った時の処理
	private void Holded_by_1P(){

		
		if (SMS_1P.nowstate == state.hold_l ||
			SMS_1P.nowstate == state.hold_m ||
			SMS_1P.nowstate == state.hold_s) {

			player2.transform.position = player1.transform.position + new Vector3 (holdeddistance,0,0);

			count.SetActive (true);
			int timerint = (int)holdtimer;
			countsorce.texture = counttexture[timerint];

			Resist_2P ();// ぐるぐる入力を取る関数

			holdtimer -= Time.deltaTime;

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

				//----- 2P用Animator取得
				Transform hand2 = player2.transform.FindChild ("Hand_Model");
				Animator anim2 = hand2.gameObject.GetComponent<Animator> ();


				anim2.SetInteger ("Held_State", 0);

				//----- 1P用Animator取得
				Transform hand1 = player1.transform.FindChild ("Hand_Model");
				Animator anim1 = hand1.gameObject.GetComponent<Animator> ();


				anim1.SetBool ("Attack", false);
				anim1.SetBool ("Hold", false);


				player1.transform.position = Vector3.Lerp(player1.transform.position,new Vector3(-5,0,0),1.0f);//仕切り直し
				player2.transform.position = Vector3.Lerp(player2.transform.position,new Vector3(5,0,0),1.0f);//仕切り直し

				SMS_1P.nowstate = state.idle;
				SMS_2P.nowstate = state.idle;

				escape = 0;

				holdtimer = 10.0f;
				count.SetActive (false);
			}

			//攻撃ボタンを連打でぐるぐる入力を妨害
			if(Input.GetButtonDown("1P_Circle") || Input.GetButtonDown("1P_Cross")){
				escape -= 1;
			}

			//10秒経過で勝利
			if (holdtimer <= 0.0f) {
				holdtimer = 0;
				win.SetActive (true);
				wintext.text = "1P WIN!";
				if (Input.anyKeyDown) {
					SceneManager.LoadScene ("Title");
				}
				//1Pの勝利

			}
		}

	}

	//---------- 2PがHold状態に入った時の処理
	private void Holded_by_2P(){
		

		if (SMS_2P.nowstate == state.hold_l ||
			SMS_2P.nowstate == state.hold_m ||
			SMS_2P.nowstate == state.hold_s) {

			player1.transform.position = player2.transform.position - new Vector3 (holdeddistance,0,0);

			Resist_1P ();// ぐるぐる入力を取る関数

			count.SetActive (true);
			int timerint = (int)holdtimer;
			countsorce.texture = counttexture[timerint];

			holdtimer -= Time.deltaTime;

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
			//ぐるぐるの入力で解除
			if (escape >= release) {

				//----- 1P用Animator取得
				Transform hand1 = player1.transform.FindChild ("Hand_Model");
				Animator anim1 = hand1.gameObject.GetComponent<Animator> ();

				anim1.SetInteger ("Held_State", 0);

				//----- 2P用Animator取得
				Transform hand2 = player2.transform.FindChild ("Hand_Model");
				Animator anim2 = hand2.gameObject.GetComponent<Animator> ();

				anim2.SetBool ("Attack", false);
				anim2.SetBool ("Hold", false);

				player1.transform.position = Vector3.Lerp(player1.transform.position,new Vector3(-5,0,0),3.0f);//仕切り直し
				player2.transform.position = Vector3.Lerp(player2.transform.position,new Vector3(5,0,0),3.0f);//仕切り直し

				SMS_1P.nowstate = state.idle;
				SMS_2P.nowstate = state.idle;

				escape = 0;

				holdtimer = 10.0f;
				count.SetActive (false);
			}

			//攻撃ボタンを押したらぐるぐる入力から1減らす
			if(Input.GetButtonDown("2P_Circle") || Input.GetButtonDown("2P_Cross")){
				escape -= 1;
			}

			//10秒経過で勝利
			if (holdtimer <= 0.0f) {
				holdtimer = 0;
				win.SetActive (true);
				wintext.text = "2P WIN!";
				if (Input.anyKeyDown) {
					SceneManager.LoadScene ("Title");
				}
				//2Pの勝利

			}
		}
	}



	//----------- ぐるぐる入力のカウント(1P用)
	public void Resist_1P(){
		hrzn = Input.GetAxis ("1P_LeftHorizontal");
		vrti = Input.GetAxis ("1P_LeftVertical");

		Transform hand = player1.transform.FindChild ("Hand_Model");
		Animator anim = hand.gameObject.GetComponent<Animator> ();

		//-----「↑』入力時直前に「↑」が押されていなければカウント
		if (hrzn >= 0.8f) {
			if(state_1p != input_state.up){
			state_1p = input_state.up;
			escape += 1;
				anim.SetInteger ("Held_State", 2);
			}
		}

		//-----「↓』入力時直前に「↓」が押されていなければカウント
		if (hrzn <= -0.8f) {
			if(state_1p != input_state.down){
			state_1p = input_state.down;
			escape += 1;
				anim.SetInteger ("Held_State", 4);
			}
		}

		//-----「→』入力時直前に「→」が押されていなければカウント
		if (vrti >= 0.8f) {
			if(state_1p != input_state.right){
			state_1p = input_state.right;
			escape += 1;
				anim.SetInteger ("Held_State", 3);
			}
		}

		//-----「←』入力時直前に「←」が押されていなければカウント
		if (vrti <= -0.8f) {
			if (state_1p != input_state.left) {
				state_1p = input_state.left;
				escape += 1;
				anim.SetInteger ("Held_State", 5);
			}
		}

		//----- 何も入力されていない
		if (hrzn == 0 && vrti == 0) {
			anim.SetInteger ("Held_State", 1);
		}

		Debug.Log ("ぐるぐる" + escape);
	}

	//----------- ぐるぐる入力のカウント（2P用）
	public void Resist_2P(){
		hrzn = Input.GetAxis ("2P_LeftHorizontal");
		vrti = Input.GetAxis ("2P_LeftVertical");

		Transform hand = player2.transform.FindChild ("Hand_Model");
		Animator anim = hand.gameObject.GetComponent<Animator> ();

		//-----「↑』入力時直前に「↑」が押されていなければカウント
		if (hrzn >= 0.8f) {
			if(state_2p != input_state.up){
				state_2p = input_state.up;
				escape += 1;
				anim.SetInteger ("Held_State", 2);
			}
		}

		//-----「↓』入力時直前に「↓」が押されていなければカウント
		if (hrzn <= -0.8f) {
			if(state_2p != input_state.down){
				state_2p = input_state.down;
				escape += 1;
				anim.SetInteger ("Held_State", 4);
			}
		}

		//-----「→』入力時直前に「→」が押されていなければカウント
		if (vrti >= 0.8f) {
			if(state_2p != input_state.right){
				state_2p = input_state.right;
				escape += 1;
				anim.SetInteger ("Held_State", 3);
			}
		}

		//-----「←』入力時直前に「←」が押されていなければカウント
		if (vrti <= -0.8f) {
			if (state_2p != input_state.left) {
				state_2p = input_state.left;
				escape += 1;
				anim.SetInteger ("Held_State", 5);
			}
		}

		//----- 何も入力されていない
		if (hrzn == 0 && vrti == 0) {
			anim.SetInteger ("Held_State", 1);
		}

		Debug.Log ("ぐるぐる" + escape);
	}
}
