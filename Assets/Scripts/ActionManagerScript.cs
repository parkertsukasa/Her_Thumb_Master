using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManagerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//InputManagerScriptから□ボタン入力時に[相手]を引数にして呼び出す
	public void Guard(GameObject opponent) {
		StateManagerScript SMS = opponent.GetComponent<StateManagerScript> ();
		switch (SMS.nowstate) {
		case StateManagerScript.state.attack://相手が「attack」だっったら
			SMS.Bind_S();//相手を「bind_s」状態に
			break;
		case StateManagerScript.state.snake://相手が「snake」だっったら
			SMS.Bind_L();//相手を「bind_l」状態に
			break;
		}
	}




}
