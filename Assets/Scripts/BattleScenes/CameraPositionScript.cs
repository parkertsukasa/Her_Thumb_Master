using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionScript : MonoBehaviour {

	private GameObject player1;
	private GameObject player2;

	private StateManagerScript SMS1P;
	private StateManagerScript SMS2P;


	private bool battle = true;

	private enum camstate
	{
		close,near,mid,far,hissatsu_1p,hissatsu_2p
	}

	private camstate nowcam = camstate.mid;

	public float distance;//1Pと2Pの距離を入れる変数
	private float center;//1Pと2Pの中心位置のx座標

	// Use this for initialization
	void Start () {

		player1 = GameObject.Find ("1P_Manager");
		SMS1P = player1.GetComponent<StateManagerScript> ();

		player2 = GameObject.Find ("2P_Manager");
		SMS2P = player2.GetComponent<StateManagerScript> ();

	}
	
	// Update is called once per frame
	void Update () {

		distance = Mathf.Abs( player2.transform.position.x - player1.transform.position.x);
		Debug.Log ("Distance =" + distance);

		center = (player1.transform.position.x + player2.transform.position.x) / 2.0f;


		//---------- 通常時にプレイヤー間の距離に応じてstate変更 --------------------
		if (battle == true) {
			if (distance >= 10.0f) {
				nowcam = camstate.far;
			} else if (distance <= 3.0f) {
				nowcam = camstate.close;
			} else if (distance >= 3.0f && distance <= 6.0f) {
				nowcam = camstate.near;
			} else {
				nowcam = camstate.mid;
			}
		}

		if (SMS1P.nowstate == StateManagerScript.state.hissatsu ||
		    SMS2P.nowstate == StateManagerScript.state.hissatsu) {

			battle = false;

			if (SMS1P.nowstate == StateManagerScript.state.hissatsu) {
				nowcam = camstate.hissatsu_1p;
			}
			if (SMS2P.nowstate == StateManagerScript.state.hissatsu) {
				nowcam = camstate.hissatsu_2p;
			}


		} else {
			battle = true;
		}


		//---------- stateに応じた位置 ----------
		CamPosChange();
			

	}

	private void CamPosChange(){

		switch (nowcam) {

		case camstate.close:

			transform.position = Vector3.Lerp (transform.position ,new Vector3(center, 2, -7.5f),Time.deltaTime);

			break;

		case camstate.near:

			transform.position = Vector3.Lerp (transform.position ,new Vector3(center, 2, -10),Time.deltaTime);

			break;

		case camstate.mid:

			transform.position = Vector3.Lerp (transform.position ,new Vector3(center, 2, -12.5f),Time.deltaTime);

			break;

		case camstate.far:

			transform.position = Vector3.Lerp (transform.position ,new Vector3(center, 2, -10),Time.deltaTime);

			break;
		}

	}



}
