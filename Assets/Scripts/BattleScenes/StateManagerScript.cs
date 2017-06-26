using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StateManagerScript : MonoBehaviour {

	public enum state {
		idle, guard, check, attack, snake, bind_s, bind_l, hold_s, hold_m, hold_l, held_s, held_m, held_l,force_check,pistol,hissatsu
	}

	public Text STATE;

	public state nowstate = state.idle;//現在の状態を格納する変数

	private float timer = 0.0f;//「bind」の時間を測るタイマー

	private const float bindtime_short = 2.0f;//「bind_s」の持続時間
	private const float bindtime_long = 3.0f;//「bind_l」の持続時間

	private Transform hand;
	private Animator anim;



	public enum move_state
	{
		idle, walk,idle_dush, dash
	}

	public move_state runstate = move_state.idle;//2度押しでダッシュをするために状態を取得する


	// Use this for initialization
	void Start () {
		hand = transform.FindChild ("Hand_Model");
		anim = hand.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		STATE.text = nowstate.ToString();

		BindReset ();

		ForceCheckReset ();

		Reset_to_Idle ();

	}

	//---------- Animation再生終了後にIdleに戻す
	private void Reset_to_Idle(){
		if(nowstate == state.attack || nowstate == state.snake || nowstate == state.pistol || nowstate == state.hissatsu){
			if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f){
				nowstate = state.idle;
				anim.SetBool ("Attack", false);
				anim.SetBool ("Snake", false);
				anim.SetBool ("Pistol", false);
				anim.SetBool ("Hissatsu", false);
			}
		}
	}

	private void ForceCheckReset(){
		if (nowstate == state.force_check) {
			timer += Time.deltaTime;
			if (timer >= 1.5f) {
				Idle ();
				timer = 0;
			}
		}
	}




	//----------一定時間経過でbind状態からの復帰
	private void BindReset(){
		//bind状態（短）からの復帰
		if (nowstate == state.bind_s) {
			timer += Time.deltaTime;
			if (timer >= bindtime_short) {
				Idle ();
				timer = 0;
			}
		}
		//bind状態（長）からの復帰
		if (nowstate == state.bind_l) {
			timer += Time.deltaTime;
			if (timer >= bindtime_long) {
				Idle ();
				timer = 0;
			}
		}
			
	}

	//InputManagerScriptから□ボタン入力時に呼び出す
	public void Guard(){
		nowstate = state.guard;
	}

	//InputManagerScriptから△ボタン入力時に呼び出す
	public void Check(){
		nowstate = state.check;
	}

	//InputManagerScriptから×ボタン入力時に呼び出す
	public void Attack(){
		nowstate = state.attack;
	}

	//InputManagerScriptから○ボタン入力時に呼び出す
	public void Snake(){
		nowstate = state.snake;
	}

	//攻撃失敗時に呼び出す(短)
	public void Bind_S(){
		nowstate = state.bind_s;
	}
		
	//攻撃失敗時に呼び出す(長)
	public void Bind_L(){
		nowstate = state.bind_l;
	}

	public void ForceCheck(){
		nowstate = state.force_check;
	}

	public void Pistol(){
		nowstate = state.pistol;
	}

	public void Hissatsu(){
		nowstate = state.hissatsu;
	}



	//Idle状態に戻す
	public void Idle(){
		nowstate = state.idle;

		//---------- Animatorの管理
		Transform myhand = transform.FindChild("Hand_Model");
		Animator myanim = myhand.gameObject.GetComponent<Animator> ();
		myanim.SetBool ("Hold",false);
		myanim.SetBool ("Guard",false);
		myanim.SetBool ("Check",false);
		myanim.SetBool ("Attack",false);
		myanim.SetBool ("Snake",false);
		myanim.SetBool ("Bind", false);
	}
		
}
