using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerScript : MonoBehaviour {

	private GameObject manager;
	ActionManagerScript AMS;

	public GameObject effect;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("Battle_Manager");
		AMS = manager.GetComponent<ActionManagerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Finger") {
			AMS.Touched ();
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Finger") {
			AMS.Released ();
		}
	}

	public void InstanceEffect(){
		Instantiate (effect, this.transform.position, this.transform.rotation);
	}
}
