using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManagerScript : MonoBehaviour {

	GameObject player1;
	GameObject player2;

	PlayerHPScript PHPS_1P;
	PlayerHPScript PHPS_2P;

	int hp_1p;
	int hp_2p;

	public GameObject hpb1;
	public GameObject hpb2;

	public Image hpber_1p;
	public Image hpber_2p;

	// Use this for initialization
	void Start () {
		player1 = GameObject.Find ("1P_Manager");
		player2 = GameObject.Find ("2P_Manager");
		PHPS_1P = player1.GetComponent<PlayerHPScript> ();
		PHPS_2P = player2.GetComponent<PlayerHPScript> ();

		hpber_1p = hpb1.GetComponent<Image> ();
		hpber_2p = hpb2.GetComponent<Image> ();

		hp_1p = PHPS_1P.hp;
		hp_2p = PHPS_2P.hp;

	}
	
	// Update is called once per frame
	void Update () {

		hp_1p = PHPS_1P.hp;
		hp_2p = PHPS_2P.hp;

		hpber_1p.fillAmount = hp_1p * 0.01f;
		hpber_2p.fillAmount = hp_2p * 0.01f;
	}
		
}
