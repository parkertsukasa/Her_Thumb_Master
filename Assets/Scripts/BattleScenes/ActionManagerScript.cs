using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ActionManagerScript : MonoBehaviour {

	private GameObject player1;
	private GameObject player2;

	private StateManagerScript SMS1;
	private StateManagerScript SMS2;

	public bool touched = false;

	private const float action_range = 2.3f;//攻撃等が行える距離の上限、定数


	public GameObject tsume_1p;//Pistolで発射する爪
	public GameObject tsume_2p;//Pistolで発射する爪

	private SEPlayScript SEPS;

	//public GameObject tsumeeffect;

	//public GameObject indexfinger;

	// Use this for initialization
	void Start () {
		player1 = GameObject.Find ("1P_Manager");
		player2 = GameObject.Find ("2P_Manager");
		SMS1 = player1.GetComponent<StateManagerScript> ();
		SMS2 = player2.GetComponent<StateManagerScript> ();
		SEPS = GetComponent<SEPlayScript> ();
		//FS = mythumb.GetComponent<FingerScript> ();

	}
	
	// Update is called once per frame
	void Update () {
		
		if (SMS1.nowstate == StateManagerScript.state.attack) {
				Attack_Process (player2,player1);
		}
		if (SMS2.nowstate == StateManagerScript.state.attack) {
				Attack_Process (player1,player2);
		}

		if (SMS1.nowstate == StateManagerScript.state.snake) {
			Snake_Process (player2,player1);
		}
		if (SMS2.nowstate == StateManagerScript.state.snake) {
			Snake_Process (player1,player2);
		}

		//Debug.Log (touched);

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
			SEPS.Guard ();
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
			SEPS.Charge ();
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
		SEPS.Stop ();
	}

	//InputManagerScriptから×ボタン入力時に第1引数に[相手]を第2引数に[自分]を渡して呼び出す
	public void Attack(GameObject opponent, GameObject myself) {
		
		StateManagerScript mySMS = myself.GetComponent<StateManagerScript> ();

		if (mySMS.nowstate == StateManagerScript.state.idle) {//idle状態のみ有効（連打の防止）
			

			mySMS.Attack ();
			SEPS.Attack ();


			//仮
			//---------- Animatorの管理
			Transform myhand = myself.transform.FindChild("Hand_Model");
			Animator myanim = myhand.gameObject.GetComponent<Animator> ();
			myanim.SetBool ("Attack", true);

			DuaringAttackMoveForwardScript DAMFS = myself.GetComponent<DuaringAttackMoveForwardScript> ();
			DAMFS.MoveForward ();

		}
	}

	//---------- Attackの処理そのものの関数

	public void Attack_Process(GameObject opponent, GameObject myself){


		if (touched == true) {//指先のコライダー同士が触れていたら

		StateManagerScript SMS = opponent.GetComponent<StateManagerScript> ();
		StateManagerScript mySMS = myself.GetComponent<StateManagerScript> ();
		PlayerHPScript PHPS = opponent.GetComponent<PlayerHPScript> ();

		AttackEffectScript AFS = myself.GetComponent<AttackEffectScript> ();

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
				SEPS.Bind ();
				myanim.SetBool("Bind",true);

				StartCoroutine( "BindTrue_before_AttackFalse" , myself);
				 
				break;
			case StateManagerScript.state.guard://相手が「guard」だっったら
				mySMS.Bind_S ();//自分を「bind_s」状態に
				AttackEffectScript oppAES = opponent.GetComponent<AttackEffectScript> ();
				oppAES.GuardEffect_On ();
				myanim.SetBool("Attack",false);
				SEPS.GuardSuccess ();
				SEPS.Bind ();
				break;
			case StateManagerScript.state.check://相手が「check」だっったら
				SMS.nowstate = StateManagerScript.state.held_m;//相手を「held_m」状態に
				oppanim.SetInteger ("Held_State", 1);

				mySMS.nowstate = StateManagerScript.state.hold_m;//自分を「hold_m」状態に
				myanim.SetBool ("Hold", true);

				SEPS.Hold ();

				AFS.Call_IF();

				PHPS.Damage_M ();//相手にダメージ（中）を与える
				break;
			case StateManagerScript.state.attack://相手が「attack」だっったら
				player1.transform.position = Vector3.Lerp(player1.transform.position,new Vector3(-5,0,0),3.0f);//仕切り直し
				player2.transform.position = Vector3.Lerp(player2.transform.position,new Vector3(5,0,0),3.0f);//仕切り直し
				SEPS.Hold ();
				AFS.Call_IF();
				myanim.SetBool("Attack",false);
				break;
			case StateManagerScript.state.bind_l://相手が「bind_l」だっったら

				SMS.nowstate = StateManagerScript.state.held_m;//相手を「held_m」状態に
				oppanim.SetInteger("Held_State",1);

				mySMS.nowstate = StateManagerScript.state.hold_m;//自分を「hold_m」状態に
				myanim.SetBool("Hold",true);

				SEPS.Hold ();

				AFS.Call_IF();

				PHPS.Damage_M ();//相手にダメージ（中）を与える
				break;
			case StateManagerScript.state.bind_s://相手が「bind_s」だっったら

				SMS.nowstate = StateManagerScript.state.held_m;//相手を「held_m」状態に
				oppanim.SetInteger("Held_State",1);

				mySMS.nowstate = StateManagerScript.state.hold_m;//自分を「hold_m」状態に
				myanim.SetBool("Hold",true);

				AFS.Call_IF();

				SEPS.Hold ();

				PHPS.Damage_M ();//相手にダメージ（中）を与える
				break;
			case StateManagerScript.state.pistol://相手が「pistol」だっったら

				SMS.nowstate = StateManagerScript.state.held_m;//相手を「held_m」状態に
				oppanim.SetInteger("Held_State",1);

				mySMS.nowstate = StateManagerScript.state.hold_m;//自分を「hold_m」状態に
				myanim.SetBool("Hold",true);

				AFS.Call_IF();

				SEPS.Hold ();

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

			SEPS.Snake ();


		}
	}

	//---------- Snakeの処理
	private void Snake_Process(GameObject opponent, GameObject myself){

			StateManagerScript SMS = opponent.GetComponent<StateManagerScript> ();
			StateManagerScript mySMS = myself.GetComponent<StateManagerScript> ();
			//PlayerHPScript PHPS = opponent.GetComponent<PlayerHPScript> ();

			//---------- Animatorの管理
			Transform myhand = myself.transform.FindChild ("Hand_Model");
			Animator myanim = myhand.gameObject.GetComponent<Animator> ();
			myanim.SetBool ("Snake", true);

			//---------- 相手のAnimatorfor1P
			Transform opphand = opponent.transform.FindChild ("Hand_Model");
			Animator oppanim = opphand.gameObject.GetComponent<Animator> ();


			MyIndexFingerHasScript MIFHS = myself.GetComponent<MyIndexFingerHasScript> ();
			GameObject indexfinger = MIFHS.myindexfinger;
			IndexFingerScript IFS = indexfinger.gameObject.GetComponent<IndexFingerScript> ();

			if (IFS.cansnake == true) {

			//if (touched == true) {//指先のコライダー同士が触れていたら

				switch (SMS.nowstate) {
			case StateManagerScript.state.idle://相手が「idle」だっったら
				SMS.Bind_S ();
				SEPS.Bind ();
					SEPS.SnakeSuccess ();
					oppanim.SetBool ("Bind", true);
					myanim.SetBool ("Snake", false);
					break;

			case StateManagerScript.state.guard://相手が「guard」だっったら
				mySMS.Bind_L ();//自分を「bind_s」状態に
				SEPS.Bind ();
				AttackEffectScript oppAES = opponent.GetComponent<AttackEffectScript> ();
				oppAES.GuardEffect_On ();
				myanim.SetBool ("Snake", false);
				SEPS.GuardSuccess ();
					break;
				case StateManagerScript.state.check://相手が「check」だっったら

				/*
					SMS.nowstate = StateManagerScript.state.held_l;//相手を「held_l」状態に
					oppanim.SetInteger ("Held_State", 1);

					mySMS.nowstate = StateManagerScript.state.hold_l;//自分を「hold_l」状態に
					myanim.SetBool ("Hold", true);

					PHPS.Damage_L ();//相手にダメージ（大）を与える

*/
					break;
				case StateManagerScript.state.bind_l://相手が「bind_l」だっったら
				/*
					SMS.nowstate = StateManagerScript.state.held_l;//相手を「held_l」状態に
					oppanim.SetInteger ("Held_State", 1);

					mySMS.nowstate = StateManagerScript.state.hold_l;//自分を「hold_l」状態に
					myanim.SetBool ("Hold", true);

					PHPS.Damage_L ();//相手にダメージ（大）を与える
					*/
					break;
				case StateManagerScript.state.bind_s://相手が「bind_s」だっったら
				/*
					SMS.nowstate = StateManagerScript.state.held_l;//相手を「held_l」状態に
					oppanim.SetInteger ("Held_State", 1);

					mySMS.nowstate = StateManagerScript.state.hold_l;//自分を「hold_l」状態に
					myanim.SetBool ("Hold", true);

					PHPS.Damage_L ();//相手にダメージ（大）を与える
					*/
					break;
				//}
			}
		}
	}

	//---------- Pistolの発射 ----------
	public void Pistol(GameObject opponent, GameObject myself,int p){

		Transform myhand = myself.transform.FindChild ("Hand_Model");
		Animator myanim = myhand.GetComponent<Animator> ();
		StateManagerScript mySMS = myself.GetComponent<StateManagerScript> ();

		if (mySMS.nowstate == StateManagerScript.state.idle) {

			myanim.SetBool ("Pistol", true);//animatorをpistolに
			mySMS.Pistol ();//StateをPistolに
			if (p == 1) {
				Instantiate (tsume_1p, myself.transform.position, Quaternion.identity);//爪を生成
				AttackEffectScript AES = myself.GetComponent<AttackEffectScript>();
				AES.PistolEffect_On ();
				SEPS.P1_Tsumetobashi ();
			}
			if (p == 2) {
				Instantiate (tsume_2p, myself.transform.position, Quaternion.identity);//爪を生成
				AttackEffectScript AES = myself.GetComponent<AttackEffectScript>();
				AES.PistolEffect_On ();
				SEPS.P2_Tsumetobashi ();
			}
		}
	}
}
