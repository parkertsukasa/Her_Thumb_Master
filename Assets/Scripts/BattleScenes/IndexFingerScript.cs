using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexFingerScript : MonoBehaviour {

	public bool cansnake = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Palm") {
			cansnake = true;
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Palm") {
			cansnake = false;
		}
	}
}
