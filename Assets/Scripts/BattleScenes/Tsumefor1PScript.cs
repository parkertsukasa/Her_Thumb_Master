using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tsumefor1PScript : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
		Invoke ("Destroy", 5.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Translate (Vector3.right * speed * Time.deltaTime);

	}

	void Destroy(){
		Destroy (gameObject);
	}
}
