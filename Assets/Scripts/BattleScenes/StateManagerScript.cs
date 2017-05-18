using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StateManagerScript : MonoBehaviour {

	public enum state {
		idle, guard, check, attack, snake, bind_s, bind_l, hold_s, hold_m, hold_l, held_s, held_m, held_l
	}

	public Text STATE;

	public state nowstate = state.idle;//現在の状態を格納する変数

	private float timer = 0.0f;//「bind」の時間を測るタイマー

	private const float bindtime_short = 1.0f;//「bind_s」の持続時間
	private const float bindtime_long = 3.0f;//「bind_s」の持続時間


	public enum move_state
	{
		idle, walk,idle_dush, dash
	}

	public move_state runstate = move_state.idle;//2度押しでダッシュをするために状態を取得する


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		STATE.text = nowstate.ToString();

	}


	//----------一定時間経過でbind状態からの復帰
	private void BindReset(){
		//bind状態（短）からの復帰
		if (nowstate == state.bind_s) {
			timer += Time.deltaTime;
			if (timer >= bindtime_short) {
				nowstate = state.idle;
				timer = 0;
			}
		}
		//bind状態（長）からの復帰
		if (nowstate == state.bind_l) {
			timer += Time.deltaTime;
			if (timer >= bindtime_long) {
				nowstate = state.idle;
				timer = 0;
			}
		}

		//idle_dushの解除
		if (runstate == move_state.idle_dush) {
			Invoke ("StateReset", 0.3f);
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

	//Idle状態に戻す
	public void Idle(){
		nowstate = state.idle;
	}

	public void Idle_one_seconds_after(){
		Invoke ("Idle", 1.0f);
	}


}
