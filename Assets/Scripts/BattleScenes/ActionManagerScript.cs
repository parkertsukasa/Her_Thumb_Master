using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManagerScript : MonoBehaviour {

	private GameObject player1;
	private GameObject player2;

	private StateManagerScript SMS1;
	private StateManagerScript SMS2;

	public float distance;//1Pと2Pの距離を入れる変数

	public bool touched = false;

	private const float action_range = 2.3f;//攻撃等が行える距離の上限、定数

	// Use this for initialization
	void Start () {
		player1 = GameObject.Find ("1P_Manager");
		player2 = GameObject.Find ("2P_Manager");
		SMS1 = player1.GetComponent<StateManagerScript> ();
		SMS2 = player2.GetComponent<StateManagerScript> ();

	}
	
	// Update is called once per frame
	void Update () {
		distance = Mathf.Abs( player2.transform.position.x - player1.transform.position.x);
		//Debug.Log ("Distance =" + distance);

		if (SMS1.nowstate == StateManagerScript.state.attack) {
				Attack_Process (player2,player1);
		}
		if (SMS2.nowstate == StateManagerScript.state.attack) {
				Attack_Process (player1,player2);
		}

		Debug.Log (touched);

	}

	public void Touched(){
		touched = true;
	}

	public void Released(){
		touched = false;
	}

	//attackをfalseにする前にbindをtrueにするコルーチン------------------------------------------------------------------

	private IEnumerator BindTrue_before_AttackFalse(GameObject myself){

		//---------- Animatorの管理
		Transform myhand = myself.transform.FindChild("Hand_Model");
		Animator myanim = myhand.gameObject.GetComponent<Animator> ();

		yield return new WaitForSeconds(0.1f);;

		myanim.SetBool("Attack",false);

	}

	//--------------------------------------------------------------------------------------------------------------

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
		
		StateManagerScript mySMS = myself.GetComponent<StateManagerScript> ();

		if (mySMS.nowstate == StateManagerScript.state.idle) {//idle状態のみ有効（連打の防止）
			

			mySMS.Attack ();


			//仮
			//---------- Animatorの管理
			Transform myhand = myself.transform.FindChild("Hand_Model");
			Animator myanim = myhand.gameObject.GetComponent<Animator> ();
			myanim.SetBool ("Attack", true);


		}
	}

	//---------- Attackの処理そのものの関数

	public void Attack_Process(GameObject opponent, GameObject myself){


		if (touched == true) {//指先のコライダー同士が触れていたら

		StateManagerScript SMS = opponent.GetComponent<StateManagerScript> ();
		StateManagerScript mySMS = myself.GetComponent<StateManagerScript> ();
		PlayerHPScript PHPS = opponent.GetComponent<PlayerHPScript> ();

		//---------- Animatorの管理
		Transform myhand = myself.transform.FindChild("Hand_Model");
		Animator myanim = myhand.gameObject.GetComponent<Animator> ();
		//myanim.SetBool ("Attack", true);

		//---------- 相手のAnimator
		Transform opphand = opponent.transform.FindChild("Hand_Model");
		Animator oppanim = opphand.gameObject.GetComponent<Animator> ();


			switch (SMS.nowstate) {
			case StateManagerScript.state.idle://相手が「idle」だっったら
				mySMS.Bind_S ();//自分を「bind_s」状態に

				myanim.SetBool("Bind",true);

				StartCoroutine( "BindTrue_before_AttackFalse" , myself);
				 
				break;
			case StateManagerScript.state.guard://相手が「guard」だっったら
				mySMS.Bind_S ();//自分を「bind_s」状態に
				myanim.SetBool("Attack",false);
				break;
			case StateManagerScript.state.check://相手が「check」だっったら
				SMS.nowstate = StateManagerScript.state.held_m;//相手を「held_m」状態に
				oppanim.SetInteger("Held_State",1);

				mySMS.nowstate = StateManagerScript.state.hold_m;//自分を「hold_m」状態に
				myanim.SetBool("Hold",true);

				PHPS.Damage_M ();//相手にダメージ（中）を与える
				break;
			case StateManagerScript.state.attack://相手が「attack」だっったら
				player1.transform.position = Vector3.Lerp(player1.transform.position,new Vector3(-5,0,0),3.0f);//仕切り直し
				player2.transform.position = Vector3.Lerp(player2.transform.position,new Vector3(5,0,0),3.0f);//仕切り直し
				myanim.SetBool("Attack",false);
				break;
			case StateManagerScript.state.bind_l://相手が「bind_l」だっったら

				SMS.nowstate = StateManagerScript.state.held_m;//相手を「held_m」状態に
				oppanim.SetInteger("Held_State",1);

				mySMS.nowstate = StateManagerScript.state.hold_m;//自分を「hold_m」状態に
				myanim.SetBool("Hold",true);

				PHPS.Damage_M ();//相手にダメージ（中）を与える
				break;
			case StateManagerScript.state.bind_s://相手が「bind_s」だっったら

				SMS.nowstate = StateManagerScript.state.held_m;//相手を「held_m」状態に
				oppanim.SetInteger("Held_State",1);

				mySMS.nowstate = StateManagerScript.state.hold_m;//自分を「hold_m」状態に
				myanim.SetBool("Hold",true);

				PHPS.Damage_M ();//相手にダメージ（中）を与える
				break;
			}
		}
	}

	//InputManagerScriptから○ボタン入力時に第1引数に[相手]を第2引数に[自分]を渡して呼び出す
	public void Snake(GameObject opponent, GameObject myself) {
		//StateManagerScript SMS = opponent.GetComponent<StateManagerScript> ();
		StateManagerScript mySMS = myself.GetComponent<StateManagerScript> ();
		//PlayerHPScript PHPS = opponent.GetComponent<PlayerHPScript> ();

		if (mySMS.nowstate == StateManagerScript.state.idle) {//idle状態のみ有効（連打の防止）

			//---------- Animatorの管理
			Transform myhand = myself.transform.FindChild("Hand_Model");
			Animator myanim = myhand.gameObject.GetComponent<Animator> ();
			myanim.SetBool ("Snake", true);

			mySMS.Snake ();


		}
	}

	//---------- Snakeの処理
	private void SnakeProcess(GameObject opponent, GameObject myself){

		Transform indexfinger = myself.transform.FindChild ("joint22");
		IndexFingerScript IFS = indexfinger.gameObject.GetComponent<IndexFingerScript> ();

		if (IFS.cansnake == true) {

			StateManagerScript SMS = opponent.GetComponent<StateManagerScript> ();
			StateManagerScript mySMS = myself.GetComponent<StateManagerScript> ();
			PlayerHPScript PHPS = opponent.GetComponent<PlayerHPScript> ();

			//---------- Animatorの管理
			Transform myhand = myself.transform.FindChild ("Hand_Model");
			Animator myanim = myhand.gameObject.GetComponent<Animator> ();
			myanim.SetBool ("Snake", true);

			//---------- 相手のAnimator
			Transform opphand = opponent.transform.FindChild ("Hand_Model");
			Animator oppanim = opphand.gameObject.GetComponent<Animator> ();


			if (touched == true) {//指先のコライダー同士が触れていたら

				switch (SMS.nowstate) {
				case StateManagerScript.state.idle://相手が「idle」だっったら
					SMS.Bind_S ();
					oppanim.SetBool ("Bind", true);
					myanim.SetBool ("Snake", false);
					Debug.Log ("OK");
					break;
				case StateManagerScript.state.guard://相手が「guard」だっったら
					mySMS.Bind_L ();//自分を「bind_s」状態に
					myanim.SetBool ("Snake", false);
					break;
				case StateManagerScript.state.check://相手が「check」だっったら

					SMS.nowstate = StateManagerScript.state.held_l;//相手を「held_l」状態に
					oppanim.SetInteger ("Held_State", 1);

					mySMS.nowstate = StateManagerScript.state.hold_l;//自分を「hold_l」状態に
					myanim.SetBool ("Hold", true);

					PHPS.Damage_L ();//相手にダメージ（大）を与える
					break;
				case StateManagerScript.state.bind_l://相手が「bind_l」だっったら

					SMS.nowstate = StateManagerScript.state.held_l;//相手を「held_l」状態に
					oppanim.SetInteger ("Held_State", 1);

					mySMS.nowstate = StateManagerScript.state.hold_l;//自分を「hold_l」状態に
					myanim.SetBool ("Hold", true);

					PHPS.Damage_L ();//相手にダメージ（大）を与える
					break;
				case StateManagerScript.state.bind_s://相手が「bind_s」だっったら

					SMS.nowstate = StateManagerScript.state.held_l;//相手を「held_l」状態に
					oppanim.SetInteger ("Held_State", 1);

					mySMS.nowstate = StateManagerScript.state.hold_l;//自分を「hold_l」状態に
					myanim.SetBool ("Hold", true);

					PHPS.Damage_L ();//相手にダメージ（大）を与える
					break;
				}
			}
		}
	}

}
