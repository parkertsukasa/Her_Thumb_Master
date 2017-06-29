using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolEffectDestroyScript : MonoBehaviour {

	public float t;

	// Use this for initialization
	void Start () {
		Invoke ("Destroy", t);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Destroy(){
		Destroy (gameObject);
	}
}
