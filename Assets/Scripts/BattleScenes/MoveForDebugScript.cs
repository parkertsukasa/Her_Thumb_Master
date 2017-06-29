using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForDebugScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//---------------- デバッグ用のキーボードによる移動 -----------------
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate (Vector3.right * 5.0f * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate (Vector3.right * -5.0f * Time.deltaTime);
		}
	}
}
