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

	float timer = 0.0f;//「bind」の時間を測るタイマー

	const float bindtime_short = 1.0f;//「bind_s」の持続時間
	const float bindtime_long = 3.0f;//「bind_s」の持続時間


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		STATE.text = nowstate.ToString();

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


}
