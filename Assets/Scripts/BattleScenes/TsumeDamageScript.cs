using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsumeDamageScript : MonoBehaviour {

	public GameObject manager;
	PlayerHPScript PHPS;

	public int playernum;

	// Use this for initialization
	void Start () {
		PHPS = manager.GetComponent<PlayerHPScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
		if (playernum == 1 && col.gameObject.tag == "Tsume2P" || playernum == 2 && col.gameObject.tag == "Tsume1P") {
			PHPS.Pistol ();
		}
	}
}
