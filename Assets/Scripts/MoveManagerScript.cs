using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManagerScript : MonoBehaviour {

	const float player_speed = 0.3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//移動用関数。InputManagerScriptから動かしたいプレイヤーを第1引数に、左スティックのアナログ値を第2引数にして実行する。
	public void Player_Move(Transform t, float input)
	{
		t.Translate (Vector3.right * input * player_speed);
	}
}
	