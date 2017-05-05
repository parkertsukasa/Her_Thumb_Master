using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldManagerScript : MonoBehaviour {

	private int escape = 0;

	private float hrzn;
	private float vrti;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		hrzn = Input.GetAxis ("1P_LeftHorizontal");
		vrti = Input.GetAxis ("1P_LeftVertical");

		if (hrzn >= 0.8f || hrzn <= -0.8f) {
			escape += 1;
		}
		if (vrti >= 0.8f || vrti <= -0.8f) {
			escape += 1;
		}

		Debug.Log ("ぐるぐる" + escape);
	}
}
