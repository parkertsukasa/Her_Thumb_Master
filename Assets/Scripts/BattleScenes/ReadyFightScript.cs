using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyFightScript : MonoBehaviour {

	public GameObject ready;
	public GameObject Fight;

	// Use this for initialization
	void Start () {
		ready.SetActive (true);
		Fight.SetActive (false);
		Invoke ("After_1sec", 1.0f);
		Invoke ("After_2sec", 2.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void After_1sec(){
		ready.SetActive (false);
		Fight.SetActive (true);
	}

	private void After_2sec(){
		ready.SetActive (false);
		Fight.SetActive (false);
	}
}
