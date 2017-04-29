using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManagerScript : MonoBehaviour {

	GameObject player1;
	GameObject player2;

	float distance;//1Pと2Pの距離を入れる変数

	const float action_range = 2.0f;//攻撃等が行える距離の上限、定数

	// Use this for initialization
	void Start () {
		player1 = GameObject.Find ("1P_Manager");
		player2 = GameObject.Find ("2P_Manager");

	}
	
	// Update is called once per frame
	void Update () {
		distance = Mathf.Abs(player2.transform.position.x - player1.transform.position.x);
		Debug.Log ("Distance =" + distance);
	}

	//InputManagerScriptから□ボタン入力時に[相手]を引数にして呼び出す
	public void Guard(GameObject opponent) {

		if (distance <= action_range) {//1Pと2Pの距離が規定の距離より近かったら
			StateManagerScript SMS = opponent.GetComponent<StateManagerScript> ();
			switch (SMS.nowstate) {
			case StateManagerScript.state.attack://相手が「attack」だっったら
				SMS.Bind_S ();//相手を「bind_s」状態に
				break;
			case StateManagerScript.state.snake://相手が「snake」だっったら
				SMS.Bind_L ();//相手を「bind_l」状態に
				break;
			}
		}
	}

	//InputManagerScriptから△ボタン入力時に[相手]を引数にして呼び出す
	public void Check(GameObject opponent) {
		//特に何もなし
	}

	//InputManagerScriptから×ボタン入力時に第1引数に[相手]を第2引数に[自分]を渡して呼び出す
	public void Attack(GameObject opponent, GameObject myself) {
		if (distance <= action_range) {//1Pと2Pの距離が規定の距離より近かったら
			StateManagerScript SMS = opponent.GetComponent<StateManagerScript> ();
			StateManagerScript mySMS = myself.GetComponent<StateManagerScript> ();
			PlayerHPScript PHPS = opponent.GetComponent<PlayerHPScript> ();
			switch (SMS.nowstate) {
			case StateManagerScript.state.idle://相手が「idle」だっったら
				//相手を「held_s」状態に
				//自分を「hold_s」状態に
				PHPS.Damage_S();//相手にダメージ（小）を与える
				break;
			case StateManagerScript.state.guard://相手が「guard」だっったら
				mySMS.Bind_S();//自分を「bind_s」状態に
				break;
			case StateManagerScript.state.check://相手が「check」だっったら
				//相手を「held_m」状態に
				//自分を「hold_m」状態に
				PHPS.Damage_M();//相手にダメージ（中）を与える
				break;
			case StateManagerScript.state.bind_l://相手が「bind_l」だっったら
				//相手を「held_m」状態に
				//自分を「hold_m」状態に
				PHPS.Damage_M();//相手にダメージ（中）を与える
				break;
			case StateManagerScript.state.bind_s://相手が「bind_s」だっったら
				//相手を「held_m」状態に
				//自分を「hold_m」状態に
				PHPS.Damage_M();//相手にダメージ（中）を与える
				break;
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
