using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManagerScript : MonoBehaviour {


	private const float player_speed = 0.3f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//移動用関数。InputManagerScriptから動かしたいプレイヤーを第1引数に、左スティックのアナログ値を第2引数にして実行する。
	public void Player_Move(GameObject p, float input)
	{
		StateManagerScript SMS;
		SMS = p.GetComponent<StateManagerScript> ();

		Debug.Log ("RUN");
		
		if (SMS.runstate == StateManagerScript.move_state.idle || SMS.runstate == StateManagerScript.move_state.walk) {
			p.transform.Translate (Vector3.right * input * player_speed);
			SMS.runstate = StateManagerScript.move_state.walk;
		}
		if (SMS.runstate == StateManagerScript.move_state.idle_dush || SMS.runstate == StateManagerScript.move_state.dash) {
			p.transform.Translate (Vector3.right * input * player_speed * 2.0f);
			SMS.runstate = StateManagerScript.move_state.dash;
		}

	}
}
	