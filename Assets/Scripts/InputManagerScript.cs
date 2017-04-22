using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerScript : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;

	MoveManagerScript MMS;

	StateManagerScript SMS;

	// Use this for initialization
	void Start () {
		MMS = GetComponent<MoveManagerScript> ();
		SMS = GetComponent<StateManagerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A)) {
			Transform t = player1.transform;
			MMS.Player_Move (t);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			Transform t = player2.transform;
			MMS.Player_Move (t);
		}
			
		if (Input.GetButton ("1P_Circle")) {
			Debug.Log ("1PMARU");
		}
		if (Input.GetButton ("2P_Circle")) {
			Debug.Log ("2PMARU");
		}

	}
}
