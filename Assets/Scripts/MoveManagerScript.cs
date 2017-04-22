using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManagerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//移動用関数。InputManagerScriptから動かしたいプレイヤーを引数にして実行する。
	public void Player_Move(Transform t)
	{
		t.Translate (Vector3.forward * 1);
	}
}
