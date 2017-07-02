using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManagerScript : MonoBehaviour {


	private const float player_speed = 5.0f;

	HPManagerScript HPMS;


	private GameObject player1;
	private GameObject player2;
	private Rigidbody r1;
	private Rigidbody r2;

	private float distance;

	public float againstpower;

	public float mindis;



	// Use this for initialization
	void Start () {
		HPMS = GetComponent<HPManagerScript> ();

		player1 = GameObject.Find ("1P_Manager");
		player2 = GameObject.Find ("2P_Manager");
		r1 = player1.GetComponent<Rigidbody> ();
		r2 = player2.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

		distance = Mathf.Abs( player2.transform.position.x - player1.transform.position.x);

		if (distance <= mindis) {
			r1.AddForce (Vector3.right * -againstpower);
			r2.AddForce (Vector3.right * againstpower);
		}

		if (player1.transform.position.x < -12) {
			r1.velocity = Vector3.zero;
			r1.AddForce (Vector3.right * againstpower * 5);
		}
		if (player2.transform.position.x > 12) {
			r2.velocity = Vector3.zero;
			r2.AddForce (Vector3.right * -againstpower * 5);
		}

	}

	//移動用関数。InputManagerScriptから動かしたいプレイヤーを第1引数に、左スティックのアナログ値を第2引数にして実行する。
	public void Player_Move(GameObject p, float input ,int pnum)
	{
		float playerspeed;
		if (pnum == 1) {
			playerspeed = HPMS.states_1p.speed;
		} else {
			playerspeed = HPMS.states_2p.speed;
		}

		StateManagerScript SMS;
		SMS = p.GetComponent<StateManagerScript> ();

		if (SMS.nowstate == StateManagerScript.state.idle) {//idle時のみ移動可能に
			Rigidbody r = p.GetComponent<Rigidbody> ();
			if (distance >= mindis) {
				if (pnum == 1 && player1.transform.position.x >= -13 || pnum == 2 && player2.transform.position.x <= 13) {
					if (SMS.runstate == StateManagerScript.move_state.idle || SMS.runstate == StateManagerScript.move_state.walk) {
						r.velocity = (Vector3.right * input * player_speed * playerspeed);
						SMS.runstate = StateManagerScript.move_state.walk;
					}
					if (SMS.runstate == StateManagerScript.move_state.idle_dush || SMS.runstate == StateManagerScript.move_state.dash) {
						StartCoroutine (Dash (r, input));
						SMS.runstate = StateManagerScript.move_state.dash;
					}
				}
			} else {
				r = p.GetComponent<Rigidbody> ();
				r.velocity = Vector3.zero;
			}
		}
	}
				

	private IEnumerator Dash(Rigidbody r,float input){
		for (int i = 0; i < 5; i++) {
			if (distance > 2) {
				r.velocity = (Vector3.right * input * player_speed * 6.0f);
			}
			yield return null;
		}
		r.velocity = Vector3.zero;
	}

	public void Player_Stop(GameObject p){
		Rigidbody r = p.GetComponent<Rigidbody> ();
		r.velocity = Vector3.zero;
	}
}
	