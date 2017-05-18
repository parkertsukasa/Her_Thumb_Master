using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManagerScript : MonoBehaviour {

	private GameObject player1;
	private GameObject player2;

	private float distance;//1Pと2Pの距離を入れる変数

	private const float action_range = 2.0f;//攻撃等が行える距離の上限、定数

	// Use this for initialization
	void Start () {
		player1 = GameObject.Find ("1P_Manager");
		player2 = GameObject.Find ("2P_Manager");

	}
	
	// Update is called once per frame
	void Update () {
		distance = Mathf.Abs( player2.transform.position.x - player1.transform.position.x);
		Debug.Log ("Distance =" + distance);
	}

	//InputManagerScriptから□ボタン入力時に第1引数に[相手]を第2引数に[自分]を引数にして呼び出す
	public void Guard(GameObject opponent,GameObject myself) {
		StateManagerScript SMS = myself.GetComponent<StateManagerScript>();
		if (SMS.nowstate == StateManagerScript.state.idle) {
			SMS.Guard ();
			Transform hand = myself.transform.FindChild ("Hand_Model");
			Animator anim = hand.gameObject.GetComponent<Animator> ();
			anim.SetBool ("Guard", true);
		}
	}

	//InputManagerScriptから□ボタン入力時に第1引数に[相手]を第2引数に[自分]を引数にして呼び出す
	public void Exit_from_Guard(GameObject opponent,GameObject myself) {
		Transform hand = myself.transform.FindChild("Hand_Model");
		Animator anim = hand.gameObject.GetComponent<Animator> ();
		anim.SetBool ("Guard", false);
	}

	//InputManagerScriptから△ボタン入力時に第1引数に[相手]を第2引数に[自分]を引数にして呼び出す
	public void Check(GameObject opponent,GameObject myself) {
		StateManagerScript SMS = myself.GetComponent<StateManagerScript>();
		if (SMS.nowstate == StateManagerScript.state.idle) {
			SMS.Check ();
			Transform hand = myself.transform.FindChild ("Hand_Model");
			Animator anim = hand.gameObject.GetComponent<Animator> ();
			anim.SetBool ("Check", true);
		}
	}

	//InputManagerScriptから□ボタン入力時に第1引数に[相手]を第2引数に[自分]を引数にして呼び出す
	public void Exit_from_Check(GameObject opponent,GameObject myself) {
		Transform hand = myself.transform.FindChild("Hand_Model");
		Animator anim = hand.gameObject.GetComponent<Animator> ();
		anim.SetBool ("Check", false);
	}

	//InputManagerScriptから×ボタン入力時に第1引数に[相手]を第2引数に[自分]を渡して呼び出す
	public void Attack(GameObject opponent, GameObject myself) {
		StateManagerScript SMS = opponent.GetComponent<StateManagerScript> ();

		if (SMS.nowstate == StateManagerScript.state.idle) {//idle状態のみ有効（連打の防止）
			//---------- Animatorの管理
			Transform hand = myself.transform.FindChild("Hand_Model");
			Animator anim = hand.gameObject.GetComponent<Animator> ();
			anim.SetBool ("Attack", true);

			//---------- 相手に応じた処理
			if (distance <= action_range) {//1Pと2Pの距離が規定の距離より近かったら
				StateManagerScript mySMS = myself.GetComponent<StateManagerScript> ();
				PlayerHPScript PHPS = opponent.GetComponent<PlayerHPScript> ();
				switch (SMS.nowstate) {
				case StateManagerScript.state.idle://相手が「idle」だっったら
				//相手を「held_s」状態に
				//自分を「hold_s」状態に
					PHPS.Damage_S ();//相手にダメージ（小）を与える
					break;
				case StateManagerScript.state.guard://相手が「guard」だっったら
					mySMS.Bind_S ();//自分を「bind_s」状態に
					break;
				case StateManagerScript.state.check://相手が「check」だっったら
				//相手を「held_m」状態に
				//自分を「hold_m」状態に
					PHPS.Damage_M ();//相手にダメージ（中）を与える
					break;
				case StateManagerScript.state.bind_l://相手が「bind_l」だっったら
				//相手を「held_m」状態に
				//自分を「hold_m」状態に
					PHPS.Damage_M ();//相手にダメージ（中）を与える
					break;
				case StateManagerScript.state.bind_s://相手が「bind_s」だっったら
				//相手を「held_m」状態に
				//自分を「hold_m」状態に
					PHPS.Damage_M ();//相手にダメージ（中）を与える
					break;
				}
			}
		}
	}

	//InputManagerScriptから○ボタン入力時に第1引数に[相手]を第2引数に[自分]を渡して呼び出す
	public void Snake(GameObject opponent, GameObject myself) {
		if (distance <= action_range) {//1Pと2Pの距離が規定の距離より近かったら
			StateManagerScript SMS = opponent.GetComponent<StateManagerScript> ();
			StateManagerScript mySMS = myself.GetComponent<StateManagerScript> ();
			PlayerHPScript PHPS = opponent.GetComponent<PlayerHPScript> ();
			switch (SMS.nowstate) {
			case StateManagerScript.state.idle://相手が「idle」だっったら
				mySMS.Bind_S();//自分を「bind_s」状態に
				Debug.Log("OK");
				break;
			case StateManagerScript.state.guard://相手が「guard」だっったら
				mySMS.Bind_L();//自分を「bind_s」状態に
				break;
			case StateManagerScript.state.check://相手が「check」だっったら
				//相手を「held_l」状態に
				//自分を「hold_l」状態に
				PHPS.Damage_L();//相手にダメージ（大）を与える
				break;
			case StateManagerScript.state.bind_l://相手が「bind_l」だっったら
				//相手を「held_l」状態に
				//自分を「hold_l」状態に
				PHPS.Damage_L();//相手にダメージ（大）を与える
				break;
			case StateManagerScript.state.bind_s://相手が「bind_s」だっったら
				//相手を「held_l」状態に
				//自分を「hold_l」状態に
				PHPS.Damage_L();//相手にダメージ（大）を与える
				break;
			}
		}
	}




}
